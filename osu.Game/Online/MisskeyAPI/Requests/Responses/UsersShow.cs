// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using JetBrains.Annotations;

namespace osu.Game.Online.MisskeyAPI.Requests.Responses
{
    public class UsersShow
    {
        public partial class Emojis
        {
            [CanBeNull]
            public List<Emoji> Emoji { get; set; }
        }

        public partial class Emoji
        {
            public string Name { get; set; }
            public Uri Url { get; set; }
        }

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        [CanBeNull]
        public string Name;

        [JsonProperty("username")]
        public string Username;

        [JsonProperty("host")]
        [CanBeNull]
        public string Host;

        [JsonProperty("avatarUrl")]
        [CanBeNull]
        public Uri AvatarUrl;

        [JsonProperty("avatarBlurhash")]
        [CanBeNull]
        public string AvatarBlurhash;

        [JsonProperty("avatarColor")]
        [CanBeNull]
        public string AvatarColor;

        [JsonProperty("isAdmin")]
        public bool isAdmin;

        [JsonProperty("isModerator")]
        public bool isModerator;

        [JsonProperty("isBot")]
        public bool isBot;

        [JsonProperty("isCat")]
        public bool isCat;

        [JsonProperty("emojis")]
        public Emojis emojis;

        [JsonProperty("onlineStatus")]
        [CanBeNull]
        public string onlineStatus;




    }
}
