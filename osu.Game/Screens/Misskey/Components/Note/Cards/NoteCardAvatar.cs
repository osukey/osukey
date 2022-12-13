// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Beatmaps.Drawables;
using osu.Game.Beatmaps.Drawables.Cards;
using osu.Game.Beatmaps.Drawables.Cards.Buttons;
using osu.Game.Graphics;
using osu.Game.Misskey.Users.Drawables;
using osu.Game.Online.API.Requests.Responses;
using osu.Game.Online.MisskeyAPI.Responses.Types;
using osu.Game.Overlays;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Misskey.Components.Note.Cards
{
    public partial class NoteCardAvatar : Container
    {
        public BindableBool Dimmed { get; } = new BindableBool();

        private Drawable avatar;

        private readonly User user;


        public NoteCardAvatar(Online.MisskeyAPI.Responses.Types.Note note)
        {
            this.user = note.User;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            ClickableAvatar internalAvatar;

            Children = new[]
            {
                avatar = new DelayedLoadWrapper(
                    internalAvatar = new ClickableAvatar(user)
                    {
                        RelativeSizeAxes = Axes.Both,
                        Masking = true,
                        CornerRadius = 50,
                        // EdgeEffect = new EdgeEffectParameters
                        // {
                        //     Type = EdgeEffectType.Shadow,
                        //     Radius = 1,
                        //     Colour = Color4.Black.Opacity(0.2f),
                        // },
                    })
                {
                    RelativeSizeAxes = Axes.Both,
                    // Size = new Vector2(HEIGHT - edge_margin * 2, HEIGHT - edge_margin * 2),
                }
            };
            // internalAvatar.OnLoadComplete += d => d.FadeInFromZero(200);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }

        private void updateState()
        {
        }
    }
}
