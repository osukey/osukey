// Copyright (c) ppy Pty Ltd <contact@ppy.sh>, sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Net.Http;
using JetBrains.Annotations;
using Newtonsoft.Json;
using osu.Framework.IO.Network;
using osu.Game.Online.MisskeyAPI.Responses.Types;
using Realms;

namespace osu.Game.Online.MisskeyAPI.Requests.Notes
{
    public class HybridTimeline : APIRequest<Note[]>
    {
        private readonly string i;

        // private readonly string? sinceId;
        private readonly string? untilId;

        public HybridTimeline(
            string i,
            string untilId = ""
        )
        {
            this.i = i;
            this.untilId = untilId;
        }

        public HybridTimeline(string i)
        {
            this.i = i;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;

            var body = new Dictionary<string, object>
            {
                { "i", i },
                { "limit", 30 },
            };

            // if (sinceId != null)
            //     body.Add("sinceId", sinceId);
            if (untilId != null)
                body.Add("untilId", untilId);

            req.AddRaw(JsonConvert.SerializeObject(body));

            return req;
        }

        protected override string Target => @"notes/local-timeline";
    }
}
