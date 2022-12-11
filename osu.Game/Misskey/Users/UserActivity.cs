// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Online.Rooms;
using osu.Game.Rulesets;
using osuTK.Graphics;

namespace osu.Game.Misskey.Users
{
    public abstract partial class UserActivity
    {
        public abstract string Status { get; }
        public virtual Color4 GetAppropriateColour(OsuColour colours) => colours.GreenDarker;

        public partial class Modding : UserActivity
        {
            public override string Status => "Modding a map";
            public override Color4 GetAppropriateColour(OsuColour colours) => colours.PurpleDark;
        }

        public partial class ChoosingBeatmap : UserActivity
        {
            public override string Status => "Choosing a beatmap";
        }

        public abstract partial class InGame : UserActivity
        {
            public IBeatmapInfo BeatmapInfo { get; }

            public IRulesetInfo Ruleset { get; }

            protected InGame(IBeatmapInfo beatmapInfo, IRulesetInfo ruleset)
            {
                BeatmapInfo = beatmapInfo;
                Ruleset = ruleset;
            }

            public override string Status => Ruleset.CreateInstance().PlayingVerb;
        }

        public partial class InMultiplayerGame : InGame
        {
            public InMultiplayerGame(IBeatmapInfo beatmapInfo, IRulesetInfo ruleset)
                : base(beatmapInfo, ruleset)
            {
            }

            public override string Status => $@"{base.Status} with others";
        }

        public partial class SpectatingMultiplayerGame : InGame
        {
            public SpectatingMultiplayerGame(IBeatmapInfo beatmapInfo, IRulesetInfo ruleset)
                : base(beatmapInfo, ruleset)
            {
            }

            public override string Status => $"Watching others {base.Status.ToLowerInvariant()}";
        }

        public partial class InPlaylistGame : InGame
        {
            public InPlaylistGame(IBeatmapInfo beatmapInfo, IRulesetInfo ruleset)
                : base(beatmapInfo, ruleset)
            {
            }
        }

        public partial class InSoloGame : InGame
        {
            public InSoloGame(IBeatmapInfo beatmapInfo, IRulesetInfo ruleset)
                : base(beatmapInfo, ruleset)
            {
            }
        }

        public partial class Editing : UserActivity
        {
            public IBeatmapInfo BeatmapInfo { get; }

            public Editing(IBeatmapInfo info)
            {
                BeatmapInfo = info;
            }

            public override string Status => @"Editing a beatmap";
        }

        public partial class Spectating : UserActivity
        {
            public override string Status => @"Spectating a game";
        }

        public partial class SearchingForLobby : UserActivity
        {
            public override string Status => @"Looking for a lobby";
        }

        public partial class InLobby : UserActivity
        {
            public override string Status => @"In a lobby";

            public readonly Room Room;

            public InLobby(Room room)
            {
                Room = room;
            }
        }
    }
}
