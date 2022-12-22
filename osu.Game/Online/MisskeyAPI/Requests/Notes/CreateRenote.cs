// Copyright (c) sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using osu.Framework.IO.Network;
using osu.Framework.Logging;

namespace osu.Game.Online.MisskeyAPI.Requests.Notes
{
    public class CreateRenote : APIRequest<MisskeyAPI.Responses.Types.Note>
    {
        private string? text;
        private string i;
        private string renoteId;

        public CreateRenote(string i, string renoteId)
        {
            this.i = i;
            this.renoteId = renoteId;
        }

        public CreateRenote(string i, string renoteId, string Text)
        {
            this.i = i;
            this.text = Text;
            this.renoteId = renoteId;
        }

        private class ReqBody
        {
            public string? Text;
            public string? i;
            public string? renoteId;
        };
        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;
            var body = new ReqBody()
            {
                renoteId = renoteId,
                i = i
            };
            if (text != null)
            {
                body.Text = text;
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
