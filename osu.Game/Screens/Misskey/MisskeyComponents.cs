// Copyright (c) sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Overlays.Login;
using osu.Game.Screens.Misskey.Components;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Misskey
{
    public class MisskeyComponents : OsuScreen
    {
        private Container contentContainer;
        private Drawable avatar;

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
                Size = new Vector2(0.3f),
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
                        // RelativeSizeAxes = Axes.Both,
                        // AutoSizeAxes = Axes.Y,
                        Size = new Vector2(200),
                        Masking = true,
                        CornerRadius = 100,
                        AutoSizeEasing = Easing.OutQuint,
                        Children = new Drawable[]
                        {
                            avatar = new DelayedLoadWrapper(
                                new Avatar()
                                {
                                    RelativeSizeAxes = Axes.Both,
                                })
                        }
                    }
                }
            };
        }
    }
}
