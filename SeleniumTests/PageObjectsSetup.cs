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
            AppSettings settings = AppSettings.LoadSettings(
                "/Users/arthurpinhas/CodeRepos/services-automation/SeleniumTests/appsettings.pageobjects.json"
            );

            string className = GetType().Name;
            if (className.Equals("ServiceTests"))
            {
                GeneralLogin.LoginToPortainer(settings.Url, settings.Username, settings.Password);
            }
        }
    }
}
