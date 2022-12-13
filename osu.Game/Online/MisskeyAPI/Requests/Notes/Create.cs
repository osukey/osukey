// Copyright (c) sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using osu.Framework.IO.Network;
using osu.Framework.Logging;

namespace osu.Game.Online.MisskeyAPI.Requests.Notes
{
    public class Create : APIRequest<MisskeyAPI.Responses.Types.Note>
    {
        private string text;
        private string i;
        private string? cw;

        public Create(string i, string Text)
        {
            this.i = i;
            this.text = Text;
        }
        public Create(string i, string Text, string cw)
        {
            this.i = i;
            this.text = Text;
            this.cw = cw;
        }

        private class ReqBody
        {
            public string? text;
            public string? i;
            public string? cw;
        };
        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;
            var body = new ReqBody()
            {
                text = text,
                i = i
            };
            if (cw != null)
            {
                body.cw = cw;
            }
            var json = JsonConvert.SerializeObject(body);
            Logger.Log(json, LoggingTarget.Network, LogLevel.Debug);
            req.AddRaw(json);

            return req;
        }

        protected override string Target => @"notes/create";
        // protected override string Uri => @"https://apicheck.sim1222.workers.dev/notes/create";
    }
}
