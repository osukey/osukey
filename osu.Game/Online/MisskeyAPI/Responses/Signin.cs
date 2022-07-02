// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using Newtonsoft.Json;

namespace osu.Game.Online.MisskeyAPI.Requests.Responses
{
    public class Signin
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("i")]
        public string i;
    }
}
