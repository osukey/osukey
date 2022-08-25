// Copyright (c) ppy Pty Ltd <contact@ppy.sh>, sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using osu.Framework.Bindables;
using osu.Framework.Extensions.ExceptionExtensions;
using osu.Framework.Extensions.ObjectExtensions;
using osu.Framework.Graphics;
using osu.Framework.Logging;
using osu.Game.Configuration;
using osu.Game.Online.MisskeyAPI.Requests;
using osu.Game.Online.MisskeyAPI.Requests.Responses;
using osu.Game.Users;

namespace osu.Game.Online.MisskeyAPI
{
    public class APIAccess : Component, IAPIProvider
    {
        private readonly OsuConfigManager config;

        private readonly string versionHash;

        private readonly Auth authentication;

        private readonly Queue<APIRequest> queue = new Queue<APIRequest>();

        public string APIEndpointUrl { get; }

        public int APIVersion => 20220217; // We may want to pull this from the game version eventually.

        public Exception LastLoginError { get; private set; }

        public string ProvidedUsername { get; private set; }

        private string password;

        public IBindable<Requests.Responses.I> LocalUser => localUser;
        public IBindable<UserActivity> Activity => activity;

        private Bindable<Requests.Responses.I> localUser { get; } = new Bindable<Requests.Responses.I>(createGuestUser());
        private Bindable<UserActivity> activity { get; } = new Bindable<UserActivity>();

        protected bool HasLogin => authentication.Token.Value != null || (!string.IsNullOrEmpty(ProvidedUsername) && !string.IsNullOrEmpty(password));

        private readonly CancellationTokenSource cancellationToken = new CancellationTokenSource();

        private readonly Logger log;

        public APIAccess(OsuConfigManager config, EndpointConfiguration endpointConfiguration, string versionHash)
        {
            this.config = config;
            this.versionHash = versionHash;

            APIEndpointUrl = endpointConfiguration.APIEndpointUrl;

            authentication = new Auth(APIEndpointUrl);
            log = Logger.GetLogger(LoggingTarget.Network);

            ProvidedUsername = config.Get<string>(OsuSetting.Username);

            authentication.TokenString = config.Get<string>(OsuSetting.MisskeyToken);
            authentication.Token.ValueChanged += onTokenChanged;

            var thread = new Thread(run)
            {
                Name = "APIAccess",
                IsBackground = true
            };

            thread.Start();
        }

        private void onTokenChanged(ValueChangedEvent<AuthToken> e) => config.SetValue(OsuSetting.MisskeyToken, config.Get<bool>(OsuSetting.SavePassword) ? authentication.TokenString : string.Empty);

        internal new void Schedule(Action action) => base.Schedule(action);

        public string AccessToken => authentication.RequestAccessToken();

        /// <summary>
        /// Number of consecutive requests which failed due to network issues.
        /// ネットワークの問題が原因で失敗した連続したリクエストの数。
        /// </summary>
        private int failureCount;

        private void run()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                switch (State.Value)
                {
                    case APIState.Failing:
                        //todo: replace this with a ping request.
                        log.Add(@"In a failing state, waiting a bit before we try again...");
                        Thread.Sleep(5000);

                        if (!IsLoggedIn) goto case APIState.Connecting;

                        if (queue.Count == 0)
                        {
                            log.Add(@"Queueing a ping request");
                            Queue(new Requests.I());
                        }

                        break;

                    case APIState.Offline:
                    case APIState.Connecting:
                        // work to restore a connection...
                        // 接続を復元する作業...
                        if (!HasLogin)
                        {
                            state.Value = APIState.Offline;
                            Thread.Sleep(50);
                            continue;
                        }

                        state.Value = APIState.Connecting;

                        // save the username at this point, if the user requested for it to be.
                        // ユーザーが要求した場合は、この時点でユーザー名を保存します。
                        config.SetValue(OsuSetting.Username, config.Get<bool>(OsuSetting.SaveUsername) ? ProvidedUsername : string.Empty);

                        if (!authentication.HasValidAccessToken)
                        {
                            LastLoginError = null;

                            try
                            {
                                authentication.AuthenticateWithLogin(ProvidedUsername, password);
                            }
                            catch (Exception e)
                            {
                                //todo: this fails even on network-related issues. we should probably handle those differently.
                                LastLoginError = e;
                                log.Add(@"Login failed!");
                                password = null;
                                authentication.Clear();
                                continue;
                            }
                        }

                        var userReq = new Requests.I();

                        userReq.Failure += ex =>
                        {
                            if (ex is WebException webException && webException.Message == @"Unauthorized")
                            {
                                log.Add(@"Login no longer valid");
                                Logout();
                            }
                            else
                                failConnectionProcess();
                        };
                        userReq.Success += u =>
                        {
                            localUser.Value = u;

                            // todo: save/pull from settings
                            localUser.Value.Status.Value = new UserStatusOnline();
                            state.Value = APIState.Online;

                            failureCount = 0;
                        };

                        if (!handleRequest(userReq))
                        {
                            failConnectionProcess();
                            continue;
                        }

                        // The Success callback event is fired on the main thread, so we should wait for that to run before proceeding.
                        // Without this, we will end up circulating this Connecting loop multiple times and queueing up many web requests
                        // before actually going online.
                        // Success コールバック イベントはメイン スレッドで発生するため、続行する前にそれが実行されるのを待つ必要があります。
                        // これがないと、実際にオンラインになる前に、この接続ループを何度も循環させ、多くの Web 要求をキューに入れることになります。

                        while (State.Value > APIState.Offline && State.Value < APIState.Online)
                            Thread.Sleep(500);

                        break;
                }

                // hard bail if we can't get a valid access token.
                // 有効なアクセス トークンを取得できない場合はハード ベイルします。
                if (authentication.RequestAccessToken() == null)
                {
                    Logout();
                    continue;
                }

                while (true)
                {
                    APIRequest req;

                    lock (queue)
                    {
                        if (queue.Count == 0) break;

                        req = queue.Dequeue();
                    }

                    handleRequest(req);
                }

                Thread.Sleep(50);
            }

            void failConnectionProcess()
            {
                // if something went wrong during the connection process, we want to reset the state (but only if still connecting).
                // 接続プロセス中に何か問題が発生した場合は、状態をリセットします (ただし、まだ接続している場合のみ)。
                if (State.Value == APIState.Connecting)
                    state.Value = APIState.Failing;
            }
        }

        public void Perform(APIRequest request)
        {
            try
            {
                request.Perform(this);
            }
            catch (Exception e)
            {
                // todo: fix exception handling
                request.Fail(e);
            }
        }

        public Task PerformAsync(APIRequest request) =>
            Task.Factory.StartNew(() => Perform(request), TaskCreationOptions.LongRunning);

        public void Login(string username, string password)
        {
            Debug.Assert(State.Value == APIState.Offline);

            ProvidedUsername = username;
            this.password = password;
        }

        // public IHubClientConnector GetHubConnector(string clientName, string endpoint, bool preferMessagePack) =>
        //     new HubClientConnector(clientName, endpoint, this, versionHash, preferMessagePack);

        public RegistrationRequest.RegistrationRequestErrors CreateAccount(string email, string username, string password)
        {
            Debug.Assert(State.Value == APIState.Offline);

            var req = new RegistrationRequest
            {
                Url = $@"{APIEndpointUrl}/users",
                Method = HttpMethod.Post,
                Username = username,
                Email = email,
                Password = password
            };

            try
            {
                req.Perform();
            }
            catch (Exception e)
            {
                try
                {
                    return JObject.Parse(req.GetResponseString().AsNonNull()).SelectToken("form_error", true).AsNonNull().ToObject<RegistrationRequest.RegistrationRequestErrors>();
                }
                catch
                {
                    // if we couldn't deserialize the error message let's throw the original exception outwards.
                    // エラー メッセージをデシリアライズできなかった場合は、元の例外を外側にスローしましょう。
                    e.Rethrow();
                }
            }

            return null;
        }

        /// <summary>
        /// Handle a single API request.
        /// Ensures all exceptions are caught and dealt with correctly.
        /// 単一の API リクエストを処理します。すべての例外がキャッチされ、正しく処理されるようにします。
        /// </summary>
        /// <param name="req">The request.</param>
        /// <returns>true if the request succeeded.</returns>
        private bool handleRequest(APIRequest req)
        {
            try
            {
                req.Perform(this);

                if (req.CompletionState != APIRequestCompletionState.Completed)
                    return false;

                // we could still be in initialisation, at which point we don't want to say we're Online yet.
                // まだ初期化中の可能性がありますが、その時点ではまだオンラインであるとは言いたくありません。
                if (IsLoggedIn) state.Value = APIState.Online;
                failureCount = 0;
                return true;
            }
            catch (HttpRequestException re)
            {
                log.Add($"{nameof(HttpRequestException)} while performing request {req}: {re.Message}");
                handleFailure();
                return false;
            }
            catch (SocketException se)
            {
                log.Add($"{nameof(SocketException)} while performing request {req}: {se.Message}");
                handleFailure();
                return false;
            }
            catch (WebException we)
            {
                log.Add($"{nameof(WebException)} while performing request {req}: {we.Message}");
                handleWebException(we);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while handling an API request.");
                return false;
            }
        }

        private readonly Bindable<APIState> state = new Bindable<APIState>();

        /// <summary>
        /// The current connectivity state of the API.
        /// API の現在の接続状態。
        /// </summary>
        public IBindable<APIState> State => state;

        private void handleWebException(WebException we)
        {
            HttpStatusCode statusCode = (we.Response as HttpWebResponse)?.StatusCode
                                        ?? (we.Status == WebExceptionStatus.UnknownError ? HttpStatusCode.NotAcceptable : HttpStatusCode.RequestTimeout);

            // special cases for un-typed but useful message responses.
            // タイプされていないが有用なメッセージ応答の特殊なケース。
            switch (we.Message)
            {
                case "Unauthorized":
                case "Forbidden":
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
            }

            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    Logout();
                    break;

                case HttpStatusCode.RequestTimeout:
                    handleFailure();
                    break;
            }
        }

        private void handleFailure()
        {
            failureCount++;
            log.Add($@"API failure count is now {failureCount}");

            if (failureCount >= 3 && State.Value == APIState.Online)
            {
                state.Value = APIState.Failing;
                flushQueue();
            }
        }

        public bool IsLoggedIn => localUser.Value.Id != "system" && localUser.Value.Id != "guest"; // TODO: should this also be true if attempting to connect?

        public void Queue(APIRequest request)
        {
            lock (queue)
            {
                if (state.Value == APIState.Offline)
                {
                    request.Fail(new WebException(@"User not logged in"));
                    return;
                }

                queue.Enqueue(request);
            }
        }

        private void flushQueue(bool failOldRequests = true)
        {
            lock (queue)
            {
                var oldQueueRequests = queue.ToArray();

                queue.Clear();

                if (failOldRequests)
                {
                    foreach (var req in oldQueueRequests)
                        req.Fail(new WebException($@"Request failed from flush operation (state {state.Value})"));
                }
            }
        }

        public void Logout()
        {
            password = null;
            authentication.Clear();

            // Scheduled prior to state change such that the state changed event is invoked with the correct user and their friends present
            // 正しいユーザーとその友人が存在する状態で状態変更イベントが呼び出されるように、状態変更の前にスケジュールされます
            Schedule(() =>
            {
                localUser.Value = createGuestUser();
                // friends.Clear();
            });

            state.Value = APIState.Offline;
            flushQueue();
        }

        private static Requests.Responses.I createGuestUser() => new GuestUser();

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            flushQueue();
            cancellationToken.Cancel();
        }
    }

    internal class GuestUser : Requests.Responses.I
    {
        public GuestUser()
        {
            Username = @"Guest";
            Id = SYSTEM_USER_ID;
        }
    }

    public enum APIState
    {
        /// <summary>
        /// We cannot login (not enough credentials).
        /// ログインできません (資格情報が不足しています)。
        /// </summary>
        Offline,

        /// <summary>
        /// We are having connectivity issues.
        /// 接続に問題があります。
        /// </summary>
        Failing,

        /// <summary>
        /// We are in the process of (re-)connecting.
        /// 私たちは（再）接続の過程にあります。
        /// </summary>
        Connecting,

        /// <summary>
        /// We are online.
        /// 私たちはオンラインです。
        /// </summary>
        Online
    }
}
