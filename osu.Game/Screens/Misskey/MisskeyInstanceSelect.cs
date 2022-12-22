// Copyright (c) ppy Pty Ltd <contact@ppy.sh>, sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using DiffPlex;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osuTK.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Online.MisskeyAPI;
using osu.Game.Online.MisskeyAPI.Requests;
using osu.Game.Online.MisskeyAPI.Requests.Responses;
using osu.Game.Overlays;
using osu.Game.Overlays.Notifications;
using osuTK;
using Meta = osu.Game.Online.MisskeyAPI.Requests.Meta;

namespace osu.Game.Screens.Misskey
{
    public partial class MisskeyInstanceSelect : OsuScreen
    {
        private Container contentContainer;

        private SearchTextBox searchTextBox;
        // private OsuButton submitButton;

        // private string instanceName = String.Empty;

        private OsuSpriteText instanceNameText;
        private OsuSpriteText instanceVersionText;
        private OsuSpriteText instanceDescriptionText;

        [Resolved]
        private IAPIProvider api { get; set; }

        [Resolved]
        private INotificationOverlay notificationOverlay { get; set; }


        [Resolved]
        private OsuColour colours { get; set; }

        [BackgroundDependencyLoader(true)]
        private void load(IAPIProvider api, INotificationOverlay notifications)
        {
            InternalChild = contentContainer = new Container()
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
                        AutoSizeEasing = Easing.OutQuint,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.X,
                                Anchor = Anchor.BottomLeft,
                                Origin = Anchor.BottomLeft,
                                Height = 3,
                                Colour = colours.Yellow,
                                Alpha = 1,
                            },
                            searchTextBox = new SearchTextBox()
                            {
                                RelativeSizeAxes = Axes.X,
                                Origin = Anchor.TopCentre,
                                Anchor = Anchor.TopCentre,
                                Text = "misskey.io"
                            },
                            new OsuButton()
                            {
                                Text = "Submit",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.X,
                                Height = 15f,
                                Width = 0.6f,
                                Action = insetanceFetch,
                            }
                        }
                    },
                    new FillFlowContainer()
                    {
                        X = 10,
                        Children = new Drawable[]
                        {
                            instanceNameText = new OsuSpriteText(),
                            instanceVersionText = new OsuSpriteText(),
                            instanceDescriptionText = new OsuSpriteText()
                        }
                    }
                }
            };

            // insetanceFetch();
            // //
            // searchTextBox.Current.BindValueChanged(_ => insetanceFetch(), true);
        }

        private void insetanceFetch()
        {
            var request = new Meta(searchTextBox.Text);

            request.Success += e =>
            {
                notificationOverlay?.Post(new SimpleNotification
                {
                    Text = e.Name,
                    Icon = FontAwesome.Solid.Check,
                });
                instanceNameText.Text = e.Name;
                instanceVersionText.Text = e.Version;
                instanceDescriptionText.Text = e.Description;
            };

            request.Failure += e =>
            {
                notificationOverlay?.Post(new SimpleNotification
                {
                    Text = e.Message,
                    Icon = FontAwesome.Solid.Times,
                });
            };

            api.Queue(request);
        }
    }
}
