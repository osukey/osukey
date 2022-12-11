// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osuTK.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Cursor;
using osu.Game.Misskey.Overlays.Login;
using osu.Game.Screens.Misskey.Components;
using osuTK;

namespace osu.Game.Screens.Misskey
{
    public partial class MisskeyPost : OsuScreen
    {
        private PostForm form;

        private const float transition_time = 400;

        public override bool HideOverlaysOnEnter => false;

        private Container contentContainer;

        private const float duration = 300;
        private const float button_height = 50;
        private const float button_vertical_margin = 15;

        //private LoginPanel loginPanel;

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
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Black,
                        Alpha = 0.6f,
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Masking = true,
                        AutoSizeDuration = transition_time,
                        AutoSizeEasing = Easing.OutQuint,
                        Children = new Drawable[]
                        {
                            form = new PostForm
                            {
                                Padding = new MarginPadding(10),
                                RequestHide = Hide,
                            },
                            new Box
                            {
                                RelativeSizeAxes = Axes.X,
                                Anchor = Anchor.BottomLeft,
                                Origin = Anchor.BottomLeft,
                                Height = 3,
                                Colour = colours.Yellow,
                                Alpha = 1,
                            },
                        }
                    }
                }
            };
        }
    }
}
