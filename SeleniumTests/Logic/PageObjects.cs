using GeneralUtils;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Elements;

namespace Logic
{
    public class PageObjects
    {
        private IWebDriver driver;

        public PageObjects(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void VerifyDockerContainers()
        {
            Thread.Sleep(5000);
            // IWebElement searchElem = driver.FindElement(By.CssSelector("[id='APjFqb']"));
            // searchElem.SendKeys("what is love");

            // searchElem.SendKeys(Keys.Enter);
            // Thread.Sleep(5000);
            // System.Console.WriteLine("test");
        }
    }
}
