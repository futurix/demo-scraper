using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerNews.Tests
{
    [TestClass]
    public class CommandLineInputTests
    {
        [TestMethod]
        public void BasicLaunch()
        {
            int result = Program.Main(new string[] { "--posts", "10" });

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void AbbreviatedLaunch()
        {
            int result = Program.Main(new string[] { "-p", "31" });

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void BadArgsLaunch()
        {
            int result = Program.Main(new string[] { "-x", "11" });

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TooManyPostsLaunch()
        {
            int result = Program.Main(new string[] { "--posts", "101" });

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void NoArgsLaunch()
        {
            int result = Program.Main(new string[] { });

            Assert.AreEqual(1, result);
        }
    }
}
