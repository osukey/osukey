// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace osu.Game.Online.MisskeyAPI.Requests.Responses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Meta //: IEquatable<Meta>
    {
        public class Feature
        {
            [JsonProperty("registration")]
            public bool Registration;

            [JsonProperty("localTimeLine")]
            public bool LocalTimeLine;

            [JsonProperty("globalTimeLine")]
            public bool GlobalTimeLine;

            [JsonProperty("hcaptcha")]
            public bool Hcaptcha;

            [JsonProperty("recaptcha")]
            public bool Recaptcha;

            [JsonProperty("serviceWorker")]
            public bool ServiceWorker;

            [JsonProperty("miauth")]
            public bool? Miauth;
        }

        [JsonProperty("maintainerName")]
        public string MaintainerName;

        [JsonProperty("maintainerEmail")]
        public string MaintainerEmail;

        [JsonProperty("version")]
        public string Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("uri")]
        public string Uri;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("langs")]
        public List<string> Langs;

        [JsonProperty("tosUrl")]
        public string TosUrl;

        [JsonProperty("defaultDarkTheme")]
        public string DefaultDarkTheme;

        [JsonProperty("defaultLightTheme")]
        public string DefaultLightTheme;

        [JsonProperty("driveCapacityPerLocalUserMb")]
        public int DriveCapacityPerLocalUserMb;

        [JsonProperty("emailRequiredForSignup")]
        public bool EmailRequiredForSignup;

        [JsonProperty("recaptchaSiteKey")]
        public string RecaptchaSiteKey;

        [JsonProperty("swPublickey")]
        public string SwPublickey;

        [JsonProperty("mascotImageUrl")]
        public string MascotImageUrl;

        [JsonProperty("bannerUrl")]
        public string BannerUrl;

        [JsonProperty("iconUrl")]
        public string IconUrl;

        [JsonProperty("maxNoteTextLength")]
        public int MaxNoteTextLength;

        [JsonProperty("emojis")]
        public List<Emoji> Emojis;

        [JsonProperty("requireSetup")]
        public bool RequireSetup;

        [JsonProperty("enableEmail")]
        public bool EnableEmail;

        [JsonProperty("enableServiceWorker")]
        public bool EnableServiceWorker;

        [JsonProperty("translatorAvailable")]
        public bool TranslatorAvailable;

        [JsonProperty("features")]
        public Feature Features;
    }
}
