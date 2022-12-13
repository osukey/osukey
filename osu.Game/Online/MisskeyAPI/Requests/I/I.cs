// Copyright (c) ppy Pty Ltd <contact@ppy.sh>, sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Net.Http;
using Newtonsoft.Json;
using osu.Framework.IO.Network;

namespace osu.Game.Online.MisskeyAPI.Requests
{

    public class User : APIRequest<MisskeyAPI.Responses.Types.User>
    {

        private string i;

        public User(string i)
        {
            this.i = i;
        }

        private class ReqBody
        {
            public string? i;
        };


        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;
            var body = new ReqBody()
            {
                i = i
            };
            var json = JsonConvert.SerializeObject(body);
            req.AddRaw(json);
            return req;
        }

        protected override string Target => @"i";
    }
}
