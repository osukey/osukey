// Copyright (c) sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace osu.Game.Online.MisskeyAPI.Responses.Types
{
    // ID = string
    // DateString = string

    public class CustomEmoji // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L248
    {
        [JsonProperty("id")]
        [CanBeNull]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("category")]
        [CanBeNull]
        public string Category { get; set; }

        [JsonProperty("aliases")]
        [ItemCanBeNull]
        public string[] Aliases { get; set; }
    }

    public class DriveFile // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L109
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        [CanBeNull]
        public string CreatedAt { get; set; }

        [JsonProperty("isSensitive")]
        public bool IsSensitive { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("blurhash")]
        public string Blurhash { get; set; }

        [JsonProperty("comment")]
        [CanBeNull]
        public string Comment { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string, string> Properties { get; set; }
    }

    public class Poll // for Note
    {
        public class Choice
        {
            [JsonProperty("isVoted")]
            public bool Id { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("votes")]
            public int Votes { get; set; }
        }

        [JsonProperty("expiresAt")]
        [CanBeNull]
        public string ExpiresAt { get; set; }

        [JsonProperty("multiple")]
        public bool Multiple { get; set; }

        [JsonProperty("choices")]
        public Choice Choices { get; set; }
    }

    public class Note // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L128
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("text")]
        [CanBeNull]
        public string Text { get; set; }

        [JsonProperty("cw")]
        [CanBeNull]
        public string Cw { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("reply")]
        [CanBeNull]
        public Note Reply { get; set; }

        [JsonProperty("replyId")]
        [CanBeNull]
        public string ReplyId { get; set; }

        [JsonProperty("renote")]
        [CanBeNull]
        public Note Renote { get; set; }

        [JsonProperty("renoteId")]
        [CanBeNull]
        public string RenoteId { get; set; }

        [JsonProperty("files")]
        public DriveFile[] Files { get; set; }

        [JsonProperty("fileIds")]
        public string[] FileIds { get; set; }

        [JsonProperty("visibility")] // 'public' | 'home' | 'followers' | 'specified'
        public string Visibility { get; set; }

        [JsonProperty("visibleUserIds")]
        public string[] VisibleUserIds { get; set; }

        [JsonProperty("localOnly")]
        public bool? LocalOnly { get; set; }

        [JsonProperty("myReaction")]
        [CanBeNull]
        public string MyReaction { get; set; }

        [JsonProperty("reactions")]
        public Dictionary<string, int> Reactions { get; set; }

        [JsonProperty("renoteCount")]
        public int RenoteCount { get; set; }

        [JsonProperty("repliesCount")]
        public int RepliesCount { get; set; }

        [JsonProperty("poll")]
        [CanBeNull]
        public Poll Poll { get; set; }

        [JsonProperty("emojis")]
        public CustomEmoji[] Emojis { get; set; }

        [JsonProperty("uri")]
        [CanBeNull]
        public string Uri { get; set; }

        [JsonProperty("url")]
        [CanBeNull]
        public string Url { get; set; }

        [JsonProperty("isHidden")]
        public bool? IsHidden { get; set; }
    }

    public class InstanceLite // for UserLite
    {
        [JsonProperty("name")]
        [CanBeNull]
        public string Name { get; set; }

        [JsonProperty("softwareName")]
        [CanBeNull]
        public string SoftwareName { get; set; }

        [JsonProperty("softwareVersion")]
        [CanBeNull]
        public string SoftwareVersion { get; set; }

        [JsonProperty("iconUrl")]
        [CanBeNull]
        public string IconUrl { get; set; }

        [JsonProperty("faviconUrl")]
        [CanBeNull]
        public string FaviconUrl { get; set; }

        [JsonProperty("themeColor")]
        [CanBeNull]
        public string ThemeColor { get; set; }
    }

    public class Instance : InstanceLite // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L430
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("caughtAt")]
        public string CaughtAt { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("usersCount")]
        public int UsersCount { get; set; }

        [JsonProperty("notesCount")]
        public int NotesCount { get; set; }

        [JsonProperty("followingCount")]
        public int FollowingCount { get; set; }

        [JsonProperty("followersCount")]
        public int FollowersCount { get; set; }

        [JsonProperty("driveUsage")]
        public int DriveUsage { get; set; }

        [JsonProperty("driveFiles")]
        public int DriveFiles { get; set; }

        [JsonProperty("latestRequestSentAt")]
        [CanBeNull]
        public string LatestRequestSentAt { get; set; }

        [JsonProperty("latestStatus")]
        public int? LatestStatus { get; set; }

        [JsonProperty("latestRequestReceivedAt")]
        [CanBeNull]
        public string LatestRequestReceivedAt { get; set; }

        [JsonProperty("lastCommunicatedAt")]
        public string LastCommunicatedAt { get; set; }

        [JsonProperty("isNotResponding")]
        public bool IsNotResponding { get; set; }

        [JsonProperty("isSuspended")]
        public bool IsSuspended { get; set; }

        [JsonProperty("openRegistrations")]
        public bool? OpenRegistrations { get; set; }

        [JsonProperty("description")]
        [CanBeNull]
        public string Description { get; set; }

        [JsonProperty("maintainerName")]
        [CanBeNull]
        public string MaintainerName { get; set; }

        [JsonProperty("maintainerEmail")]
        [CanBeNull]
        public string MaintainerEmail { get; set; }

        [JsonProperty("infoUpdatedAt")]
        [CanBeNull]
        public string InfoUpdatedAt { get; set; }
    }

    public class LiteInstanceMetadata // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L256
    {
        public class Ad
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("ratio")]
            public int Ratio { get; set; }

            [JsonProperty("place")]
            public string Place { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("imageUrl")]
            public string ImageUrl { get; set; }
        }

        [JsonProperty("maintainerName")]
        [CanBeNull]
        public string MaintainerName { get; set; }

        [JsonProperty("maintainerEmail")]
        [CanBeNull]
        public string MaintainerEmail { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("name")]
        [CanBeNull]
        public string Name { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("description")]
        [CanBeNull]
        public string Description { get; set; }

        [JsonProperty("tosUrl")]
        [CanBeNull]
        public string TosUrl { get; set; }

        [JsonProperty("disableRegistration")]
        public bool DisableRegistration { get; set; }

        [JsonProperty("disableLocalTimeline")]
        public bool DisableLocalTimeline { get; set; }

        [JsonProperty("disableGlobalTimeline")]

        public bool DisableGlobalTimeline { get; set; }

        [JsonProperty("driveCapacityPerLocalUserMb")]
        public int DriveCapacityPerLocalUserMb { get; set; }

        [JsonProperty("driveCapacityPerRemoteUserMb")]
        public int DriveCapacityPerRemoteUserMb { get; set; }

        [JsonProperty("enableHcaptcha")]
        public bool EnableHcaptcha { get; set; }

        [JsonProperty("hcaptchaSiteKey")]
        [CanBeNull]
        public string HcaptchaSiteKey { get; set; }

        [JsonProperty("enableRecaptcha")]
        public bool EnableRecaptcha { get; set; }

        [JsonProperty("recaptchaSiteKey")]
        [CanBeNull]
        public string RecaptchaSiteKey { get; set; }

        [JsonProperty("swPublickey")]
        [CanBeNull]
        public string SwPublickey { get; set; }

        [JsonProperty("maxNoteTextLength")]
        public int MaxNoteTextLength { get; set; }

        [JsonProperty("enableEmail")]
        public bool EnableEmail { get; set; }

        [JsonProperty("enableTwitterIntegration")]
        public bool EnableTwitterIntegration { get; set; }

        [JsonProperty("enableGithubIntegration")]
        public bool EnableGithubIntegration { get; set; }

        [JsonProperty("enableDiscordIntegration")]
        public bool EnableDiscordIntegration { get; set; }

        [JsonProperty("enableServiceWorker")]
        public bool EnableServiceWorker { get; set; }

        [JsonProperty("emojis")]
        public CustomEmoji[] Emojis { get; set; }

        [JsonProperty("ads")]
        public Ad[] Ads { get; set; }
    }

    public class InstanceMetadata : LiteInstanceMetadata // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L290
    {
        [JsonProperty("feautres")]
        public Dictionary<string, object> Features { get; set; }
    }

    public class ServerInfo // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L296
    {
        public class cpu
        {
            [JsonProperty("model")]
            public string Model { get; set; }

            [JsonProperty("cores")]
            public int Cores { get; set; }
        }

        public class mem
        {
            [JsonProperty("total")]
            public int Total { get; set; }
        }

        public class fs
        {
            [JsonProperty("total")]
            public int Total { get; set; }

            [JsonProperty("used")]
            public int Used { get; set; }
        }

        [JsonProperty("machine")]
        public string Machine { get; set; }

        [JsonProperty("cpu")]
        public cpu Cpu { get; set; }

        [JsonProperty("mem")]
        public mem Mem { get; set; }

        [JsonProperty("fs")]
        public fs Fs { get; set; }
    }

    public class Stats // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L311
    {
        [JsonProperty("notesCount")]
        public int NotesCount { get; set; }

        [JsonProperty("originalNotesCount")]
        public int OriginalNotesCount { get; set; }

        [JsonProperty("usersCount")]
        public int UsersCount { get; set; }

        [JsonProperty("originalUsersCount")]
        public int OriginalUsersCount { get; set; }

        [JsonProperty("instances")]
        public int Instances { get; set; }

        [JsonProperty("driveUsageLocal")]
        public int DriveUsageLocal { get; set; }

        [JsonProperty("driveUsageRemote")]
        public int DriveUsageRemote { get; set; }
    }

    public class Page // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L321
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("content")]
        public Dictionary<string, object>[] Content { get; set; } // Record<string, any>[] https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L327

        [JsonProperty("variables")]
        public Dictionary<string, object>[] Variables { get; set; } // Record<string, any>[]

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("summary")]
        [CanBeNull]
        public string Summary { get; set; }

        [JsonProperty("hideTitleWhenPinned")]
        public bool HideTitleWhenPinned { get; set; }

        [JsonProperty("alignCenter")]
        public bool AlignCenter { get; set; }

        [JsonProperty("font")]
        public string Font { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("eyeCatchingImageId")]
        [CanBeNull]
        public string EyeCatchingImageId { get; set; }

        [JsonProperty("eyeCatchingImage")]
        [CanBeNull]
        public DriveFile EyeCatchingImage { get; set; }

        [JsonProperty("attachedFiles")]
        public Dictionary<string, object>[] AttachedFiles { get; set; } // Record<string, any>[]

        [JsonProperty("likedCount")]
        public int LikedCount { get; set; }

        [JsonProperty("isLiked")]
        public bool? IsLiked { get; set; }
    }

    public class PageEvent // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L343
    {
        [JsonProperty("pageId")]
        public string PageId { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        // [JsonProperty("var")]
        // public Dictionary<string, object> Var { get; set; } // Record<string, any> todo: fix this

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class Announcement // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L351
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        [CanBeNull]
        public string UpdatedAt { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imageUrl")]
        [CanBeNull]
        public string ImageUrl { get; set; }

        [JsonProperty("isRead")]
        public bool? IsRead { get; set; }
    }

    public class Antenna // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L361
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("keywords")]
        public string[][] Keywords { get; set; } // string[][]

        [JsonProperty("excludeKeywords")]
        public string[][] ExcludeKeywords { get; set; } // string[][]

        [JsonProperty("src")]
        public string Src { get; set; } // 'home' | 'all' | 'users' | 'list' | 'group'

        [JsonProperty("userListId")]
        [CanBeNull]
        public string UserListId { get; set; }

        [JsonProperty("userGroupId")]
        [CanBeNull]
        public string UserGroupId { get; set; }

        [JsonProperty("users")]
        public string[] Users { get; set; }

        [JsonProperty("caseSensitive")]
        public bool CaseSensitive { get; set; }

        [JsonProperty("notify")]
        public bool Notify { get; set; }

        [JsonProperty("withReplies")]
        public bool WithReplies { get; set; }

        [JsonProperty("withFile")]
        public bool WithFile { get; set; }

        [JsonProperty("hasUnreadNote")]
        public bool HasUnreadNote { get; set; }
    }

    public class AuthSession // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L380
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("app")]
        public Dictionary<string, object> App { get; set; } //todo

        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public class NoteFavorite // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L390
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("noteId")]
        public string NoteId { get; set; }

        [JsonProperty("note")]
        public Note Note { get; set; }
    }

    public class FollowRequest // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L397
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("follower")]
        public User Follower { get; set; }

        [JsonProperty("followee")]
        public User Followee { get; set; }
    }

    public class Channel // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L403
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Following // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L408
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("followerId")]
        public string FollowerId { get; set; }

        [JsonProperty("followeeId")]
        public string FolloweeId { get; set; }
    }

    public class FollowingFolloweePopulated : Following // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L415
    {
        [JsonProperty("followee")]
        public User Followee { get; set; }
    }

    public class FollowingFollowerPopulated : Following // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L419
    {
        [JsonProperty("follower")]
        public User Follower { get; set; }
    }

    public class Blocking // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L423
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("blockeeId")]
        public string BlockeeId { get; set; }

        [JsonProperty("blockee")]
        public User Blockee { get; set; }
    }

    public class Signin // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L459
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, object> Headers { get; set; } //todo

        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class UserLite // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L9
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("host")]
        [CanBeNull]
        public string Host { get; set; }

        [JsonProperty("onlineStatus")] // 'online' | 'active' | 'offline' | 'unknown'
        public string OnlineStatus { get; set; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }

        [JsonProperty("avatarBlurhash")]
        public string AvatarBlurhash { get; set; }

        [JsonProperty("emojis")]
        public CustomEmoji[] Emojis { get; set; }

        [JsonProperty("instance")]
        [CanBeNull]
        public InstanceLite Instance { get; set; }
    }

    public class User : UserLite // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L31
    {
        [JsonProperty("bannerBlurhash")]
        [CanBeNull]
        public string BannerBlurhash { get; set; }

        [JsonProperty("bannerColor")]
        [CanBeNull]
        public string BannerColor { get; set; }

        [JsonProperty("bannerUrl")]
        [CanBeNull]
        public string BannerUrl { get; set; }

        [JsonProperty("birthday")]
        [CanBeNull]
        public string Birthday { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("description")]
        [CanBeNull]
        public string Description { get; set; }

        [JsonProperty("ffVisibility")] // 'public' | 'followers' | 'private'
        public string FfVisibility { get; set; }

        [JsonProperty("fields")]
        [CanBeNull]
        public Dictionary<string, string>[] Fields { get; set; }

        [JsonProperty("followersCount")]
        public int FollowersCount { get; set; }

        [JsonProperty("followingCount")]
        public int FollowingCount { get; set; }

        [JsonProperty("hasPendingFollowRequestFromYou")]
        public bool HasPendingFollowRequestFromYou { get; set; }

        [JsonProperty("hasPendingFollowRequestToYou")]
        public bool HasPendingFollowRequestToYou { get; set; }

        [JsonProperty("isAdmin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("isBlocked")]
        public bool IsBlocked { get; set; }

        [JsonProperty("isBlocking")]
        public bool IsBlocking { get; set; }

        [JsonProperty("isBot")]
        public bool IsBot { get; set; }

        [JsonProperty("isCat")]
        public bool IsCat { get; set; }

        [JsonProperty("isLocked")]
        public bool IsLocked { get; set; }

        [JsonProperty("isModerator")]
        public bool IsModerator { get; set; }

        [JsonProperty("isMuted")]
        public bool IsMuted { get; set; }

        [JsonProperty("isSilenced")]
        public bool IsSilenced { get; set; }

        [JsonProperty("isSuspended")]
        public bool IsSuspended { get; set; }

        [JsonProperty("lang")]
        [CanBeNull]
        public string Lang { get; set; }

        [JsonProperty("lastFetchedAt")]
        [CanBeNull]
        public string LastFetchedAt { get; set; }

        [JsonProperty("location")]
        [CanBeNull]
        public string Location { get; set; }

        [JsonProperty("notesCount")]
        public int NotesCount { get; set; }

        [JsonProperty("pinnedNoteIds")]
        [ItemCanBeNull]
        public string[] PinnedNoteIds { get; set; }

        [JsonProperty("pinnedNote")]
        [CanBeNull]
        public Note[] PinnedNote { get; set; }

        [JsonProperty("pinnedPageId")]
        [CanBeNull]
        public string PinnedPageId { get; set; }

        [JsonProperty("pinnedPage")]
        [CanBeNull]
        public Page PinnedPage { get; set; }

        [JsonProperty("publicReactions")]
        public bool PublicReactions { get; set; }

        [JsonProperty("securityKeys")]
        public bool SecurityKeys { get; set; }

        [JsonProperty("twoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }

        [JsonProperty("updatedAt")]
        [CanBeNull]
        public string UpdatedAt { get; set; }

        [JsonProperty("uri")]
        [CanBeNull]
        public string Uri { get; set; }

        [JsonProperty("url")]
        [CanBeNull]
        public string Url { get; set; }
    }

    public class Me : User // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L81
    {
        [JsonProperty("avatarId")]
        public string AvatarId { get; set; } //DriveFile['id']

        [JsonProperty("bannerId")]
        public string BannerId { get; set; } //DriveFile['id']

        [JsonProperty("autoAcceptFollowed")]
        public bool AutoAcceptFollowed { get; set; }

        [JsonProperty("alwaysMarkNsfw")]
        public bool AlwaysMarkNsfw { get; set; }

        [JsonProperty("carefulBot")]
        public bool CarefulBot { get; set; }

        [JsonProperty("emailNotificationTypes")]
        public string[] EmailNotificationTypes { get; set; }

        [JsonProperty("hasPendingReceivedFollowRequest")]
        public bool HasPendingReceivedFollowRequest { get; set; }

        [JsonProperty("hasUnreadAnnouncement")]
        public bool HasUnreadAnnouncement { get; set; }

        [JsonProperty("hasUnreadAntenna")]
        public bool HasUnreadAntenna { get; set; }

        [JsonProperty("hasUnreadChannel")]
        public bool HasUnreadChannel { get; set; }

        [JsonProperty("hasUnreadMentions")]
        public bool HasUnreadMentions { get; set; }

        [JsonProperty("hasUnreadMessagingMessage")]
        public bool HasUnreadMessagingMessage { get; set; }

        [JsonProperty("hasUnreadNotification")]
        public bool HasUnreadNotification { get; set; }

        [JsonProperty("hasUnreadSpecifiedNotes")]
        public bool HasUnreadSpecifiedNotes { get; set; }

        [JsonProperty("hideOnlineStatus")]
        public bool HideOnlineStatus { get; set; }

        [JsonProperty("injectFeaturedNote")]
        public bool InjectFeaturedNote { get; set; }

        [JsonProperty("integrations")]
        public Dictionary<string, object> Integrations { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("isExplorable")]
        public bool IsExplorable { get; set; }

        [JsonProperty("mutedWords")]
        public string[][] MutedWords { get; set; }

        [JsonProperty("mutingNotificationTypes")]
        public string[] MutingNotificationTypes { get; set; }

        [JsonProperty("noCrawle")]
        public bool NoCrawle { get; set; }

        [JsonProperty("receiveAnnouncementEmail")]
        public bool ReceiveAnnouncementEmail { get; set; }

        [JsonProperty("usePasswordLessLogin")]
        public bool UsePasswordLessLogin { get; set; }
    }

    public class UserList // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L74
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("userIds")]
        public string[] UserIds { get; set; }
    }

    public class NoteReaction // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L166
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("user")]
        public UserLite User { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class MessagingMessage // https://github.com/misskey-dev/misskey.js/blob/c89374c321aeb1cca2582922d4a9a9be059c691e/src/entities.ts#L232
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("file")]
        [CanBeNull]
        public DriveFile File { get; set; }

        [JsonProperty("fileId")]
        [CanBeNull]
        public string FileId { get; set; }

        [JsonProperty("isRead")]
        public bool IsRead { get; set; }

        [JsonProperty("reads")]
        public string[] Reads { get; set; }

        [JsonProperty("text")]
        [CanBeNull]
        public string Text { get; set; }

        [JsonProperty("user")]
        public UserLite User { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("recipient")]
        [CanBeNull]
        public UserLite Recipient { get; set; }

        [JsonProperty("recipientId")]
        [CanBeNull]
        public string RecipientId { get; set; }

        [JsonProperty("group")]
        [CanBeNull]
        public Dictionary<string, object> Group { get; set; } //todo

        [JsonProperty("groupId")]
        [CanBeNull]
        public string GroupId { get; set; }
    }

    // export type UserSorting =
    // | '+follower'
    // | '-follower'
    // | '+createdAt'
    // | '-createdAt'
    // | '+updatedAt'
    // | '-updatedAt';
    // export type OriginType = 'combined' | 'local' | 'remote';
}
