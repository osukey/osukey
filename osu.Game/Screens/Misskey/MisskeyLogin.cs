// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Overlays.Login;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Misskey
{
    public class MisskeyLogin : OsuScreen
    {
        public override bool HideOverlaysOnEnter => false;

        private Container contentContainer;

        private const float duration = 300;
        private const float button_height = 50;
        private const float button_vertical_margin = 15;

        //private LoginPanel loginPanel;

        [Resolved]
        private OsuGameBase game { get; set; }

        [Resolved]
        private OsuColour colours { get; set; }

        [BackgroundDependencyLoader(true)]
        private void load()
        {
            InternalChild = contentContainer = new Container
            {
                Masking = true,
                CornerRadius = 10,
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(0.5f, 0.4f),
                Children = new Drawable[]
                {
                    new Box
                    {
                        Colour = new Color4(255, 255, 255, 255),
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(0.5f, 0.4f),
                        Anchor = Anchor.Centre,
                    },
                    new Container
                    {
                    }
                }
            };
        }
    }
}
