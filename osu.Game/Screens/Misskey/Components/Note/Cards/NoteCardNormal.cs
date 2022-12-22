// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Beatmaps.Drawables.Cards;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Online.MisskeyAPI;
using osu.Game.Online.MisskeyAPI.Requests.Notes;
using osu.Game.Online.MisskeyAPI.Requests.Reactions;
using osu.Game.Overlays;
using osu.Game.Overlays.BeatmapSet;
using osu.Game.Overlays.Notifications;
using osu.Game.Overlays.Profile.Header.Components;
using osu.Game.Resources.Localisation.Web;
using osu.Game.Screens.Misskey.Components.Note.Cards.Statistics;
using osu.Game.Skinning.Components;
using osuTK;

namespace osu.Game.Screens.Misskey.Components.Note.Cards
{
    public partial class NoteCardNormal : NoteCard
    {
        protected override Drawable IdleContent => idleBottomContent;
        protected override Drawable DownloadInProgressContent => downloadProgressBar;

        private const float height = 120;

        [Cached]
        private readonly NoteCardContent content;

        private NoteCardAvatar thumbnail = null!;
        // private CollapsibleButtonContainer buttonContainer = null!;

        // private FillFlowContainer<BeatmapCardStatistic> statisticsContainer = null!;
        private TextFlowContainer noteText = null!;

        private FillFlowContainer idleBottomContent = null!;
        private BeatmapCardDownloadProgressBar downloadProgressBar = null!;

        private IconButton replyButton = null!;
        private IconButton renoteButton = null!;
        private IconButton reactButton = null!;
        private IconButton menuButton = null!;

        [Resolved]
        private IAPIProvider api { get; set; } = null!;

        [Resolved(CanBeNull = true)]
        private INotificationOverlay? notifications { get; set; }

        // [Resolved]
        // private OverlayColourProvider colourProvider { get; set; } = null!;

        public NoteCardNormal(Online.MisskeyAPI.Responses.Types.Note note, bool allowExpansion = true)
            : base(note, allowExpansion)
        {
            content = new NoteCardContent(height);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Width = WIDTH;
            Height = height;

            FillFlowContainer leftIconArea = null!;
            FillFlowContainer titleBadgeArea = null!;
            GridContainer artistContainer = null!;

            // LinkFlowContainer titleText = null!;
            LinkFlowContainer artistText = null!;

            Child = content.With(c =>
            {
                c.MainContent = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        thumbnail = new NoteCardAvatar(Note)
                        {
                            Name = @"Left (icon) area",
                            Size = new Vector2(Width * 0.2f),
                            // Padding = new MarginPadding { Right = CORNER_RADIUS },
                            Child = leftIconArea = new FillFlowContainer
                            {
                                Margin = new MarginPadding(5),
                                AutoSizeAxes = Axes.Both,
                                Direction = FillDirection.Horizontal,
                                Spacing = new Vector2(1)
                            }
                        },
                        // buttonContainer = new CollapsibleButtonContainer(Note)
                        // {
                        //     X = height - CORNER_RADIUS,
                        //     Width = WIDTH - height + CORNER_RADIUS,
                        //     // FavouriteState = { BindTarget = FavouriteState },
                        //     ButtonsCollapsedWidth = CORNER_RADIUS,
                        //     ButtonsExpandedWidth = 30,
                        //     Children = new Drawable[]
                        //     {
                        new FillFlowContainer
                        {
                            // X = height - CORNER_RADIUS,
                            X = height - 30,
                            RelativeSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Children = new Drawable[]
                            {
                                new GridContainer
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    ColumnDimensions = new[]
                                    {
                                        new Dimension(),
                                        new Dimension(GridSizeMode.AutoSize),
                                    },
                                    RowDimensions = new[]
                                    {
                                        new Dimension(GridSizeMode.AutoSize)
                                    },
                                    Content = new[]
                                    {
                                        new Drawable[]
                                        {
                                            new OsuSpriteText()
                                            {
                                                Text = new RomanisableString(Note.User.Name, Note.User.Username),
                                                Font = OsuFont.Default.With(size: 22.5f, weight: FontWeight.SemiBold),
                                                RelativeSizeAxes = Axes.X,
                                                Truncate = true
                                                // Padding = new MarginPadding { Left = SettingsPanel.CONTENT_MARGINS },
                                                // AutoSizeAxes = Axes.Y,
                                            },
                                            titleBadgeArea = new FillFlowContainer
                                            {
                                                Anchor = Anchor.BottomRight,
                                                Origin = Anchor.BottomRight,
                                                AutoSizeAxes = Axes.Both,
                                                Direction = FillDirection.Horizontal,
                                            }
                                        }
                                    }
                                },
                                artistContainer = new GridContainer
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    ColumnDimensions = new[]
                                    {
                                        new Dimension(),
                                        new Dimension(GridSizeMode.AutoSize)
                                    },
                                    RowDimensions = new[]
                                    {
                                        new Dimension(GridSizeMode.AutoSize)
                                    },
                                    Content = new[]
                                    {
                                        new[]
                                        {
                                            artistText = new LinkFlowContainer()
                                            {
                                                // Text = createArtistText(),
                                                // Font = OsuFont.Default.With(size: 17.5f, weight: FontWeight.SemiBold),
                                                RelativeSizeAxes = Axes.X,
                                                // Truncate = true
                                                AutoSizeAxes = Axes.Y,
                                            },
                                            Empty()
                                        },
                                    }
                                },
                                noteText = new TextFlowContainer
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Alpha = 1,
                                    AlwaysPresent = true,
                                }.With(flow =>
                                {
                                    flow.AddText(Note.Text, t => t.Font = OsuFont.Default.With(size: 15));
                                }),
                                new GridContainer()
                                {
                                    Name = @"Bottom content",
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    // Anchor = Anchor.BottomLeft,
                                    // Origin = Anchor.BottomLeft,
                                    // Margin = new MarginPadding { Top = 10, Left = 90 },
                                    // Y = Y + 10f,
                                    ColumnDimensions = new[]
                                    {
                                        new Dimension(GridSizeMode.AutoSize),
                                        new Dimension(GridSizeMode.AutoSize),
                                        new Dimension(GridSizeMode.AutoSize),
                                        new Dimension(GridSizeMode.AutoSize),
                                    },
                                    RowDimensions = new[]
                                    {
                                        new Dimension(GridSizeMode.AutoSize)
                                    },
                                    Content = new[]
                                    {
                                        new Drawable[]
                                        {
                                            replyButton = new IconButton()
                                            {
                                                Icon = FontAwesome.Solid.Reply,
                                            },
                                            renoteButton = new IconButton()
                                            {
                                                Icon = FontAwesome.Solid.Retweet,
                                                Action = () =>
                                                {
                                                    var req = new CreateRenote(api.AccessToken, Note.Id);
                                                    req.Success += _ =>
                                                    {
                                                        notifications?.Post(new SimpleNotification
                                                        {
                                                            Text = "Renoteしました",
                                                            Icon = FontAwesome.Solid.PaperPlane,
                                                        });
                                                    };
                                                    req.Failure += _ =>
                                                    {
                                                        notifications?.Post(new SimpleNotification
                                                        {
                                                            Text = "Renoteに失敗しました",
                                                            Icon = FontAwesome.Solid.ExclamationTriangle,
                                                        });
                                                    };
                                                    api.Queue(req);
                                                },
                                            },
                                            reactButton = new IconButton()
                                            {
                                                Icon = FontAwesome.Solid.Plus,
                                                Action = () =>
                                                {
                                                    var req = new CreateReaction(api.AccessToken, Note.Id, "👍");
                                                    req.Success += _ =>
                                                    {
                                                        notifications?.Post(new SimpleNotification
                                                        {
                                                            Text = "リアクションしました",
                                                            Icon = FontAwesome.Solid.PaperPlane,
                                                        });
                                                    };
                                                    req.Failure += _ =>
                                                    {
                                                        notifications?.Post(new SimpleNotification
                                                        {
                                                            Text = "リアクションに失敗しました",
                                                            Icon = FontAwesome.Solid.ExclamationTriangle,
                                                        });
                                                    };
                                                    api.Queue(req);
                                                },
                                            },
                                            menuButton = new IconButton()
                                            {
                                                Icon = FontAwesome.Solid.EllipsisH,
                                            },
                                        }
                                    }
                                    // }
                                }
                            }
                        },

                        //     }
                        // }
                    }
                };
                // c.ExpandedContent = new Container
                // {
                //     RelativeSizeAxes = Axes.X,
                //     AutoSizeAxes = Axes.Y,
                //     Padding = new MarginPadding { Horizontal = 10, Vertical = 13 },
                //     Child = new BeatmapCardDifficultyList(BeatmapSet)
                // };
                c.Expanded.BindTarget = Expanded;
                // titleText.AddLink(Note.User.Name, $"{api.APIEndpointUrl}@{Note.User.Username}@{Note.User.Host}");
                artistText.AddLink($"@{Note.User.Username}@{Note.User.Host}", $"{api.APIEndpointUrl}/@{Note.User.Username}@{Note.User.Host}");
            });

            // if (BeatmapSet.HasVideo)
            //     leftIconArea.Add(new VideoIconPill { IconSize = new Vector2(20) });
            //
            // if (BeatmapSet.HasStoryboard)
            //     leftIconArea.Add(new StoryboardIconPill { IconSize = new Vector2(20) });
            //
            // if (BeatmapSet.FeaturedInSpotlight)
            // {
            //     titleBadgeArea.Add(new SpotlightBeatmapBadge
            //     {
            //         Anchor = Anchor.BottomRight,
            //         Origin = Anchor.BottomRight,
            //         Margin = new MarginPadding { Left = 5 }
            //     });
            // }

            // if (BeatmapSet.HasExplicitContent)
            // {
            //     titleBadgeArea.Add(new ExplicitContentBeatmapBadge
            //     {
            //         Anchor = Anchor.BottomRight,
            //         Origin = Anchor.BottomRight,
            //         Margin = new MarginPadding { Left = 5 }
            //     });
            // }

            // if (BeatmapSet.TrackId != null)
            // {
            //     artistContainer.Content[0][1] = new FeaturedArtistBeatmapBadge
            //     {
            //         Anchor = Anchor.BottomRight,
            //         Origin = Anchor.BottomRight,
            //         Margin = new MarginPadding { Left = 5 }
            //     };
            // }
        }

        private LocalisableString createArtistText()
        {
            var romanisableArtist = new RomanisableString(Note.User.Username + "@" + Note.User.Host, Note.User.Username + "@" + Note.User.Host);
            return romanisableArtist;
        }

        // private IEnumerable<BeatmapCardStatistic> createStatistics()
        // {
        //     var hypesStatistic = HypesStatistic.CreateFor(BeatmapSet);
        //     if (hypesStatistic != null)
        //         yield return hypesStatistic;
        //
        //     var nominationsStatistic = NominationsStatistic.CreateFor(BeatmapSet);
        //     if (nominationsStatistic != null)
        //         yield return nominationsStatistic;
        //
        //     yield return new FavouritesStatistic(BeatmapSet) { Current = FavouriteState };
        //     yield return new PlayCountStatistic(BeatmapSet);
        //
        //     var dateStatistic = BeatmapCardDateStatistic.CreateFor(BeatmapSet);
        //     if (dateStatistic != null)
        //         yield return dateStatistic;
        // }

        protected override void UpdateState()
        {
            base.UpdateState();

            bool showDetails = IsHovered || Expanded.Value;

            // buttonContainer.ShowDetails.Value = showDetails;
            thumbnail.Dimmed.Value = showDetails;

            // statisticsContainer.FadeTo(showDetails ? 1 : 0, TRANSITION_DURATION, Easing.OutQuint);
        }
    }
}
