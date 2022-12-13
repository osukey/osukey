// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Game.Beatmaps.Drawables.Cards;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Online;
using osu.Game.Overlays;

namespace osu.Game.Screens.Misskey.Components.Note.Cards
{
    public abstract partial class DrawableNoteCard : OsuClickableContainer
    {
        public const float TRANSITION_DURATION = 400;
        public const float CORNER_RADIUS = 10;

        protected const float WIDTH = 430;

        public IBindable<bool> Expanded { get; }

        public readonly Online.MisskeyAPI.Responses.Types.Note Note;

        // protected readonly Bindable<BeatmapSetFavouriteState> FavouriteState;

        // protected abstract Drawable IdleContent { get; }
        // protected abstract Drawable DownloadInProgressContent { get; }

        // protected readonly BeatmapDownloadTracker DownloadTracker;

        protected DrawableNoteCard(Online.MisskeyAPI.Responses.Types.Note note, bool allowExpansion = true)
            : base(HoverSampleSet.Button)
        {
            Expanded = new BindableBool { Disabled = !allowExpansion };

            Note = note;
            // FavouriteState = new Bindable<BeatmapSetFavouriteState>(new BeatmapSetFavouriteState(note.HasFavourited, note.FavouriteCount));
            // DownloadTracker = new BeatmapDownloadTracker(note);
        }

        [BackgroundDependencyLoader(true)]
        private void load(BeatmapSetOverlay? beatmapSetOverlay)
        {
            // Action = () => beatmapSetOverlay?.FetchAndShowBeatmapSet(Note.OnlineID);

            // AddInternal(DownloadTracker);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // DownloadTracker.State.BindValueChanged(_ => UpdateState());
            Expanded.BindValueChanged(_ => UpdateState(), true);
            FinishTransforms(true);
        }

        protected override bool OnHover(HoverEvent e)
        {
            UpdateState();
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            UpdateState();
            base.OnHoverLost(e);
        }

        protected virtual void UpdateState()
        {
            // bool showProgress = DownloadTracker.State.Value == DownloadState.Downloading || DownloadTracker.State.Value == DownloadState.Importing;

            // IdleContent.FadeTo(showProgress ? 0 : 1, TRANSITION_DURATION, Easing.OutQuint);
            // DownloadInProgressContent.FadeTo(showProgress ? 1 : 0, TRANSITION_DURATION, Easing.OutQuint);
        }

        /// <summary>
        /// Creates a beatmap card of the given <paramref name="size"/> for the supplied <paramref name="note"/>.
        /// </summary>
        public static NoteCard Create(Online.MisskeyAPI.Responses.Types.Note note, BeatmapCardSize size, bool allowExpansion = true)
        {
            switch (size)
            {
                case BeatmapCardSize.Normal:
                    return new NoteCardNormal(note, allowExpansion);

                case BeatmapCardSize.Extra:
                    return new NoteCardNormal(note, allowExpansion); //todo: extra

                default:
                    throw new ArgumentOutOfRangeException(nameof(size), size, @"Unsupported card size");
            }
        }
    }
}
