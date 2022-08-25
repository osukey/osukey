// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using JetBrains.Annotations;
using Newtonsoft.Json;
using osu.Framework.IO.Network;
using osu.Framework.Logging;
using osu.Game.Online.MisskeyAPI.Requests.Responses;

namespace osu.Game.Online.MisskeyAPI
{
    /// <summary>
    /// An API request with a well-defined response type.
    /// </summary>
    /// <typeparam name="T">Type of the response (used for deserialisation).</typeparam>
    public abstract class APIRequest<T> : APIRequest where T : class
    {
        protected override WebRequest CreateWebRequest() => new JsonWebRequest<T>(Uri);




        /// <summary>
        /// The deserialised response object. May be null if the request or deserialisation failed.
        /// 逆シリアル化された応答オブジェクト。リクエストまたは逆シリアル化が失敗した場合、null になることがあります。
        /// </summary>
        [CanBeNull]
        public T Response { get; private set; }

        /// <summary>
        /// Invoked on successful completion of an API request.
        /// This will be scheduled to the API's internal scheduler (run on update thread automatically).
        /// API リクエストが正常に完了すると呼び出されます。これは、API の内部スケジューラにスケジュールされます (更新スレッドで自動的に実行されます)。
        /// </summary>
        public new event APISuccessHandler<T> Success;

        protected APIRequest()
        {
            base.Success += () => Success?.Invoke(Response);
        }

        protected override void PostProcess()
        {
            base.PostProcess();

            if (WebRequest != null)
            {
                Response = ((JsonWebRequest<T>)WebRequest).ResponseObject;
                Logger.Log($"{GetType()} finished with response size of {WebRequest.ResponseStream.Length:#,0} bytes", LoggingTarget.Network);
            }
        }

        internal void TriggerSuccess(T result)
        {
            if (Response != null)
                throw new InvalidOperationException("Attempted to trigger success more than once");

            Response = result;

            TriggerSuccess();
        }
    }

    /// <summary>
    /// AN API request with no specified response type.
    /// 応答タイプが指定されていない API 要求。
    /// </summary>
    public abstract class APIRequest
    {
        // ターゲット指定
        protected abstract string Target { get; }

        // リクエスト
        protected virtual WebRequest CreateWebRequest() => new WebRequest(Uri);

        // リクエストURI
        protected virtual string Uri => $@"{API.APIEndpointUrl}/api/{Target}";

        protected APIAccess API;
        protected WebRequest WebRequest;

        //// <summary>
        //// The currently logged in user. Note that this will only be populated during <see cref="Perform"/>.
        //// </summary>
        protected I User { get; private set; }

        /// <summary>
        /// Invoked on successful completion of an API request.
        /// This will be scheduled to the API's internal scheduler (run on update thread automatically).
        /// API リクエストが正常に完了すると呼び出されます。これは、API の内部スケジューラにスケジュールされます (更新スレッドで自動的に実行されます)。
        /// </summary>
        public event APISuccessHandler Success;

        /// <summary>
        /// Invoked on failure to complete an API request.
        /// This will be scheduled to the API's internal scheduler (run on update thread automatically).
        /// API リクエストの完了に失敗したときに呼び出されます。これは、API の内部スケジューラにスケジュールされます (更新スレッドで自動的に実行されます)。
        /// </summary>
        public event APIFailureHandler Failure;

        private readonly object completionStateLock = new object();

        /// <summary>
        /// The state of this request, from an outside perspective.
        /// This is used to ensure correct notification events are fired.
        /// 外部から見たこのリクエストの状態。これは、正しい通知イベントが確実に発生するようにするために使用されます。
        /// </summary>
        public APIRequestCompletionState CompletionState { get; private set; }

        /// <summary>
        /// ここには、MisskeyのAPIのリクエストが入ります。
        /// iは自動挿入されます
        /// </summary>
        [CanBeNull]
        public Dictionary<string, string> Body { get; set; } = new Dictionary<string, string>();

        // 実行部分
        public void Perform(IAPIProvider api)
        {
            if (!(api is APIAccess apiAccess))
            {
                Fail(new NotSupportedException($"A {nameof(APIAccess)} is required to perform requests."));
                return;
            }

            API = apiAccess;
            User = apiAccess.LocalUser.Value;

            if (isFailing) return;

            WebRequest = CreateWebRequest();
            WebRequest.Failed += Fail;
            WebRequest.AllowRetryOnTimeout = false;

            WebRequest.AddHeader("x-api-version", API.APIVersion.ToString(CultureInfo.InvariantCulture));

            Body ??= new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(API.AccessToken))
                Body.Add("i", API.AccessToken);
            WebRequest.AddRaw(JsonConvert.SerializeObject(Body));

            if (isFailing) return;

            try
            {
                Logger.Log($@"Performing request {this}", LoggingTarget.Network);
                WebRequest.Perform();
            }
            catch (OperationCanceledException)
            {
                // ignore this. internally Perform is running async and the fail state may have changed since
                // the last check of `isFailing` above.
            }

            if (isFailing) return;

            PostProcess();

            TriggerSuccess();
        }

        /// <summary>
        /// Perform any post-processing actions after a successful request.
        /// リクエストが成功した後、後処理アクションを実行します。
        /// </summary>
        protected virtual void PostProcess()
        {
        }

        internal void TriggerSuccess()
        {
            lock (completionStateLock)
            {
                if (CompletionState != APIRequestCompletionState.Waiting)
                    return;

                CompletionState = APIRequestCompletionState.Completed;
            }

            if (API == null)
                Success?.Invoke();
            else
                API.Schedule(() => Success?.Invoke());
        }

        internal void TriggerFailure(Exception e)
        {
            lock (completionStateLock)
            {
                if (CompletionState != APIRequestCompletionState.Waiting)
                    return;

                CompletionState = APIRequestCompletionState.Failed;
            }

            if (API == null)
                Failure?.Invoke(e);
            else
                API.Schedule(() => Failure?.Invoke(e));
        }

        public void Cancel() => Fail(new OperationCanceledException(@"Request cancelled"));

        public void Fail(Exception e)
        {
            lock (completionStateLock)
            {
                if (CompletionState != APIRequestCompletionState.Waiting)
                    return;

                WebRequest?.Abort();

                // in the case of a cancellation we don't care about whether there's an error in the response.
                // キャンセルの場合、応答にエラーがあるかどうかは気にしません。
                if (!(e is OperationCanceledException))
                {
                    string responseString = WebRequest?.GetResponseString();

                    // naive check whether there's an error in the response to avoid unnecessary JSON deserialisation.
                    // 不必要な JSON の逆シリアル化を避けるために、応答にエラーがあるかどうかを素朴にチェックします。
                    if (!string.IsNullOrEmpty(responseString) && responseString.Contains(@"""error"""))
                    {
                        try
                        {
                            // attempt to decode a displayable error string.
                            // 表示可能なエラー文字列のデコードを試みます。
                            var error = JsonConvert.DeserializeObject<DisplayableError>(responseString);
                            if (error != null)
                                e = new APIException(error.ErrorMessage, e);
                        }
                        catch
                        {
                        }
                    }
                }

                Logger.Log($@"Failing request {this} ({e})", LoggingTarget.Network);
                TriggerFailure(e);
            }
        }

        /// <summary>
        /// Whether this request is in a failing or failed state.
        /// このリクエストが失敗または失敗した状態にあるかどうか。
        /// </summary>
        private bool isFailing
        {
            get
            {
                lock (completionStateLock)
                    return CompletionState == APIRequestCompletionState.Failed;
            }
        }

        private class DisplayableError
        {
            [JsonProperty("error")]
            public string ErrorMessage { get; set; }
        }
    }

    public delegate void APIFailureHandler(Exception e);

    public delegate void APISuccessHandler();

    public delegate void APIProgressHandler(long current, long total);

    public delegate void APISuccessHandler<in T>(T content);
}
