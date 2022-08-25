// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Globalization;
using Newtonsoft.Json;

namespace osu.Game.Online.MisskeyAPI
{
    [Serializable]
    public class AuthToken
    {
        /// <summary>
        /// Misskey access token.
        /// </summary>
        [JsonProperty(@"i")]
        public string AccessToken;

        [JsonProperty(@"id")]
        public string MyUserId;

        public long ExpiresIn
        {
            get => AccessTokenExpiry - DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            set => AccessTokenExpiry = DateTimeOffset.Now.AddSeconds(value).ToUnixTimeSeconds();
        }

        public bool IsValid => !string.IsNullOrEmpty(AccessToken) && ExpiresIn < 30;

        public long AccessTokenExpiry;

        public override string ToString() => $@"{AccessToken}";

        public static AuthToken Parse(string value)
        {
            return new AuthToken
            {
                AccessToken = value
            };
        }
    }
}
