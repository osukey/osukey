// Copyright (c) sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using osuTK;

namespace osu.Game.Screens.Misskey.Components
{
    public class PopupButton : IconButton
    {
        public PopupButton()
        {
            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            HoverColour = colours.YellowDark.Opacity(0.6f);
            FlashColour = colours.Yellow;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // works with AutoSizeAxes above to make buttons autosize with the scale animation.
            Content.AutoSizeAxes = Axes.None;
            Content.Size = new Vector2(DEFAULT_BUTTON_SIZE);
        }
    }

    // Hou to use
    // private IconButton playlistButton;
    // playlistButton = new PopupButton
    // {
    //     Origin = Anchor.Centre,
    //     Anchor = Anchor.CentreRight,
    //     Position = new Vector2(-bottom_black_area_height / 2, 0),
    //     Icon = FontAwesome.Solid.Bars,
    //     Action = togglePlaylist
    // },

}
