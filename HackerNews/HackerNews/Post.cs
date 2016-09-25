using Newtonsoft.Json;
using System;

namespace HackerNews
{
    /// <summary>
    /// Structure that represents a single post on Hacker News.
    /// </summary>
    struct Post
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("uri")]
        public string URI { get; set; }

        [JsonProperty("author")]
        public string User { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("comments")]
        public int Comments { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// Static method to validate contents of a post.
        /// </summary>
        public static bool Validate(ref Post post)
        {
            return
                !String.IsNullOrWhiteSpace(post.Title) &&
                post.Title.Length <= 256 &&
                !String.IsNullOrWhiteSpace(post.URI) &&
                Uri.IsWellFormedUriString(post.URI, UriKind.Absolute) &&
                !String.IsNullOrWhiteSpace(post.User) &&
                post.User.Length <= 256 && 
                post.Points >= 0 &&
                post.Comments >= 0 &&
                post.Rank >= 0;
        }
    }
}
