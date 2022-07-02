// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;


namespace osu.Game.Online.MisskeyAPI.Requests.Responses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class I //: IEquatable<I>
    {
        /// <summary>
        /// A user ID which can be used to represent any system user which is not attached to a user profile.
        /// </summary>
        public const string SYSTEM_USER_ID = "system";

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Channel
        {
        }

        public class Emoji
        {
            [JsonProperty("name")]
            public string Name;

            [JsonProperty("url")]
            public string Url;
        }

        public class Field
        {
            [JsonProperty("name")]
            public string Name;

            [JsonProperty("value")]
            public string Value;
        }

        public class File
        {
            [JsonProperty("id")]
            public string Id;

            [JsonProperty("createdAt")]
            public DateTime CreatedAt;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("type")]
            public string Type;

            [JsonProperty("md5")]
            public string Md5;

            [JsonProperty("size")]
            public int Size;

            [JsonProperty("isSensitive")]
            public bool IsSensitive;

            [JsonProperty("blurhash")]
            public string Blurhash;

            [JsonProperty("properties")]
            public Properties Properties;

            [JsonProperty("url")]
            public string Url;

            [JsonProperty("thumbnailUrl")]
            public string ThumbnailUrl;

            [JsonProperty("comment")]
            public string Comment;

            [JsonProperty("folderId")]
            public string FolderId;

            [JsonProperty("folder")]
            [CanBeNull]
            public Folder Folder;

            [JsonProperty("userId")]
            public string UserId;

            [JsonProperty("user")]
            [CanBeNull]
            public User User;
        }

        public class Folder
        {
            [JsonProperty("id")]
            public string Id;

            [JsonProperty("createdAt")]
            public DateTime CreatedAt;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("foldersCount")]
            public int? FoldersCount;

            [JsonProperty("filesCount")]
            public int? FilesCount;

            [JsonProperty("parentId")]
            public string ParentId;

            [JsonProperty("parent")]
            [CanBeNull]
            public Parent Parent;
        }

        public class Integration
        {
        }

        public class MyReaction
        {
        }

        public class Parent
        {
        }

        public class PinnedNote
        {
            [JsonProperty("id")]
            public string Id;

            [JsonProperty("createdAt")]
            public DateTime CreatedAt;

            [JsonProperty("text")]
            public string Text;

            [JsonProperty("cw")]
            [CanBeNull]
            public string Cw;

            [JsonProperty("userId")]
            public string UserId;

            [JsonProperty("user")]
            public User User;

            [JsonProperty("replyId")]
            [CanBeNull]
            public string ReplyId;

            [JsonProperty("renoteId")]
            [CanBeNull]
            public string RenoteId;

            [JsonProperty("reply")]
            [CanBeNull]
            public Reply Reply;

            [JsonProperty("renote")]
            [CanBeNull]
            public Renote Renote;

            [JsonProperty("isHidden")]
            public bool? IsHidden;

            [JsonProperty("visibility")]
            public string Visibility;

            [JsonProperty("mentions")]
            [CanBeNull]
            public List<string> Mentions;

            [JsonProperty("visibleUserIds")]
            [CanBeNull]
            public List<string> VisibleUserIds;

            [JsonProperty("fileIds")]
            [CanBeNull]
            public List<string> FileIds;

            [JsonProperty("files")]
            [CanBeNull]
            public List<File> Files;

            [JsonProperty("tags")]
            [CanBeNull]
            public List<string> Tags;

            [JsonProperty("poll")]
            [CanBeNull]
            public Poll Poll;

            [JsonProperty("channelId")]
            [CanBeNull]
            public string ChannelId;

            [JsonProperty("channel")]
            [CanBeNull]
            public Channel Channel;

            [JsonProperty("localOnly")]
            public bool? LocalOnly;

            [JsonProperty("emojis")]
            public List<Emoji> Emojis;

            [JsonProperty("reactions")]
            public Reactions Reactions;

            [JsonProperty("renoteCount")]
            public int RenoteCount;

            [JsonProperty("repliesCount")]
            public int RepliesCount;

            [JsonProperty("uri")]
            [CanBeNull]
            public string Uri;

            [JsonProperty("url")]
            [CanBeNull]
            public string Url;

            [JsonProperty("myReaction")]
            [CanBeNull]
            public MyReaction MyReaction;
        }

        public class PinnedPages
        {
            [JsonProperty("id")]
            public string Id;

            [JsonProperty("createdAt")]
            public DateTime CreatedAt;

            [JsonProperty("updatedAt")]
            public DateTime UpdatedAt;

            [JsonProperty("title")]
            public string Title;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("summary")]
            public string Summary;

            [JsonProperty("content")]
            public List<object> Content;

            [JsonProperty("variables")]
            public List<object> Variables;

            [JsonProperty("userId")]
            public string UserId;

            [JsonProperty("user")]
            public User User;
        }

        public class Poll
        {
        }

        public class Properties
        {
            [JsonProperty("width")]
            public int? Width;

            [JsonProperty("height")]
            public int? Height;

            [JsonProperty("orientation")]
            public int? Orientation;

            [JsonProperty("avgColor")]
            [CanBeNull]
            public string AvgColor;
        }

        public class Reactions
        {
        }

        public class Renote
        {
        }

        public class Reply
        {
        }

        [JsonProperty("id")]
        public string Id { get; set; } = "guest";

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("username")]
        public string Username;

        [JsonProperty("host")]
        public string Host;

        [JsonProperty("avatarUrl")]
        public string AvatarUrl;

        [JsonProperty("avatarBlurhash")]
        public object AvatarBlurhash;

        [JsonProperty("avatarColor")]
        public object AvatarColor;

        [JsonProperty("isAdmin")]
        public bool? IsAdmin;

        [JsonProperty("isModerator")]
        public bool? IsModerator;

        [JsonProperty("isBot")]
        public bool? IsBot;

        [JsonProperty("isCat")]
        public bool? IsCat;

        [JsonProperty("emojis")]
        public List<Emoji> Emojis;

        [JsonProperty("onlineStatus")]
        public string OnlineStatus;

        [JsonProperty("url")]
        public string Url;

        [JsonProperty("uri")]
        public string Uri;

        [JsonProperty("createdAt")]
        public DateTime CreatedAt;

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt;

        [JsonProperty("lastFetchedAt")]
        public DateTime LastFetchedAt;

        [JsonProperty("bannerUrl")]
        public string BannerUrl;

        [JsonProperty("bannerBlurhash")]
        public object BannerBlurhash;

        [JsonProperty("bannerColor")]
        public object BannerColor;

        [JsonProperty("isLocked")]
        public bool IsLocked;

        [JsonProperty("isSilenced")]
        public bool IsSilenced;

        [JsonProperty("isSuspended")]
        public bool IsSuspended;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("location")]
        public string Location;

        [JsonProperty("birthday")]
        public string Birthday;

        [JsonProperty("lang")]
        public string Lang;

        [JsonProperty("fields")]
        public List<Field> Fields;

        [JsonProperty("followersCount")]
        public int FollowersCount;

        [JsonProperty("followingCount")]
        public int FollowingCount;

        [JsonProperty("notesCount")]
        public int NotesCount;

        [JsonProperty("pinnedNoteIds")]
        public List<string> PinnedNoteIds;

        [JsonProperty("pinnedNotes")]
        public List<PinnedNote> PinnedNotes;

        [JsonProperty("pinnedPageId")]
        public string PinnedPageId;

        [JsonProperty("pinnedPage")]
        public PinnedPages PinnedPage;

        [JsonProperty("publicReactions")]
        public bool PublicReactions;

        [JsonProperty("twoFactorEnabled")]
        public bool TwoFactorEnabled;

        [JsonProperty("usePasswordLessLogin")]
        public bool UsePasswordLessLogin;

        [JsonProperty("securityKeys")]
        public bool SecurityKeys;

        [JsonProperty("isFollowing")]
        public bool? IsFollowing;

        [JsonProperty("isFollowed")]
        public bool? IsFollowed;

        [JsonProperty("hasPendingFollowRequestFromYou")]
        public bool? HasPendingFollowRequestFromYou;

        [JsonProperty("hasPendingFollowRequestToYou")]
        public bool? HasPendingFollowRequestToYou;

        [JsonProperty("isBlocking")]
        public bool? IsBlocking;

        [JsonProperty("isBlocked")]
        public bool? IsBlocked;

        [JsonProperty("isMuted")]
        public bool? IsMuted;

        [JsonProperty("avatarId")]
        public string AvatarId;

        [JsonProperty("bannerId")]
        public string BannerId;

        [JsonProperty("injectFeaturedNote")]
        public bool InjectFeaturedNote;

        [JsonProperty("receiveAnnouncementEmail")]
        public bool ReceiveAnnouncementEmail;

        [JsonProperty("alwaysMarkNsfw")]
        public bool AlwaysMarkNsfw;

        [JsonProperty("carefulBot")]
        public bool CarefulBot;

        [JsonProperty("autoAcceptFollowed")]
        public bool AutoAcceptFollowed;

        [JsonProperty("noCrawle")]
        public bool NoCrawle;

        [JsonProperty("isExplorable")]
        public bool IsExplorable;

        [JsonProperty("isDeleted")]
        public bool IsDeleted;

        [JsonProperty("hideOnlineStatus")]
        public bool HideOnlineStatus;

        [JsonProperty("hasUnreadSpecifiedNotes")]
        public bool HasUnreadSpecifiedNotes;

        [JsonProperty("hasUnreadMentions")]
        public bool HasUnreadMentions;

        [JsonProperty("hasUnreadAnnouncement")]
        public bool HasUnreadAnnouncement;

        [JsonProperty("hasUnreadAntenna")]
        public bool HasUnreadAntenna;

        [JsonProperty("hasUnreadChannel")]
        public bool HasUnreadChannel;

        [JsonProperty("hasUnreadMessagingMessage")]
        public bool HasUnreadMessagingMessage;

        [JsonProperty("hasUnreadNotification")]
        public bool HasUnreadNotification;

        [JsonProperty("hasPendingReceivedFollowRequest")]
        public bool HasPendingReceivedFollowRequest;

        [JsonProperty("integrations")]
        public Integration Integrations;

        [JsonProperty("mutedWords")]
        public List<List<string>> MutedWords;

        [JsonProperty("mutedInstances")]
        public List<string> MutedInstances;

        [JsonProperty("mutingNotificationTypes")]
        public List<string> MutingNotificationTypes;

        [JsonProperty("emailNotificationTypes")]
        public List<string> EmailNotificationTypes;

        [JsonProperty("email")]
        [CanBeNull]
        public string Email;

        [JsonProperty("emailVerified")]
        public bool? EmailVerified;

        [JsonProperty("securityKeysList")]
        [CanBeNull]
        public List<SecurityKeysLists> SecurityKeysList;

        public class SecurityKeysLists
        {
        }

        public class User
        {
            [JsonProperty("id")]
            public string Id;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("username")]
            public string Username;

            [JsonProperty("host")]
            public string Host;

            [JsonProperty("avatarUrl")]
            public string AvatarUrl;

            [JsonProperty("avatarBlurhash")]
            public object AvatarBlurhash;

            [JsonProperty("avatarColor")]
            public object AvatarColor;

            [JsonProperty("isAdmin")]
            public bool IsAdmin;

            [JsonProperty("isModerator")]
            public bool IsModerator;

            [JsonProperty("isBot")]
            public bool IsBot;

            [JsonProperty("isCat")]
            public bool IsCat;

            [JsonProperty("emojis")]
            public List<Emoji> Emojis;

            [JsonProperty("onlineStatus")]
            public string OnlineStatus;

            //public bool Equals(I other) => this.MatchesOnlineID(other);
        }
    }
}
