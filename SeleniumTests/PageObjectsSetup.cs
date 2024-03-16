using GeneralUtils;
using Logic;
using NUnit.Framework;

namespace SeleniumTests
{
    public class PageObjectsSetup : BaseDriver
    {
        public PageObjects PageObjects;

        [SetUp]
        public void SetUpPageObjects()
        {
            PageObjects = new PageObjects(driver);
        }
    }
}
