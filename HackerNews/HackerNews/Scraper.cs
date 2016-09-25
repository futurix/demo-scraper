using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews
{
    /// <summary>
    /// High level scraper class - makes multiple web requests under the hood, when needed.
    /// </summary>
    class Scraper
    {
        /// <summary>
        /// Base URI of a target website.
        /// </summary>
        const string BaseAddress = "https://news.ycombinator.com/";

        /// <summary>
        /// Parsed web site posts.
        /// </summary>
        public List<Post> Results;

        private int numberOfPosts = 0;

        /// <summary>
        /// Create instance of scraper.
        /// </summary>
        /// <param name="numberOfPosts">Number of posts to return.</param>
        public Scraper(int numberOfPosts)
        {
            this.numberOfPosts = numberOfPosts;

            Results = new List<Post>();
        }

        /// <summary>
        /// Launches scraping process.
        /// </summary>
        public void Run()
        {
            if (numberOfPosts > 0)
            {
                int availablePosts = 0;
                int page = 1;

                // loops until the required number of posts is retrieved
                while (availablePosts < numberOfPosts)
                {
                    string targetUri;

                    // add page ID for pages after the first one
                    if (page > 1)
                        targetUri = String.Format("{0}news?p={1}", BaseAddress, page);
                    else
                        targetUri = BaseAddress;

                    // launch page request
                    ScraperRequest req = new ScraperRequest(targetUri);
                    req.Run();

                    // merge the posts if success
                    if (req.Status == ScraperStatus.Success)
                    {
                        if (req.Results.Count > 0)
                        {
                            int maxPostsToMerge = numberOfPosts - availablePosts;

                            // only add enough posts to satisfy the required number of posts
                            Results.AddRange(req.Results.Take(maxPostsToMerge));
                        }

                        availablePosts = Results.Count;
                        page++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
