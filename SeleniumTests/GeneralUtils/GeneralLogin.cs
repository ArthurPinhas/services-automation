using OpenQA.Selenium;
using SeleniumTests.Elements;

namespace GeneralUtils
{
    public static class GeneralLogin
    {
        [ThreadStatic]
        public static IWebDriver _driver;

        public static void InitializeBaseDriver(IWebDriver driver)
        {
            _driver = driver;
        }

        public static void LoginToPortainer(string url, string userName, string password)
        {
            GeneralMethods.GoToUrl(url);
            GeneralMethods.SendKeys(Elements.UserInput, userName);
            GeneralMethods.SendKeys(Elements.PassInput, password);
            GeneralMethods.Click(Elements.LoginButton);
            GeneralMethods.WaitUntilElementIsDisplayed(Elements.PortainerSearchXpath);
        }
    }
}
