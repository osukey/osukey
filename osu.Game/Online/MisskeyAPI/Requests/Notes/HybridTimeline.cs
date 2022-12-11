// Copyright (c) ppy Pty Ltd <contact@ppy.sh>, sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Net.Http;
using JetBrains.Annotations;
using Newtonsoft.Json;
using osu.Framework.IO.Network;
using Realms;

namespace osu.Game.Online.MisskeyAPI.Requests.Notes
{
    public class HybridTimeline : APIRequest<MisskeyAPI.Requests.Responses.Notes.HybridTimeline>
    {
        private readonly string i;

        private readonly string sinceId;
        private readonly string untilId;

        public HybridTimeline(
            string i,
            string sinceId = "",
            string untilId = ""
        )
        {
            this.i = i;
            this.sinceId = sinceId;
            this.untilId = untilId;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;

            var body = new Dictionary<string, string>
            {
                { "i", i },
            };

            if (sinceId != "")
                body.Add("sinceId", sinceId);
            if (untilId != "")
                body.Add("untilId", untilId);

            req.AddRaw(JsonConvert.SerializeObject(body));

            return req;
        }

        protected override string Target => @"notes/hybrid-timeline";
    }
}
