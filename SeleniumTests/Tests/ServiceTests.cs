using Logic;
using NUnit.Framework;

namespace SeleniumTests
{
    public class ServiceTests : PageObjectsSetup
    {
        [Test]
        [Category("ServiceTests")]
        public void FirstTest()
        {
            PageObjects.VerifyDockerContainers();
        }
    }
}
