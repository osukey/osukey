// Copyright (c) sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
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
using osu.Game.Online.API.Requests.Responses;
using osu.Game.Overlays;

namespace osu.Game.Screens.Misskey.Components
{
    public abstract partial class NoteCard : OsuClickableContainer
    {
        public const float TRANSITION_DURATION = 400;
        public const float CORNER_RADIUS = 10;

        protected const float WIDTH = 430;

        public IBindable<bool> Expanded { get; }

        public readonly APIBeatmapSet BeatmapSet;

        protected readonly Bindable<BeatmapSetFavouriteState> FavouriteState;

        protected abstract Drawable IdleContent { get; }
        protected abstract Drawable DownloadInProgressContent { get; }

        protected readonly BeatmapDownloadTracker DownloadTracker;

        protected NoteCard(APIBeatmapSet beatmapSet, bool allowExpansion = true)
            : base(HoverSampleSet.Button)
        {
            Expanded = new BindableBool { Disabled = !allowExpansion };

            BeatmapSet = beatmapSet;
            FavouriteState = new Bindable<BeatmapSetFavouriteState>(new BeatmapSetFavouriteState(beatmapSet.HasFavourited, beatmapSet.FavouriteCount));
            DownloadTracker = new BeatmapDownloadTracker(beatmapSet);
        }

        [BackgroundDependencyLoader(true)]
        private void load(BeatmapSetOverlay? beatmapSetOverlay)
        {
            Action = () => beatmapSetOverlay?.FetchAndShowBeatmapSet(BeatmapSet.OnlineID);

            AddInternal(DownloadTracker);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            DownloadTracker.State.BindValueChanged(_ => UpdateState());
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
            bool showProgress = DownloadTracker.State.Value == DownloadState.Downloading || DownloadTracker.State.Value == DownloadState.Importing;

            IdleContent.FadeTo(showProgress ? 0 : 1, TRANSITION_DURATION, Easing.OutQuint);
            DownloadInProgressContent.FadeTo(showProgress ? 1 : 0, TRANSITION_DURATION, Easing.OutQuint);
        }

        /// <summary>
        /// Creates a beatmap card of the given <paramref name="size"/> for the supplied <paramref name="beatmapSet"/>.
        /// </summary>
        public static BeatmapCard Create(APIBeatmapSet beatmapSet, BeatmapCardSize size, bool allowExpansion = true)
        {
            switch (size)
            {
                case BeatmapCardSize.Normal:
                    return new BeatmapCardNormal(beatmapSet, allowExpansion);

                case BeatmapCardSize.Extra:
                    return new BeatmapCardExtra(beatmapSet, allowExpansion);

                default:
                    throw new ArgumentOutOfRangeException(nameof(size), size, @"Unsupported card size");
            }
        }
    }
}
