using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HackerNews
{
    /// <summary>
    /// Scraper request class representing a single page HTTP/HTTPS request.
    /// </summary>
    class ScraperRequest
    {
        /// <summary>
        /// List of parsed posts retrieved in this request.
        /// </summary>
        public List<Post> Results;
        /// <summary>
        /// Final request status.
        /// </summary>
        public ScraperStatus Status;

        private string targetUrl;

        /// <summary>
        /// Creates request for a single page of the target site.
        /// </summary>
        /// <param name="targetUrl">Fully qualified target URI.</param>
        public ScraperRequest(string targetUrl)
        {
            this.targetUrl = targetUrl;

            Results = new List<Post>();
            Status = ScraperStatus.Empty;
        }

        /// <summary>
        /// The main request.
        /// </summary>
        public void Run()
        {
            HtmlWeb engine = new HtmlWeb();
            HtmlDocument doc;

            // trying to load the page
            try
            {
                doc = engine.Load(targetUrl);
            }
            catch
            {
                // connection failure
                doc = null;
                Status = ScraperStatus.ConnectionError;
            }

            // if we have a page...
            if (doc != null)
            {
                // ... fetch the posts and ...
                HtmlNodeCollection mainNodes = doc.DocumentNode.SelectNodes("/html/body/center/table/tr/td/table//tr[@class='athing']");

                // ... loop through them
                foreach (HtmlNode mainNode in mainNodes)
                {
                    int? rank = null;
                    string title = null;
                    string url = null;
                    int? score = null;
                    string user = null;
                    int? comments = null;

                    // get handles of various bits of post metadata
                    HtmlNode rankNode = mainNode.SelectSingleNode("td/span[@class='rank']");
                    HtmlNode titleNode = mainNode.SelectSingleNode("td[@class='title']/a[@class='storylink']");
                    HtmlNode followupNode = mainNode.NextSibling;
                    HtmlNode pointsNode = followupNode?.SelectSingleNode("td[@class='subtext']/span[@class='score']");
                    HtmlNode userNode = followupNode?.SelectSingleNode("td[@class='subtext']/a[@class='hnuser']");
                    HtmlNode commentsNode = followupNode?.SelectSingleNode("td[@class='subtext']/a[last()]");

                    // post rank
                    if (rankNode != null)
                    {
                        rank = ExtractFirstNumber(rankNode.InnerText);
                    }

                    // title and URI
                    if (titleNode != null)
                    {
                        title = titleNode.InnerText;
                        url = titleNode.Attributes["href"]?.Value;

                        // fix the internal Hacker News links
                        if (!String.IsNullOrWhiteSpace(url) && url.StartsWith("item"))
                        {
                            url = targetUrl + url;
                        }
                    }

                    // points / upvotes
                    if (pointsNode != null)
                    {
                        score = ExtractFirstNumber(pointsNode.InnerText);
                    }

                    // username
                    if (userNode != null)
                    {
                        user = userNode.InnerText;
                    }

                    // comments (if any)
                    if (commentsNode != null)
                    {
                        comments = ExtractFirstNumber(commentsNode.InnerText);
                    }

                    // if no comments found - set to zero
                    if (!comments.HasValue)
                    {
                        comments = 0;
                    }

                    // validate the basics
                    if (rank.HasValue && !String.IsNullOrWhiteSpace(title) && !String.IsNullOrWhiteSpace(url) && score.HasValue && !String.IsNullOrWhiteSpace(user) && comments.HasValue)
                    {
                        Post newPost = new Post()
                        {
                            Title = title.Trim(),
                            URI = url,
                            User = user.Trim(),
                            Points = score.Value,
                            Comments = comments.Value,
                            Rank = rank.Value
                        };

                        // validate and save the post
                        if (Post.Validate(ref newPost))
                        {
                            Results.Add(newPost);
                        }
                    }
                }

                // if any posts found - it's a success!
                if (Results.Count > 0)
                {
                    Status = ScraperStatus.Success;
                }
            }
        }

        /// <summary>
        /// Fetched the first integer from a string.
        /// </summary>
        private int? ExtractFirstNumber(string data)
        {
            Match match = Regex.Match(data, "\\d+");

            if (match.Success)
            {
                int tempValue;

                if (Int32.TryParse(match.Value, out tempValue))
                {
                    return tempValue;
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Status of the scraper request.
    /// </summary>
    enum ScraperStatus
    {
        /// <summary>
        /// Posts fetched successfully.
        /// </summary>
        Success,

        /// <summary>
        /// Unable to retrieve the webpage.
        /// </summary>
        ConnectionError,

        /// <summary>
        /// No posts found.
        /// </summary>
        Empty
    }
}
