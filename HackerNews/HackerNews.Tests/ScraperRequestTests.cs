using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerNews.Tests
{
    [TestClass]
    public class ScraperRequestTests
    {
        /// <summary>
        /// Runs scraper against unrelated domain.
        /// </summary>
        [TestMethod]
        public void UnrelatedDomainTest()
        {
            ScraperRequest req = new ScraperRequest("https://www.example.com/");
            req.Run();

            Assert.IsTrue(req.Status == ScraperStatus.Empty);
        }

        /// <summary>
        /// Runs scraper on a different page of the correct domain.
        /// </summary>
        [TestMethod]
        public void WrongPageTest()
        {
            ScraperRequest req = new ScraperRequest("https://news.ycombinator.com/newsfaq.html");
            req.Run();

            Assert.IsTrue(req.Results.Count == 0);
        }
    }
}
