using GeneralUtils;
using Logic;
using NUnit.Framework;

namespace SeleniumTests
{
    public class PageObjectsSetup : BaseDriver
    {
        public PageObjects PageObjects;
        public AppSettings settings;

        [SetUp]
        public void SetUpPageObjects()
        {
            AppSettings settings = AppSettings.LoadSettings(
                "/Users/arthurpinhas/CodeRepos/services-automation/SeleniumTests/appsettings.pageobjects.json"
            );

            PageObjects = new PageObjects(driver, settings);

            string className = GetType().Name;
            if (className.Equals("ServiceTests"))
            {
                GeneralLogin.LoginToPortainer(settings.Url, settings.Username, settings.Password);
            }
        }
    }
}
