// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Extensions.LocalisationExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Online.MisskeyAPI;
using osu.Game.Misskey.Overlays.Settings;
using osu.Game.Online.MisskeyAPI.Requests.Notes;
using osu.Game.Overlays;
using osu.Game.Overlays.Notifications;
using osu.Game.Overlays.OSD;
using osu.Game.Resources.Localisation.Web;
using osu.Game.Screens.Misskey;
using osuTK;

namespace osu.Game.Screens.Misskey.Components
{
    public partial class PostForm : FillFlowContainer
    {
        private OnScreenDisplay? onScreenDisplay { get; set; }
        [Resolved(CanBeNull = true)]
        private INotificationOverlay? notifications { get; set; }
        private partial class ResToast : Toast
        {
            public ResToast(string value, string desc)
                : base("Info", value, desc)
            {
            }
        }

        private TextBox cwBox = null!;
        private TextBox textBox = null!;
        private ShakeContainer shakeSignIn = null!;

        [Resolved]
        private IAPIProvider api { get; set; } = null!;

        public Action? RequestHide;

        private void post()
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                shakeSignIn.Shake();
                return;
            }

            var createReq = new Create(api.AccessToken, textBox.Text);

            createReq.Success += _ =>
            {
                textBox.Text = string.Empty;
                cwBox.Text = string.Empty;
                notifications?.Post(new SimpleNotification
                {
                    Text = "投稿しました",
                    Icon = FontAwesome.Solid.Check,
                });
            };

            api.Queue(createReq);
            notifications?.Post(new SimpleNotification
            {
                Text = "送信しています",
                Icon = FontAwesome.Solid.PaperPlane,
            });

        }

        [BackgroundDependencyLoader(permitNulls: true)]
        private void load(OsuConfigManager config, AccountCreationOverlay accountCreation)
        {
            Direction = FillDirection.Vertical;
            Spacing = new Vector2(0, 5);
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            ErrorTextFlowContainer errorText;
            // LinkFlowContainer forgottenPaswordLink;

            Children = new Drawable[]
            {
                cwBox = new OsuTextBox
                {
                    PlaceholderText = "CW",
                    RelativeSizeAxes = Axes.X,
                    TabbableContentContainer = this
                },
                textBox = new OsuTextBox
                {
                    PlaceholderText = "お気持ち表明してください",
                    RelativeSizeAxes = Axes.X,
                    TabbableContentContainer = this,
                },
                errorText = new ErrorTextFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                },
                // new SettingsCheckbox
                // {
                //     LabelText = "Remember username",
                //     Current = config.GetBindable<bool>(OsuSetting.SaveUsername),
                // },
                // new SettingsCheckbox
                // {
                //     LabelText = "Stay signed in",
                //     Current = config.GetBindable<bool>(OsuSetting.SavePassword),
                // },
                // forgottenPaswordLink = new LinkFlowContainer
                // {
                //     Padding = new MarginPadding { Left = SettingsPanel.CONTENT_MARGINS },
                //     RelativeSizeAxes = Axes.X,
                //     AutoSizeAxes = Axes.Y,
                // },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        shakeSignIn = new ShakeContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Child = new SettingsButton
                            {
                                Text = "ノート",
                                Action = () => post()
                            },
                        }
                    }
                },
                new SettingsButton
                {
                    Text = "test",
                    Action = () =>
                    {
                        onScreenDisplay?.Display(new ResToast("送信しています", ""));
                    }
                }
            };

            // forgottenPaswordLink.AddLink(LayoutStrings.PopupLoginLoginForgot, $"https://simkey.net/about");

            textBox.OnCommit += (_, _) => post();

            if (api.LastLoginError?.Message is string error)
                errorText.AddErrors(new[] { error });
        }

        public override bool AcceptsFocus => true;

        protected override bool OnClick(ClickEvent e) => true;

        // protected override void OnFocus(FocusEvent e)
        // {
        //     Schedule(() => { GetContainingInputManager().ChangeFocus(string.IsNullOrEmpty(username.Text) ? username : password); });
        // }
    }
}
