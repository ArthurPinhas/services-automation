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
            Thread.Sleep(2000);
        }

        public static void SendKeys(string cssSelector, string text)
        {
            try
            {
                IWebElement element = GeneralMethods.GetWebElement(cssSelector);
                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Method SendKeys failed css: {cssSelector} text: {text}, {ex.Message}"
                );
            }
        }
    }
}
