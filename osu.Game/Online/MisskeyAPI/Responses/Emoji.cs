// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace osu.Game.Online.MisskeyAPI.Requests.Responses
{
    public class Emoji : IEquatable<Emoji>
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("aliases")]
        public List<string> Aliases;

        [JsonProperty("category")]
        public string Category;

        [JsonProperty("host")]
        public string Host;

        [JsonProperty("url")]
        public string Url;

        public bool Equals(Emoji other) => Id == other?.Id;
    }
}
