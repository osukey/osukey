// Copyright (c) sim1222 <kokt@sim1222.com>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace osu.Game.Online.MisskeyAPI.Requests.Responses.Notes
{
    public class Channel
    {
    }

    public class Emoji
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class File
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("isSensitive")]
        public bool? IsSensitive { get; set; }

        [JsonProperty("blurhash")]
        public string Blurhash { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("folderId")]
        public string FolderId { get; set; }

        [JsonProperty("folder")]
        public Folder Folder { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class Folder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("foldersCount")]
        public int? FoldersCount { get; set; }

        [JsonProperty("filesCount")]
        public int? FilesCount { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("parent")]
        public Parent Parent { get; set; }
    }

    public class MyReaction
    {
    }

    public class Parent
    {
    }

    public class Poll
    {
    }

    public class Properties
    {
        [JsonProperty("width")]
        public int? Width { get; set; }

        [JsonProperty("height")]
        public int? Height { get; set; }

        [JsonProperty("orientation")]
        public int? Orientation { get; set; }

        [JsonProperty("avgColor")]
        public string AvgColor { get; set; }
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

    public class HybridTimeline
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("cw")]
        public string Cw { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("replyId")]
        public string ReplyId { get; set; }

        [JsonProperty("renoteId")]
        public string RenoteId { get; set; }

        [JsonProperty("reply")]
        public Reply Reply { get; set; }

        [JsonProperty("renote")]
        public Renote Renote { get; set; }

        [JsonProperty("isHidden")]
        public bool? IsHidden { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("mentions")]
        public List<string> Mentions { get; set; }

        [JsonProperty("visibleUserIds")]
        public List<string> VisibleUserIds { get; set; }

        [JsonProperty("fileIds")]
        public List<string> FileIds { get; set; }

        [JsonProperty("files")]
        public List<File> Files { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("poll")]
        public Poll Poll { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("channel")]
        public Channel Channel { get; set; }

        [JsonProperty("localOnly")]
        public bool? LocalOnly { get; set; }

        [JsonProperty("emojis")]
        public List<Emoji> Emojis { get; set; }

        [JsonProperty("reactions")]
        public Reactions Reactions { get; set; }

        [JsonProperty("renoteCount")]
        public int? RenoteCount { get; set; }

        [JsonProperty("repliesCount")]
        public int? RepliesCount { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("myReaction")]
        public MyReaction MyReaction { get; set; }
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
        public string Host { get; set; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }

        [JsonProperty("avatarBlurhash")]
        public object AvatarBlurhash { get; set; }

        [JsonProperty("avatarColor")]
        public object AvatarColor { get; set; }

        [JsonProperty("isAdmin")]
        public bool? IsAdmin { get; set; }

        [JsonProperty("isModerator")]
        public bool? IsModerator { get; set; }

        [JsonProperty("isBot")]
        public bool? IsBot { get; set; }

        [JsonProperty("isCat")]
        public bool? IsCat { get; set; }

        [JsonProperty("emojis")]
        public List<Emoji> Emojis { get; set; }

        [JsonProperty("onlineStatus")]
        public string OnlineStatus { get; set; }
    }
}
