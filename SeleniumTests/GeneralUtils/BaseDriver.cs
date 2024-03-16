using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace GeneralUtils
{
    public class BaseDriver
    {
        public static IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            // Configure ChromeOptions
            ChromeOptions options = new ChromeOptions();
            // options.AddArguments("--headless");
            options.AddArguments("--disable-blink-features=AutomationControlled");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("window-size=1920,1080");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                driver.Dispose();
            }
        }
    }
}
