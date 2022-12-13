// Copyright (c) sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osu.Game.Graphics.Containers;
using osu.Game.Online.API.Requests.Responses;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace osu.Game.Screens.Misskey.Components
{

    [LongRunningLoad]
    public partial class Avatar : Sprite
    {
        // private readonly APIUser user;
        private Online.MisskeyAPI.Responses.Types.Note note;

        /// <summary>
        /// A simple, non-interactable avatar sprite for the specified user.
        /// </summary>
        ///// <param name="user">The user. A null value will get a placeholder avatar.</param>
        public Avatar(Online.MisskeyAPI.Responses.Types.Note note)
        {
            // this.user = user;
            // RelativeSizeAxes = Axes.Both;
            Size = new Vector2(10f);
            FillMode = FillMode.Fit;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            this.note = note;
        }

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore textures)
        {
            // if (user != null && user.Id > 1)
            //     // TODO: The fallback here should not need to exist. Users should be looked up and populated via UserLookupCache or otherwise
            //     // in remaining cases where this is required (chat tabs, local leaderboard), at which point this should be removed.
            //     Texture = textures.Get(user.AvatarUrl ?? $@"https://a.ppy.sh/{user.Id}");

            Texture ??= textures.Get(note.User.AvatarUrl);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(300, Easing.OutQuint);
        }
    }
}
