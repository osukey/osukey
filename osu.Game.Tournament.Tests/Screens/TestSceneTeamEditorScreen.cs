// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Misskey.Screens.Editors;

namespace osu.Game.Misskey.Tests.Screens
{
    public class TestSceneTeamEditorScreen : TournamentTestScene
    {
        public TestSceneTeamEditorScreen()
        {
            Add(new TeamEditorScreen
            {
                Width = 0.85f // create room for control panel
            });
        }
    }
}
