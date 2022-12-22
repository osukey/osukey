// Copyright (c) sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using osu.Framework.IO.Network;
using osu.Framework.Logging;
using osu.Game.Online.MisskeyAPI.Requests.Responses;

namespace osu.Game.Online.MisskeyAPI.Requests.Reactions
{
    public class CreateReaction : APIRequest<NullResponse>
    {
        private string noteId;
        private string i;
        private string reaction;

        public CreateReaction(string i, string noteId, string reaction)
        {
            this.i = i;
            this.noteId = noteId;
            this.reaction = reaction;
        }
        private class ReqBody
        {
            public string? noteId;
            public string? i;
            public string? reaction;
        };
        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;
            var body = new ReqBody()
            {
                noteId = noteId,
                i = i,
                reaction = reaction
            };
            var json = JsonConvert.SerializeObject(body);
            Logger.Log(json, LoggingTarget.Network, LogLevel.Debug);
            req.AddRaw(json);

            return req;
        }

        protected override string Target => @"notes/reactions/create";
        // protected override string Uri => @"https://apicheck.sim1222.workers.dev/notes/create";
    }
}
