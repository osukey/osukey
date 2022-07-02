// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace osu.Game.Online.MisskeyAPI.Requests.Responses
{
    public class UsersShow
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Emoji
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public class Field
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class File
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("createdAt")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("md5")]
            public string Md5 { get; set; }

            [JsonProperty("size")]
            public int Size { get; set; }

            [JsonProperty("isSensitive")]
            public bool IsSensitive { get; set; }

            [JsonProperty("blurhash")]
            public object Blurhash { get; set; }

            [JsonProperty("properties")]
            public Properties Properties { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("thumbnailUrl")]
            public object ThumbnailUrl { get; set; }

            [JsonProperty("comment")]
            public object Comment { get; set; }

            [JsonProperty("folderId")]
            public object FolderId { get; set; }

            [JsonProperty("folder")]
            public object Folder { get; set; }

            [JsonProperty("userId")]
            public object UserId { get; set; }

            [JsonProperty("user")]
            public object User { get; set; }
        }

        public class Integrations
        {
        }

        public class PinnedNote
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("createdAt")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("userId")]
            public string UserId { get; set; }

            [JsonProperty("user")]
            public User User { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("cw")]
            public object Cw { get; set; }

            [JsonProperty("visibility")]
            public string Visibility { get; set; }

            [JsonProperty("renoteCount")]
            public int RenoteCount { get; set; }

            [JsonProperty("repliesCount")]
            public int RepliesCount { get; set; }

            [JsonProperty("reactions")]
            public Reactions Reactions { get; set; }

            [JsonProperty("emojis")]
            public List<Emoji> Emojis { get; set; }

            [JsonProperty("fileIds")]
            public List<string> FileIds { get; set; }

            [JsonProperty("files")]
            public List<File> Files { get; set; }

            [JsonProperty("replyId")]
            public object ReplyId { get; set; }

            [JsonProperty("renoteId")]
            public object RenoteId { get; set; }
        }

        public class Properties
        {
        }

        public class Reactions
        {
            [JsonProperty(":thinkhappy@miss.nem.one:")]
            public int ThinkhappyMissNemOne { get; set; }
        }

        public class Root
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("host")]
            public object Host { get; set; }

            [JsonProperty("avatarUrl")]
            public string AvatarUrl { get; set; }

            [JsonProperty("avatarBlurhash")]
            public string AvatarBlurhash { get; set; }

            [JsonProperty("avatarColor")]
            public object AvatarColor { get; set; }

            [JsonProperty("isAdmin")]
            public bool IsAdmin { get; set; }

            [JsonProperty("isModerator")]
            public bool IsModerator { get; set; }

            [JsonProperty("isBot")]
            public bool IsBot { get; set; }

            [JsonProperty("isCat")]
            public bool IsCat { get; set; }

            [JsonProperty("emojis")]
            public List<Emoji> Emojis { get; set; }

            [JsonProperty("onlineStatus")]
            public string OnlineStatus { get; set; }

            [JsonProperty("url")]
            public object Url { get; set; }

            [JsonProperty("uri")]
            public object Uri { get; set; }

            [JsonProperty("createdAt")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("updatedAt")]
            public DateTime UpdatedAt { get; set; }

            [JsonProperty("lastFetchedAt")]
            public object LastFetchedAt { get; set; }

            [JsonProperty("bannerUrl")]
            public string BannerUrl { get; set; }

            [JsonProperty("bannerBlurhash")]
            public string BannerBlurhash { get; set; }

            [JsonProperty("bannerColor")]
            public object BannerColor { get; set; }

            [JsonProperty("isLocked")]
            public bool IsLocked { get; set; }

            [JsonProperty("isSilenced")]
            public bool IsSilenced { get; set; }

            [JsonProperty("isSuspended")]
            public bool IsSuspended { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("location")]
            public string Location { get; set; }

            [JsonProperty("birthday")]
            public string Birthday { get; set; }

            [JsonProperty("lang")]
            public object Lang { get; set; }

            [JsonProperty("fields")]
            public List<Field> Fields { get; set; }

            [JsonProperty("followersCount")]
            public int FollowersCount { get; set; }

            [JsonProperty("followingCount")]
            public int FollowingCount { get; set; }

            [JsonProperty("notesCount")]
            public int NotesCount { get; set; }

            [JsonProperty("pinnedNoteIds")]
            public List<string> PinnedNoteIds { get; set; }

            [JsonProperty("pinnedNotes")]
            public List<PinnedNote> PinnedNotes { get; set; }

            [JsonProperty("pinnedPageId")]
            public object PinnedPageId { get; set; }

            [JsonProperty("pinnedPage")]
            public object PinnedPage { get; set; }

            [JsonProperty("publicReactions")]
            public bool PublicReactions { get; set; }

            [JsonProperty("ffVisibility")]
            public string FfVisibility { get; set; }

            [JsonProperty("twoFactorEnabled")]
            public bool TwoFactorEnabled { get; set; }

            [JsonProperty("usePasswordLessLogin")]
            public bool UsePasswordLessLogin { get; set; }

            [JsonProperty("securityKeys")]
            public bool SecurityKeys { get; set; }

            [JsonProperty("avatarId")]
            public string AvatarId { get; set; }

            [JsonProperty("bannerId")]
            public string BannerId { get; set; }

            [JsonProperty("injectFeaturedNote")]
            public bool InjectFeaturedNote { get; set; }

            [JsonProperty("receiveAnnouncementEmail")]
            public bool ReceiveAnnouncementEmail { get; set; }

            [JsonProperty("alwaysMarkNsfw")]
            public bool AlwaysMarkNsfw { get; set; }

            [JsonProperty("carefulBot")]
            public bool CarefulBot { get; set; }

            [JsonProperty("autoAcceptFollowed")]
            public bool AutoAcceptFollowed { get; set; }

            [JsonProperty("noCrawle")]
            public bool NoCrawle { get; set; }

            [JsonProperty("isExplorable")]
            public bool IsExplorable { get; set; }

            [JsonProperty("isDeleted")]
            public bool IsDeleted { get; set; }

            [JsonProperty("hideOnlineStatus")]
            public bool HideOnlineStatus { get; set; }

            [JsonProperty("hasUnreadSpecifiedNotes")]
            public bool HasUnreadSpecifiedNotes { get; set; }

            [JsonProperty("hasUnreadMentions")]
            public bool HasUnreadMentions { get; set; }

            [JsonProperty("hasUnreadAnnouncement")]
            public bool HasUnreadAnnouncement { get; set; }

            [JsonProperty("hasUnreadAntenna")]
            public bool HasUnreadAntenna { get; set; }

            [JsonProperty("hasUnreadChannel")]
            public bool HasUnreadChannel { get; set; }

            [JsonProperty("hasUnreadMessagingMessage")]
            public bool HasUnreadMessagingMessage { get; set; }

            [JsonProperty("hasUnreadNotification")]
            public bool HasUnreadNotification { get; set; }

            [JsonProperty("hasPendingReceivedFollowRequest")]
            public bool HasPendingReceivedFollowRequest { get; set; }

            [JsonProperty("integrations")]
            public Integrations Integrations { get; set; }

            [JsonProperty("mutedWords")]
            public List<object> MutedWords { get; set; }

            [JsonProperty("mutedInstances")]
            public List<object> MutedInstances { get; set; }

            [JsonProperty("mutingNotificationTypes")]
            public List<object> MutingNotificationTypes { get; set; }

            [JsonProperty("emailNotificationTypes")]
            public List<string> EmailNotificationTypes { get; set; }

            [JsonProperty("showTimelineReplies")]
            public bool ShowTimelineReplies { get; set; }
        }

        public class User
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("host")]
            public object Host { get; set; }

            [JsonProperty("avatarUrl")]
            public string AvatarUrl { get; set; }

            [JsonProperty("avatarBlurhash")]
            public string AvatarBlurhash { get; set; }

            [JsonProperty("avatarColor")]
            public object AvatarColor { get; set; }

            [JsonProperty("isAdmin")]
            public bool IsAdmin { get; set; }

            [JsonProperty("isCat")]
            public bool IsCat { get; set; }

            [JsonProperty("emojis")]
            public List<Emoji> Emojis { get; set; }

            [JsonProperty("onlineStatus")]
            public string OnlineStatus { get; set; }
        }
    }
}
