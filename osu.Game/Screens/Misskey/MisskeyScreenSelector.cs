// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using osu.Game.Graphics.Containers;
using osu.Game.Overlays.Dashboard.Friends;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Misskey
{
    public class MisskeyScreenSelector : OsuScreen
    {
        private Box background;
        private Container contentContainer;

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
                Size = new Vector2(0.9f, 0.8f),
                Children = new Drawable[]
                {
                    new Container
                    {
                        Name = "List",
                        // RelativeSizeAxes = Axes.X,
                        Size = new Vector2(1000, 500f),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Children = new Drawable[]
                        {
                            background = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = colours.Gray9
                            },

                            new OsuScrollContainer()
                            {
                                RelativeSizeAxes = Axes.Both,
                                Child = new FillFlowContainer
                                {
                                    Size = new Vector2(1000f),
                                    Children = new Drawable[]
                                    {
                                        new OsuButton()
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Size = new Vector2(500f, 50f),
                                            Text = "MisskeyInstanceSelect",
                                            Action = () => this.Push(new MisskeyInstanceSelect())
                                        },
                                        new OsuButton()
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Size = new Vector2(500f, 50f),
                                            Text = "Welcome",
                                            Action = () => this.Push(new Welcome())
                                        },
                                        new OsuButton()
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Size = new Vector2(500f, 50f),
                                            Text = "Components",
                                            Action = () => this.Push(new MisskeyComponents())
                                        },
                                        new OsuButton()
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Size = new Vector2(500f, 50f),
                                            Text = "MisskeyLogin",
                                            Action = () => this.Push(new MisskeyLogin())
                                        },
                                        new OsuButton()
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Size = new Vector2(500f, 50f),
                                            Text = "MisskeyInstanceSelect",
                                            Action = () => this.Push(new MisskeyInstanceSelect())
                                        },
                                        new OsuButton()
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Size = new Vector2(500f, 50f),
                                            Text = "Welcome",
                                            Action = () => this.Push(new Welcome())
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
