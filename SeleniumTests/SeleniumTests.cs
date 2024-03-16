using Logic;
using NUnit.Framework;

namespace SeleniumTests
{
    public class SeleniumTests : PageObjectsSetup
    {
        [Test]
        [Category("Selenium")]
        public void FirstTest()
        {
            PageObjects.VerifyDockerContainers();
        }
    }
}
