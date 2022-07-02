// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osu.Game.Graphics;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Misskey
{
    public class MisskeyScreenSelector : OsuScreen
    {
        private Container contentContainer;

        [Resolved]
        private OsuGameBase game { get; set; }

        [Resolved]
        private OsuColour colours { get; set; }

        [BackgroundDependencyLoader(true)]
        private void load()
        {
            InternalChild = contentContainer = new Container()
            {
                CornerRadius = 10,
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(0.9f, 0.8f),

                Children = new Drawable[]
                {
                    new Box
                    {
                        Colour = colours.GreySeaFoamDark,
                        RelativeSizeAxes = Axes.Both,
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        //Width = 0.35f,
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        Children = new Drawable[]
                        {
                            new BasicButton()
                            {
                                Text = "MisskeyLogin",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.X,
                                Height = 60f,
                                Width = 0.9f,
                                Action = () => this.Push(new MisskeyLogin())
                            },
                            new BasicButton()
                            {
                                Text = "MisskeyInstanceSelect",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.X,
                                Height = 60f,
                                Width = 0.9f,
                                Action = () => this.Push(new MisskeyInstanceSelect())
                            }
                        }
                    }
                }
            };
        }
    }
}
