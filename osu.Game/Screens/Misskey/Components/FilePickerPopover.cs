// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Database;
using osu.Game.Extensions;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using osu.Game.Graphics.UserInterfaceV2;
using osu.Game.Localisation;
using osu.Game.Overlays;
using osu.Game.Overlays.Mods;
using osu.Game.Rulesets;
using osu.Game.Rulesets.Mods;
using osuTK;

namespace osu.Game.Screens.Misskey.Components
{
    internal partial class FilePickerPopover : OsuPopover
    {
        private OsuFileSelector? fileSelector;
        private readonly string choosePath;
        private readonly string[] allowedExtensions;
        private readonly Bindable<FileInfo?> currentFile;

        [Resolved]
        private Bindable<RulesetInfo> ruleset { get; set; } = null!;

        [Resolved]
        private Bindable<IReadOnlyList<Mod>> selectedMods { get; set; } = null!;

        [Resolved]
        private RealmAccess realm { get; set; } = null!;

        public FilePickerPopover(string choosePath, string[] allowedExtensions, Bindable<FileInfo?> file)
        {
            this.choosePath = choosePath;
            this.allowedExtensions = allowedExtensions;
            this.currentFile = file;

            Child = new Container
            {
                Size = new Vector2(600, 400),
                Children = new Drawable[]
                {
                    fileSelector = new OsuFileSelector(choosePath, allowedExtensions)
                    {
                        RelativeSizeAxes = Axes.Both,
                        CurrentFile = { BindTarget = currentFile }
                    },
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Body.BorderThickness = 3;
            Body.BorderColour = colours.Orange1;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

        }


        protected override void UpdateState(ValueChangedEvent<Visibility> state)
        {
            base.UpdateState(state);
        }
    }
}
