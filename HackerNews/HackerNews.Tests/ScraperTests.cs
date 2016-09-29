using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerNews.Tests
{
    [TestClass]
    public class ScraperTests
    {
        [TestMethod]
        public void BadInputTest()
        {
            Scraper scr = new Scraper(-1);
            scr.Run();

            Assert.IsTrue(scr.Results.Count == 0);
        }

        [TestMethod]
        public void EmptyTest()
        {
            Scraper scr = new Scraper(0);
            scr.Run();

            Assert.IsTrue(scr.Results.Count == 0);
        }

        [TestMethod]
        public void MeaningOfLifeTest()
        {
            Scraper scr = new Scraper(42);
            scr.Run();

            Assert.IsTrue(scr.Results.Count == 42);
        }
    }
}
