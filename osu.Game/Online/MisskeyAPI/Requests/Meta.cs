// Copyright (c) ppy Pty Ltd <contact@ppy.sh>, sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Net.Http;
using osu.Framework.IO.Network;
using osu.Game.Online.MisskeyAPI.Responses.Types;

namespace osu.Game.Online.MisskeyAPI.Requests
{
    public class Meta : APIRequest<InstanceMetadata>
    {

        private readonly string endpoint;
        public Meta(string endpoint)
        {
            this.endpoint = endpoint;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;
            return req;
        }

        protected override string Uri => $@"https://{endpoint}/api/meta";
        protected override string Target => @"";
    }
}
