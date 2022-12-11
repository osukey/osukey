// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Graphics.Sprites;

namespace osu.Game.Misskey.Overlays.Notifications
{
    public partial class SimpleErrorNotification : SimpleNotification
    {
        public override string PopInSampleName => "UI/error-notification-pop-in";

        public SimpleErrorNotification()
        {
            Icon = FontAwesome.Solid.Bomb;
        }
    }
}
