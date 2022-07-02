// Copyright (c) ppy Pty Ltd <contact@ppy.sh>, sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Net.Http;
using osu.Framework.IO.Network;

namespace osu.Game.Online.MisskeyAPI.Requests
{
    public class I : APIRequest<MisskeyAPI.Requests.Responses.I>
    {
        public I()
        {
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;
            return req;
        }

        protected override string Target => @"meta";
    }
}
