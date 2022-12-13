// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osu.Game.Audio;
using osu.Game.Beatmaps.Drawables.Cards;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Online.API;
using osu.Game.Online.API.Requests.Responses;
using osu.Game.Online.MisskeyAPI.Requests.Notes;
using osu.Game.Online.MisskeyAPI.Responses.Types;
using osu.Game.Overlays;
using osu.Game.Overlays.BeatmapListing;
using osu.Game.Resources.Localisation.Web;
using osu.Game.Screens.Misskey.Components;
using osuTK;
using osuTK.Graphics;
using ExpandedContentScrollContainer = osu.Game.Screens.Misskey.Components.Note.Cards.ExpandedContentScrollContainer;
using IAPIProvider = osu.Game.Online.MisskeyAPI.IAPIProvider;
using NoteCard = osu.Game.Screens.Misskey.Components.Note.Cards.NoteCard;

namespace osu.Game.Screens.Misskey
{
    public partial class Timeline : OsuScreen
    {
        // [Cached]
        // protected readonly OsuScrollContainer ScrollFlow;

        protected readonly LoadingLayer Loading;

        [Resolved]
        private PreviewTrackManager previewTrackManager { get; set; }

        [Resolved]
        private IAPIProvider api { get; set; }

        private OsuScrollContainer panelTarget;
        private FillFlowContainer<NoteCard> foundContent;

        private int page = 0;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new FillFlowContainer
            {
                Margin = new MarginPadding(10f),
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    new Container
                    {
                        AutoSizeAxes = Axes.Y,
                        RelativeSizeAxes = Axes.X,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = OsuColour.Gray(0.1f)
                            },
                            panelTarget = new OsuScrollContainer()
                            {
                                Height = 750f,
                                Width = 600f,
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                // RelativeSizeAxes = Axes.X,
                                Masking = true,
                                Padding = new MarginPadding { Horizontal = 20 },
                            }
                        },
                    },
                }
            };
        }

        private string lastNoteID;

        protected override void LoadComplete()
        {
            base.LoadComplete();
            var req = new HybridTimeline(api.AccessToken);
            req.Success += res =>
            {
                onSearchFinished(res);
                lastNoteID = res.Last().Id;
            };
            onSearchStarted();
            api.Queue(req);
        }

        private CancellationTokenSource cancellationToken;

        private Task panelLoadTask;

        private void onSearchStarted()
        {
            cancellationToken?.Cancel();

            // if (panelTarget.Any())
            //     Loading.Show();
        }

        private void onSearchFinished(Note[] searchResult)
        {
            cancellationToken?.Cancel();

            var newCards = createCardsFor(searchResult);

            if (page == 0)
            {
                //No matches case
                if (!newCards.Any())
                {
                    replaceResultsAreaContent(new NotFoundDrawable());
                    return;
                }

                var content = createCardContainerFor(newCards);

                panelLoadTask = LoadComponentAsync(foundContent = content, replaceResultsAreaContent, (cancellationToken = new CancellationTokenSource()).Token);
            }
            else
            {
                // new results may contain beatmaps from a previous page,
                // this is dodgy but matches web behaviour for now.
                // see: https://github.com/ppy/osu-web/issues/9270
                // todo: replace custom equality compraer with ExceptBy in net6.0
                // newCards = newCards.ExceptBy(foundContent.Select(c => c.BeatmapSet.OnlineID), c => c.BeatmapSet.OnlineID);
                // newCards = newCards.Except(foundContent, BeatmapCardEqualityComparer.Default);

                panelLoadTask = LoadComponentsAsync(newCards, loaded =>
                {
                    lastFetchDisplayedTime = Time.Current;
                    foundContent.AddRange(loaded);
                    loaded.ForEach(p => p.FadeIn(200, Easing.OutQuint));
                }, (cancellationToken = new CancellationTokenSource()).Token);
            }
        }

        private IEnumerable<NoteCard> createCardsFor(Note[] notes) =>
            notes.Select(set =>
                NoteCard.Create(set, BeatmapCardSize.Normal).With(c =>
                {
                    c.Anchor = Anchor.TopCentre;
                    c.Origin = Anchor.TopCentre;
                })).ToArray();

        private static ReverseChildIDFillFlowContainer<NoteCard> createCardContainerFor(IEnumerable<NoteCard> newCards)
        {
            // spawn new children with the contained so we only clear old content at the last moment.
            // reverse ID flow is required for correct Z-ordering of the cards' expandable content (last card should be front-most).
            var content = new ReverseChildIDFillFlowContainer<NoteCard>
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Spacing = new Vector2(10),
                Alpha = 0,
                Margin = new MarginPadding
                {
                    Top = 15,
                    // the + 20 adjustment is roughly eyeballed in order to fit all of the expanded content height after it's scaled
                    // as well as provide visual balance to the top margin.
                    Bottom = ExpandedContentScrollContainer.HEIGHT + 20
                },
                ChildrenEnumerable = newCards
            };
            return content;
        }

        private void replaceResultsAreaContent(Drawable content)
        {
            // Loading.Hide();
            lastFetchDisplayedTime = Time.Current;

            panelTarget.Child = content;

            content.FadeInFromZero();
        }

        protected override void Dispose(bool isDisposing)
        {
            cancellationToken?.Cancel();
            base.Dispose(isDisposing);
        }

        public partial class NotFoundDrawable : CompositeDrawable
        {
            public NotFoundDrawable()
            {
                RelativeSizeAxes = Axes.X;
                Height = 250;
                Alpha = 0;
                Margin = new MarginPadding { Top = 15 };
            }

            [BackgroundDependencyLoader]
            private void load(LargeTextureStore textures)
            {
                AddInternal(new FillFlowContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(10, 0),
                    Children = new Drawable[]
                    {
                        new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            FillMode = FillMode.Fit,
                            Texture = textures.Get(@"Online/not-found")
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = BeatmapsStrings.ListingSearchNotFoundQuote,
                        }
                    }
                });
            }
        }

        // TODO: localisation requires Text/LinkFlowContainer support for localising strings with links inside
        // (https://github.com/ppy/osu-framework/issues/4530)

        private const double time_between_fetches = 500;

        private double lastFetchDisplayedTime;

        protected override void Update()
        {
            base.Update();

            const int pagination_scroll_distance = 200;

            bool shouldShowMore = panelLoadTask?.IsCompleted != false
                                  && Time.Current - lastFetchDisplayedTime > time_between_fetches
                                  && (panelTarget.ScrollableExtent > 0 && panelTarget.IsScrolledToEnd(pagination_scroll_distance));

            if (shouldShowMore)
                FetchNextPage();
        }

        private bool locked;
        private void FetchNextPage()
        {
            if (locked)
                return;
            var req = new HybridTimeline(api.AccessToken, lastNoteID);
            req.Success += res =>
            {
                lastNoteID = res.Last().Id;
                page++;
                onSearchFinished(res);
                locked = false;
            };
            api.Queue(req);
            locked = true;
        }

        private class BeatmapCardEqualityComparer : IEqualityComparer<BeatmapCard>
        {
            public static BeatmapCardEqualityComparer Default { get; } = new BeatmapCardEqualityComparer();

            public bool Equals(BeatmapCard x, BeatmapCard y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;

                return x.BeatmapSet.Equals(y.BeatmapSet);
            }

            public int GetHashCode(BeatmapCard obj) => obj.BeatmapSet.GetHashCode();
        }
    }
}
