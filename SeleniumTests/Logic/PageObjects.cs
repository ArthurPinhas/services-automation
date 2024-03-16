using GeneralUtils;
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
            driver.Navigate().GoToUrl(Elements.DockerContainerPage); // Now you can use Elements.DockerContainerPage directly
            // Thread.Sleep(5000);
            // IWebElement searchElem = driver.FindElement(By.CssSelector("[id='APjFqb']"));
            // searchElem.SendKeys("what is love");

            // searchElem.SendKeys(Keys.Enter);
            // Thread.Sleep(5000);
            // System.Console.WriteLine("test");
        }
    }
}
