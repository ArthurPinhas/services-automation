using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GeneralUtils;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Elements;

namespace Logic
{
    public class PageObjects
    {
        private IWebDriver driver;
        private AppSettings settings;

        public PageObjects(IWebDriver driver, AppSettings settings)
        {
            this.driver = driver;
            this.settings = settings;
        }

        public async Task SendNotification(string message)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://ntfy.sh/{settings.NotifyTopic}";
                    StringContent content = new StringContent(message, Encoding.UTF8, "text/plain");
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    response.EnsureSuccessStatusCode();
                    Console.WriteLine("Notification sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"An error occurred while sending the notification: {ex.Message}"
                    );
                }
            }
        }

        public void VerifyDockerContainers()
        {
            GeneralMethods.ClickOnElementByXPath(Elements.StackButtonXpath);
            GeneralMethods.Click(Elements.ContainersButton);
            var elemList = GeneralMethods.GetWebElements(Elements.ContainerStatus);
            bool anyAssertionFailed = false;

            foreach (var container in elemList)
            {
                var status = GeneralMethods.GetTextOfIWebElement(container);
                if (!(status.Equals("running") || status.Equals("healthy")))
                {
                    Console.WriteLine($"Assertion failed: Actual status was : {status}");
                    SendNotification(
                            $"Not all containers are up or in good health, Error returned from service is : {status}"
                        )
                        .Wait();
                    anyAssertionFailed = true;
                }
            }

            if (!anyAssertionFailed)
            {
                SendNotification("All containers are up and in good health.").Wait();
            }
        }
    }
}
