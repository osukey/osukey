// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Localisation;
using osuTK.Graphics;
using osu.Game.Graphics;
using osu.Game.Resources.Localisation.Web;

namespace osu.Game.Misskey.Users
{
    public abstract partial class UserStatus
    {
        public abstract LocalisableString Message { get; }
        public abstract Color4 GetAppropriateColour(OsuColour colours);
    }

    public partial class UserStatusOnline : UserStatus
    {
        public override LocalisableString Message => UsersStrings.StatusOnline;
        public override Color4 GetAppropriateColour(OsuColour colours) => colours.GreenLight;
    }

    public abstract partial class UserStatusBusy : UserStatusOnline
    {
        public override Color4 GetAppropriateColour(OsuColour colours) => colours.YellowDark;
    }

    public partial class UserStatusOffline : UserStatus
    {
        public override LocalisableString Message => UsersStrings.StatusOffline;
        public override Color4 GetAppropriateColour(OsuColour colours) => Color4.Black;
    }

    public partial class UserStatusDoNotDisturb : UserStatus
    {
        public override LocalisableString Message => "Do not disturb";
        public override Color4 GetAppropriateColour(OsuColour colours) => colours.RedDark;
    }
}
