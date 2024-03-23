using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace GeneralUtils
{
    public static class GeneralMethods
    {
        [ThreadStatic]
        private static WebDriverWait _wait;

        public static void InitializeBaseDriver()
        {
            _wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromSeconds(20));
        }

        public static void RetryActions()
        {
            BaseDriver.driver.Navigate().Refresh();
        }

        public static void AssertIsTrue(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception($"Condition is false, {message}");
            }
        }

        public static void Click(string cssSelector)
        {
            try
            {
                _wait
                    .Until(
                        SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                            (By.CssSelector(cssSelector))
                        )
                    )
                    .Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(
                    $"The element with the css {cssSelector} was not found. " + ex.Message
                );
            }
        }

        public static void ClickById(string cssSelector) =>
            _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                        (By.Id(cssSelector))
                    )
                )
                .Click();

        public static void RightClick(string cssSelector)
        {
            IWebElement element = _wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                    (By.CssSelector(cssSelector))
                )
            );
            Actions actions = new Actions(BaseDriver.driver);
            actions.ContextClick(element).Build().Perform();
        }

        public static void ClickAndEnter(string css)
        {
            _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                        (By.CssSelector(css))
                    )
                )
                .Click();
            BaseDriver.driver.FindElement(By.CssSelector(css)).SendKeys(Keys.Enter);
        }

        public static void Enter(string css)
        {
            BaseDriver.driver.FindElement(By.CssSelector(css)).SendKeys(Keys.Enter);
        }

        public static void ClickMultipeElements(string cssSelector)
        {
            _wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    (By.CssSelector(cssSelector))
                )
            );

            while (GetWebElementsWithoutWait(cssSelector).Count() > 0)
            {
                GetWebElementsWithoutWait(cssSelector).Last().Click();
            }
        }

        public static void doubleClick(string css, int index = 0)
        {
            Actions actions = new Actions(BaseDriver.driver);
            IWebElement elementLocator = BaseDriver.driver.FindElements(By.CssSelector(css))[index];
            actions.DoubleClick(elementLocator).Perform();
        }

        public static int ReturnCountOfElements(string css)
        {
            int count = BaseDriver.driver.FindElements(By.CssSelector(css)).Count;
            return count;
        }

        public static void ClickOnElementByXPath(string XPath) =>
            _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                        (By.XPath(XPath))
                    )
                )
                .Click();

        public static void ClickElementIsVisible(string cssSelector) =>
            _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                        (By.CssSelector(cssSelector))
                    )
                )
                .Click();

        public static void SendKeys(string cssSelector, string text)
        {
            try
            {
                IWebElement element = _wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                        (By.CssSelector(cssSelector))
                    )
                );
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

        public static void DragAndDropElement(string sourceCss, string targetCss)
        {
            IWebElement sourceElement = _wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                    (By.CssSelector(sourceCss))
                )
            );
            IWebElement targetElement = _wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                    (By.CssSelector(targetCss))
                )
            );

            Actions builder = new Actions(BaseDriver.driver);
            builder.DragAndDrop(sourceElement, targetElement).Perform();
        }

        public static void SendKey(string cssSelector, string text, int index = 0)
        {
            try
            {
                IWebElement element = GetWebElements(cssSelector)[index];
                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Sending keys to element with CSS selector '{cssSelector}' failed",
                    ex
                );
            }
        }

        public static void SendKeysUpload(string cssSelector, string text)
        {
            IWebElement element = _wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                    (By.CssSelector(cssSelector))
                )
            );
            element.SendKeys(text);
        }

        public static void SendKeysIwebElement(string text, IWebElement webElement)
        {
            webElement.Clear();
            webElement.SendKeys(text);
        }

        public static void WaitUntilTextIsVisibleAndClick(string css) =>
            _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                        (By.CssSelector(css))
                    )
                )
                .Click();

        public static void ClickAndSendKeys(string cssSelector, string text)
        {
            _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                        (By.CssSelector(cssSelector))
                    )
                )
                .Click();
            _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                        (By.CssSelector(cssSelector))
                    )
                )
                .SendKeys(text);
        }

        public static void SendKeysAndClick(
            string cssSelector_sendKeys,
            string cssSelector,
            string text
        )
        {
            _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                        (By.CssSelector(cssSelector_sendKeys))
                    )
                )
                .SendKeys(text);
            _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                        (By.CssSelector(cssSelector))
                    )
                )
                .Click();
        }

        public static IWebElement GetWebElement(string cssSelector)
        {
            WaitUntilElementIsDisplayed(cssSelector);
            IWebElement element = BaseDriver.driver.FindElement(By.CssSelector(cssSelector));
            return element;
        }

        public static IWebElement GetWebElementWithDelay(string cssSelector, int waitTime)
        {
            WaitUntilElementIsDisplayed(cssSelector, 0, 0, waitTime);
            IWebElement element = BaseDriver.driver.FindElement(By.CssSelector(cssSelector));
            return element;
        }

        public static IWebElement GetWebElementXpath(string xpath)
        {
            WaitUntilElementIsDisplayedXpath(xpath);
            IWebElement element = BaseDriver.driver.FindElement(By.XPath(xpath));
            return element;
        }

        public static IList<IWebElement> GetWebElementsXpath(string xpath)
        {
            WaitUntilElementIsVisibleXpath(xpath, 1);
            IList<IWebElement> elements = BaseDriver.driver.FindElements(By.XPath(xpath));
            return elements;
        }

        public static IList<IWebElement> GetWebElementsWithoutWait(string cssSelector)
        {
            IList<IWebElement> elements = BaseDriver.driver.FindElements(
                By.CssSelector(cssSelector)
            );
            return elements;
        }

        public static IList<IWebElement> GetWebElements(string cssSelector)
        {
            WaitUntilElementIsDisplayed(cssSelector);
            IList<IWebElement> elements = BaseDriver.driver.FindElements(
                By.CssSelector(cssSelector)
            );
            return elements;
        }

        public static void WaitUntilElementIsVisible(string css, int waitMin = 2)
        {
            var wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromMinutes(waitMin));
            var txt = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    (By.CssSelector(css))
                )
            ).Text;
        }

        public static void WaitUntilElementIsVisibleXpath(string xpath, int waitMin = 2)
        {
            _wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromMinutes(waitMin));
            var txt = _wait
                .Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                        (By.XPath(xpath))
                    )
                )
                .Text;
        }

        public static void WaitUntilTextIsVisible(string css, int waitMin = 2)
        {
            var wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromMinutes(waitMin));
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    (By.CssSelector(css))
                )
            );
        }

        public static bool WaitUntilTextIsVisibleInElement(
            string css,
            string expectedText,
            int waitTime = 30
        )
        {
            var wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromSeconds(waitTime));

            DateTime endWaitTime = DateTime.Now.Add(TimeSpan.FromSeconds(waitTime));

            try
            {
                IWebElement element = null;

                while (DateTime.Now < endWaitTime)
                {
                    element = wait.Until(driver =>
                    {
                        IWebElement tempElement = GetWebElementWithDelay(css, waitTime);

                        if (
                            tempElement != null
                            && tempElement.Displayed
                            && tempElement.Text.Contains(expectedText)
                        )
                        {
                            return tempElement; // Condition met, exit the wait
                        }

                        return null; // Keep retrying
                    });

                    if (element != null && element.Text.Contains(expectedText))
                    {
                        return true; // Condition met, exit the loop
                    }
                }

                return false; // Timeout reached
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsElementVisible(string css, int waitMin = 2)
        {
            var wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromMinutes(waitMin));

            try
            {
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(
                        (By.CssSelector(css))
                    )
                );
                IWebElement element = GetWebElement(css);

                return element.Displayed;
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"Timeout exception: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception: {ex.Message}");
                throw;
            }
        }

        public static void WaitUntilElementClickable(string css, int waitMin = 2)
        {
            var wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromMinutes(waitMin));
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    (By.CssSelector(css))
                )
            );
        }

        public static void WaitUntilInvisibilityOfElementLocated(string css, int waitMin = 2)
        {
            var wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromMinutes(waitMin));
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(
                    (By.CssSelector(css))
                )
            );
        }

        public static void WaitUntilElementIsExists(string css) =>
            _wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists((By.CssSelector(css)))
            );

        public static void MoveToElementAndClickAction(IWebElement webElement)
        {
            Actions actions = new Actions(BaseDriver.driver);
            actions.MoveToElement(webElement);
            actions.Click();
            actions.Build().Perform();
            actions.Click();
        }

        public static void WaitUntilTextIsEqualTo(
            string css,
            string expectedText,
            int FromSeconds = 20
        )
        {
            var wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromSeconds(FromSeconds));
            var element = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(css))
            );
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(
                    element,
                    expectedText
                )
            );
        }

        public static void MouseHoverAndClick(string css)
        {
            IWebElement mouseOver = BaseDriver.driver.FindElement(By.CssSelector(css));
            Actions actions = new Actions(BaseDriver.driver);
            actions.MoveToElement(mouseOver).Perform();
            GeneralMethods.WaitUntilElementClickable(css);
            GeneralMethods.Click(css);
        }

        public static void MouseHover(string css)
        {
            IWebElement mouseOver = BaseDriver.driver.FindElement(By.CssSelector(css));
            Actions actions = new Actions(BaseDriver.driver);
            actions.MoveToElement(mouseOver).Perform();
        }

        public static void ContextClick(string css)
        {
            Actions actions = new Actions(BaseDriver.driver);
            IWebElement webElement = BaseDriver.driver.FindElement(By.CssSelector(css));
            actions.ContextClick(webElement).Perform();
        }

        public static void WaitUntilElementIsDisplayedXpath(string Xpath)
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible((By.XPath(Xpath))));
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Waiting by Xpath  : '{Xpath}' failed , Exception : {ex.ToString()}"
                );
            }
        }

        public static int GetCountOfElementsByCss(string css)
        {
            try
            {
                WaitUntilElementIsDisplayed(css);
            }
            catch
            {
                return 0;
            }

            IList<IWebElement> listOfElements = BaseDriver.driver.FindElements(By.CssSelector(css));
            return listOfElements.Count;
        }

        public static bool IsElementDisplayed_StringCssWithTime(string css, int timer = 20)
        {
            for (int i = 0; i < timer; i++)
            {
                IList<IWebElement> element = BaseDriver.driver.FindElements(By.CssSelector(css));
                if (element.Count > 0)
                {
                    Console.WriteLine($"CSS : '{css}' is displayed after {i} seconds;");
                    return true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            throw new Exception($"CSS : '{css}' is not displayed after {timer} seconds;");
        }

        public static bool IsElementClickAble_StringCssWithTime(string css, int timer = 20)
        {
            for (int i = 0; i < timer; i++)
            {
                if (true)
                {
                    WaitUntilElementIsClickable(css);
                    return true;
                }

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            throw new Exception($"CSS : '{css}' is not clickable after {timer} seconds;");
        }

        public static bool IsElementIsNotExists(string css, int time)
        {
            var element = BaseDriver.driver.FindElement(By.CssSelector(css));
            for (int i = 0; i < time; i++)
            {
                if (element == null)
                {
                    return true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            return false;
        }

        public static bool IsElementDisplayed_StringCss(string css)
        {
            IList<IWebElement> element = BaseDriver.driver.FindElements(By.CssSelector(css));
            if (element.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsElementDisplayedXpath(string xpath, int wait)
        {
            _wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromSeconds(wait));
            Thread.Sleep(TimeSpan.FromSeconds(wait));

            IList<IWebElement> element = BaseDriver.driver.FindElements(By.XPath(xpath));
            if (element.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsElementDisplayed_Id(string css)
        {
            IList<IWebElement> element = BaseDriver.driver.FindElements(By.Id(css));
            if (element.Count > 0)
            {
                return true;
            }
            return false;
        }

        public static string GetAttributeOfElement(IWebElement element, string attribName)
        {
            string attribute = element.GetAttribute(attribName);
            if (attribute != null)
            {
                return attribute;
            }
            else
            {
                throw new Exception("Attribute = " + attribName + " - is null.");
            }
        }

        public static void WaitUntilElementIsClickable(string css, int timeout = 120)
        {
            _wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromSeconds(timeout));
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(css)));
        }

        public static void SelectValueFromDropDownReactAndNotSelectXpath //Text = "FBTestSec" //
        (string valueForSelect, string cssOpenDropDown, string cssListOfValuesInDropDown) // can't write in dropdown + type of drop down select
        {
            Click(cssOpenDropDown);
            //_driver.FindElement(By.CssSelector(cssOpenDropDown)).Click();

            WaitUntilElementIsDisplayed(cssListOfValuesInDropDown);
            IList<IWebElement> dropDownItems = BaseDriver.driver.FindElements(
                By.CssSelector(cssListOfValuesInDropDown)
            );
            dropDownItems[0].Click();
        }

        public static void WaitUntilElementIsDisplayed(
            string css,
            double waitSecondsBefore = 0,
            double waitSecondsAfter = 0,
            double wait = 30
        )
        {
            _wait = new WebDriverWait(BaseDriver.driver, TimeSpan.FromSeconds(wait));
            Thread.Sleep(TimeSpan.FromSeconds(waitSecondsBefore));
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible((By.CssSelector(css))));
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Waiting by CSS  : '{css}' failed , Exception : {ex.ToString()}"
                ); // error
            }
            Thread.Sleep(TimeSpan.FromSeconds(waitSecondsAfter));
        }

        public static void ClickOnElementUseActionClass(string css)
        {
            Thread.Sleep(TimeSpan.FromSeconds(3));
            IWebElement dropDowntest = BaseDriver.driver.FindElement(By.CssSelector(css));
            Actions builder = new Actions(BaseDriver.driver);
            builder.MoveToElement(dropDowntest).Perform();
            builder.MoveToElement(dropDowntest).Click().Perform();
        }

        public static void SendKeysUsingActionClass(string css, string txt)
        {
            Actions actions = new Actions(BaseDriver.driver);
            IWebElement element = BaseDriver.driver.FindElement(By.CssSelector(css));
            actions.SendKeys(element, txt).SendKeys(Keys.Enter).Perform();
        }

        public static void UploadFile(string file_path, string css_input)
        {
            BaseDriver.driver.FindElement(By.CssSelector(css_input)).SendKeys(file_path);
        }

        public static void UploadFileUsingJS(string file_path, string css)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)BaseDriver.driver;
            jsExecutor.ExecuteScript(
                "arguments[0].value = arguments[1];",
                BaseDriver.driver.FindElement(By.CssSelector(css)),
                file_path
            );
        }

        public static void KeyDownAndEnterUsingActionClass(string css)
        {
            Actions actions = new Actions(BaseDriver.driver);
            IWebElement element = BaseDriver.driver.FindElement(By.CssSelector(css));
            actions.SendKeys(Keys.Down).SendKeys(Keys.Enter).Perform();
        }

        public static void SendKeysEnterUsingActionClass(string css, string txt)
        {
            Actions actions = new Actions(BaseDriver.driver);
            IWebElement element = BaseDriver.driver.FindElement(By.CssSelector(css));
            actions.SendKeys(element, txt).SendKeys(Keys.Enter).Perform();
        }

        public static void EnterUsingActionClass(string css)
        {
            Actions actions = new Actions(BaseDriver.driver);
            IWebElement element = BaseDriver.driver.FindElement(By.CssSelector(css));
            actions.SendKeys(Keys.Enter).Perform();
        }

        public static void SendKeysUsingActionClassXpath(string xpath, string txt)
        {
            Actions actions = new Actions(BaseDriver.driver);
            IWebElement element = BaseDriver.driver.FindElement(By.XPath(xpath));
            actions.SendKeys(element, txt).SendKeys(Keys.Enter).Perform();
        }

        public static void SendKeysUsingActionClassNoEnter(string css, string txt)
        {
            Actions actions = new Actions(BaseDriver.driver);
            IWebElement element = BaseDriver.driver.FindElement(By.CssSelector(css));
            actions.SendKeys(element, txt).Perform();
        }

        public static void ClickOnElementByIWebElement(
            IWebElement element,
            double waitTimeInSecondsBeforeClick = 0,
            double waitTimeInSecondsAfterClick = 0
        )
        {
            Thread.Sleep(TimeSpan.FromSeconds(waitTimeInSecondsBeforeClick));
            element.Click();
            Thread.Sleep(TimeSpan.FromSeconds(waitTimeInSecondsAfterClick));
        }

        public static bool WaitUntilElementIsDisplayedWithOutException(
            string css,
            int timeOutSeconds = 30
        )
        {
            bool flag = false;

            for (int i = 0; i < timeOutSeconds; i++)
            {
                if (IsElementDisplayed_StringCss(css))
                {
                    return flag = true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return flag;
        }

        public static bool WaitUntilElementIsNotDisplayedWithOutException(
            string css,
            int timeOutSeconds = 30
        )
        {
            bool flag = false;

            for (int i = 0; i < timeOutSeconds; i++)
            {
                try
                {
                    IWebElement element = BaseDriver.driver.FindElement(By.CssSelector(css));
                }
                catch
                {
                    return flag = true; // the clip created
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return flag;
        }

        public static string GetTextOfIWebElement(string css, double timeout = 0)
        {
            WaitUntilElementIsDisplayed(css, timeout);
            string text = BaseDriver.driver.FindElement(By.CssSelector(css)).Text;
            return text;
        }

        public static string GetTextOfIWebElement(
            IWebElement element,
            string css,
            double timeout = 0
        )
        {
            WaitUntilElementIsDisplayed(css, timeout);
            string text = element.FindElement(By.CssSelector(css)).Text;
            return text;
        }

        public static string GetTextOfIWebElementXpath(string xpath)
        {
            WaitUntilElementIsDisplayedXpath(xpath);
            string text = BaseDriver.driver.FindElement(By.XPath(xpath)).Text;
            return text;
        }

        public static bool IsElementClickable_ByCSS(string css, int timeout = 20)
        {
            try
            {
                WaitUntilElementIsClickable(css, timeout);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool IsElementNotClickable_ByCSS(string css)
        {
            if (!IsElementClickable_ByCSS(css))
            {
                return true;
            }

            return false;
        }

        public static void GoToUrl(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                BaseDriver.driver.Navigate().GoToUrl(url);
                BaseDriver.driver.Manage().Window.Maximize();
            }
            else
            {
                throw new Exception($"Go to url : '{url}' is failed. Url is null or white space");
            }
        }

        public static bool IsElementDisplayedWithoutException(string css)
        {
            IList<IWebElement> element = BaseDriver.driver.FindElements(By.CssSelector(css));

            if (element.Count > 0)
            {
                return true;
            }

            return false;
        }

        public static void CustomWait(string css, int seconds = 120)
        {
            while (seconds > 0)
            {
                if (IsElementDisplayedWithoutException(css))
                {
                    Console.WriteLine($"css {css} is present");
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
                seconds--;
            }

            throw new Exception($"css {css} is not present after {seconds} seconds");
        }

        public static void CatchAndClickAction(string css, int topX)
        {
            WaitUntilElementIsExists(css);

            IList<IWebElement> list = BaseDriver.driver.FindElements(By.CssSelector(css));

            if (list.Count == 0)
            {
                throw new Exception("Event clips count = 0 ");
            }

            Actions action = new Actions(BaseDriver.driver);

            for (int i = 0; i < list.Count; i++)
            {
                if (topX == i)
                {
                    break;
                }
                action.Click(list[i]);
            }

            action.Build().Perform();
        }

        public static bool CatchAndClick(string css, string selectedItems_css, int topX)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible((By.CssSelector(css))));

            IList<IWebElement> items = BaseDriver.driver.FindElements(By.CssSelector(css));

            if (topX > items.Count)
            {
                topX = items.Count - 1;
            }

            for (int i = 0; i < topX; i++)
            {
                items[i].Click();
            }

            IList<IWebElement> selectedItems = BaseDriver.driver.FindElements(
                By.CssSelector(selectedItems_css)
            );

            return selectedItems.Count() > 1;
        }

        public static void ClearAndSendKeysWithActions(string css, string txt)
        {
            try
            {
                Actions actions = new Actions(BaseDriver.driver);
                var element = _wait.Until(
                    ExpectedConditions.ElementIsVisible((By.CssSelector(css)))
                );
                actions
                    .Click(element)
                    .SendKeys(Keys.Control + "a")
                    .SendKeys(Keys.Delete)
                    .SendKeys(txt)
                    .Perform();
            }
            catch (Exception ex)
            {
                throw new Exception($"Waiting by CSS: '{css}' failed, Exception: {ex.ToString()}");
            }
        }

        public static void ClearAndSendKeysPlusEnterWithActions(string css, string txt)
        {
            try
            {
                Actions actions = new Actions(BaseDriver.driver);
                var element = _wait.Until(
                    ExpectedConditions.ElementIsVisible((By.CssSelector(css)))
                );
                actions
                    .Click(element)
                    .SendKeys(Keys.Control + "a")
                    .SendKeys(Keys.Delete)
                    .SendKeys(txt)
                    .SendKeys(Keys.Enter)
                    .Perform();
            }
            catch (Exception ex)
            {
                throw new Exception($"Waiting by CSS: '{css}' failed, Exception: {ex.ToString()}");
            }
        }

        public static void ClearAndSendKeys(string css, string txt, int time = 10)
        {
            string actName = null;
            try
            {
                var element = _wait.Until(
                    ExpectedConditions.ElementIsVisible((By.CssSelector(css)))
                );
                for (int i = 0; time > i; i++)
                {
                    Thread.Sleep(1000);
                    element.Clear();
                    element.SendKeys(txt);
                    actName = element.GetAttribute("value");
                    if (txt.Contains(actName))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Waiting by CSS  : '{css}' failed , Exception : {ex.ToString()}"
                );
            }
            if (!txt.Equals(actName))
            {
                Console.WriteLine($"The name sent is {txt} but the name {actName} is written");
            }
        }

        public static string CreateGuid(int length)
        {
            Assert.That(length > 0, "the length of Guid smaller than 1");
            string guidString = Guid.NewGuid().ToString();
            return guidString.Substring(0, Math.Min(length, guidString.Length));
        }

        public static string CreateGuidWithCurrentDate(int length)
        {
            return CurrentDate() + "-" + CreateGuid(length);
        }

        public static string CurrentDate()
        {
            return DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        }

        public static void ClearAndSendKeysWithClick(string css, string txt)
        {
            Click(css);

            IWebElement el = BaseDriver.driver.FindElement(By.CssSelector(css));

            // clear
            el.SendKeys(Keys.Control + "a");
            el.SendKeys(Keys.Backspace);

            Click(css);

            foreach (var item in txt)
            {
                el.SendKeys(item.ToString());
                Thread.Sleep(100);
            }
        }

        public static void SelectFromDropDownByTxt(string css, string txt)
        {
            GeneralMethods.WaitUntilElementIsWithDelayDisplayed(css, 5);
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(css))).Click();
            var element = _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(css)));
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(txt);
        }

        public static void LoginYoutubeAndDropboxPP(
            string emailString,
            string email,
            string nextButtonEmail,
            string passwordString,
            string password,
            string access,
            string nextButtonPassword
        )
        {
            SendKeys(emailString, email);
            Click(nextButtonEmail);
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(passwordString)));
            SendKeys(passwordString, password);
            Click(nextButtonPassword);
            Click(access);
        }

        public static void LoginFaceboxAndInstagramPP(
            string emailString,
            string email,
            string passwordString,
            string password,
            string nextButton,
            string reconnectButton,
            string gotItButton
        )
        {
            SendKeys(emailString, email);
            SendKeys(passwordString, password);
            Click(nextButton);
            Click(reconnectButton);
            Click(gotItButton);
        }

        public static void LoginBoxPP(
            string emailString,
            string email,
            string passwordString,
            string password,
            string loginButton,
            string nextButton
        )
        {
            SendKeys(emailString, email);
            SendKeys(passwordString, password);
            Click(loginButton);
            Click(nextButton);
        }

        public static void LoginTwitterPP(
            string emailString,
            string email,
            string passwordString,
            string password,
            string loginButton
        )
        {
            SendKeys(emailString, email);
            SendKeys(passwordString, password);
            Click(loginButton);
        }

        public static void SelectSettingsPublishPoint(
            string PublishApp,
            string option,
            string privacy,
            string managePage
        )
        {
            switch (PublishApp)
            {
                case "YouTube":
                    BaseDriver
                        .driver.FindElement(By.CssSelector(privacy))
                        .SendKeys(option + Keys.Enter);
                    break;

                case "Facebook":
                    BaseDriver
                        .driver.FindElement(By.CssSelector(privacy))
                        .SendKeys("Wsctest" + Keys.Enter);
                    BaseDriver
                        .driver.FindElement(By.CssSelector(managePage))
                        .SendKeys(option + Keys.Enter);
                    break;

                case "Instagram":
                    BaseDriver
                        .driver.FindElement(By.CssSelector(privacy))
                        .SendKeys(option + Keys.Enter);
                    break;

                case "Email":
                    BaseDriver
                        .driver.FindElement(By.CssSelector(privacy))
                        .SendKeys("autoWSC@gmail.com" + Keys.Enter);
                    break;
            }
        }

        public static void SelectFromDropDownByXPath(string css)
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(css)));
                IWebElement dropdown = BaseDriver.driver.FindElement(By.XPath(css));
                dropdown.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                _wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.CssSelector("[class=' css-s6wecc-menu']")
                    )
                );
                BaseDriver.driver.FindElement(By.CssSelector("[class=' css-s6wecc-menu']")).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void SelectFromDropDownSendKeysPlusEnter(string css, string txt) =>
            _wait
                .Until(ExpectedConditions.ElementIsVisible((By.CssSelector(css))))
                .SendKeys(txt + Keys.Enter);

        public static void ClickOnElementUserJavaScript(string css)
        {
            WaitUntilElementIsExists(css);
            IList<IWebElement> element = BaseDriver.driver.FindElements(By.CssSelector(css));
            if (element.Count != 0)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)BaseDriver.driver;
                js.ExecuteScript("arguments[0].click()", element[0]);
            }
            else
            {
                throw new Exception(
                    $"ClickOnElementUserJavaScript failed . Count of css :  {css}  = 0"
                );
            }
        }

        public static bool ScrollToElementByName(
            string css,
            string expectedText,
            int maxIterations = 3
        )
        {
            bool isPresent = false;
            for (int i = 0; i < maxIterations; i++)
            {
                IList<IWebElement> contentElements = BaseDriver.driver.FindElements(
                    By.CssSelector(css)
                );

                foreach (IWebElement element in contentElements)
                {
                    if (element.Text.Contains(expectedText))
                    {
                        IJavaScriptExecutor _js = (IJavaScriptExecutor)BaseDriver.driver;
                        _js.ExecuteScript("arguments[0].scrollIntoView();", element);
                        isPresent = true;
                        return isPresent; // Exit the method if the expected text is found
                    }
                }

                IJavaScriptExecutor js = (IJavaScriptExecutor)BaseDriver.driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", contentElements.Last());
                Thread.Sleep(1000);
            }

            return isPresent; // Return the result after completing the specified iterations
        }

        public static void ScrollToElement(string css)
        {
            for (int i = 0; i < 3; i++)
            {
                IList<IWebElement> contentElements = BaseDriver.driver.FindElements(
                    By.CssSelector(css)
                );
                IJavaScriptExecutor js = (IJavaScriptExecutor)BaseDriver.driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", contentElements.Last());
                Thread.Sleep(1000);
            }
        }

        public static void ScrollToElementXpath(string xpath)
        {
            for (int i = 0; i < 3; i++)
            {
                IList<IWebElement> contentElements = BaseDriver.driver.FindElements(
                    By.XPath(xpath)
                );
                IJavaScriptExecutor js = (IJavaScriptExecutor)BaseDriver.driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", contentElements.Last());
                Thread.Sleep(1000);
            }
        }

        public static void ClickOnElementUserJavaScript(IWebElement webElement)
        {
            Assert.That(
                webElement,
                Is.Not.Null,
                "ClickOnElementUserJavaScript failed . webElement is null"
            );
            IJavaScriptExecutor js = (IJavaScriptExecutor)BaseDriver.driver;
            js.ExecuteScript("arguments[0].click()", webElement);
        }

        public static void ClickWithActionClass(string css, string name = null)
        {
            WaitUntilElementIsExists(css);
            Actions action = new Actions(BaseDriver.driver);
            IList<IWebElement> element = BaseDriver.driver.FindElements(By.CssSelector(css));
            foreach (var item in element)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (item.Text.ToLower().Equals(name.ToLower()))
                    {
                        item.Click();
                        return;
                    }
                }
                else
                {
                    action.Click().Build().Perform();
                }
            }
        }

        public static void OverAllPublishPoints(
            string css,
            string name = null,
            string deleteConfirmPublishPointString = null
        )
        {
            WaitUntilElementIsExists(css);
            Actions action = new Actions(BaseDriver.driver);
            IList<IWebElement> element = BaseDriver.driver.FindElements(By.CssSelector(css));
            foreach (IWebElement item in element)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (item.Text.ToLower().Equals(name.ToLower()))
                    {
                        IWebElement deleteMyPP = item.FindElement(By.CssSelector("div"));
                        GeneralMethods.ClickOnElementByIWebElement(deleteMyPP, 3, 2);
                        Thread.Sleep(1000);
                        IWebElement buttonConfirmPP = BaseDriver.driver.FindElement(
                            By.CssSelector(deleteConfirmPublishPointString)
                        );
                        GeneralMethods.ClickOnElementByIWebElement(buttonConfirmPP, 3, 2);
                        return;
                    }
                }
                else
                {
                    action.Click().Build().Perform();
                }
            }
        }

        public static void DeleteAllRules(
            string css,
            string deleteRule,
            string confirmButton,
            double waitTimeInSecondsBeforeClick,
            double waitTimeInSecondsAfterClick
        )
        {
            WaitUntilElementIsExists(css);
            Actions action = new Actions(BaseDriver.driver);
            IWebElement element = BaseDriver.driver.FindElement(By.CssSelector(css));
            IWebElement deleteRuleFromList = element.FindElement(By.CssSelector(deleteRule));
            GeneralMethods.ClickOnElementByIWebElement(
                deleteRuleFromList,
                waitTimeInSecondsBeforeClick,
                waitTimeInSecondsAfterClick
            );
            Click(confirmButton);
        }

        public static void ClickWithActionClassByTitle(string css, string name = null)
        {
            WaitUntilElementIsExists(css);
            Actions action = new Actions(BaseDriver.driver);
            IList<IWebElement> element = BaseDriver.driver.FindElements(By.CssSelector(css));
            foreach (IWebElement item in element)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (item.GetAttribute("title").ToLower().Equals(name.ToLower()))
                    {
                        item.Click();
                        return;
                    }
                }
                else
                {
                    action.Click().Build().Perform();
                }
            }
        }

        public static bool IsElementPresent_byXPath(string elementName)
        {
            try
            {
                BaseDriver.driver.FindElement(By.XPath(elementName));
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            return true;
        }

        public static string GetTextOfXpath(string Xpath)
        {
            WaitUntilElementIsDisplayedXpath(Xpath);
            string text = BaseDriver.driver.FindElement(By.XPath(Xpath)).Text;
            return text;
        }

        public static void SelectValueFromDropDownByText(string css_dropdown, string text) // relevant for css type = select
        {
            try
            {
                WaitUntilElementIsVisible(css_dropdown);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Waiting by CSS  : '{css_dropdown}' failed , Exception : {ex.ToString()}"
                );
            }

            SelectElement dropDown = new SelectElement(
                BaseDriver.driver.FindElement(By.CssSelector(css_dropdown))
            );
            dropDown.SelectByText(text);
        }

        public static void SelectValueFromDropDownReactAndNotSelectCss(
            string valueForSelect,
            string cssOpenDropDown,
            string cssListOfValuesInDropDown
        ) // can't write in dropdown + type of drop down select
        {
            ClickOnElementUseActionClass(cssOpenDropDown); // for server YURI
            WaitUntilElementIsDisplayed(cssListOfValuesInDropDown);
            IList<IWebElement> dropDownItems = BaseDriver.driver.FindElements(
                By.CssSelector(cssListOfValuesInDropDown)
            );
            foreach (var item in dropDownItems)
            {
                if (item.Text.Contains(valueForSelect))
                {
                    item.Click();
                    return;
                }
            }
            Assert.Fail($"Value with name {valueForSelect} is not present in drop down");
        }

        public static void SelectValueFromDropDownByTextUseActionClass(string css, string text) // relevant for drop down type = react and select (user can manually select data in drop down like text)
        {
            try
            {
                WaitUntilElementIsExists(css);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Waiting by CSS  : '{css}' failed , Exception : {ex.ToString()}"
                );
            }
            IWebElement dropDowntest = BaseDriver.driver.FindElement(By.CssSelector(css));
            Actions builder = new Actions(BaseDriver.driver);
            builder.MoveToElement(dropDowntest).Perform();
            builder.MoveToElement(dropDowntest).Click().Perform();
            builder.MoveToElement(dropDowntest).SendKeys(Keys.Delete).Perform();
            builder.MoveToElement(dropDowntest).SendKeys(text + Keys.Enter).Perform();
        }

        public static string SelectValueFromDropDownReactByIndex(
            int indexToClick,
            string cssOpenDropDown,
            string cssListOfValuesInDropDown
        ) // can't write in dropdown + type of drop down select
        {
            ClickOnElementUseActionClass(cssOpenDropDown); // for server YURI
            WaitUntilElementIsDisplayed(cssListOfValuesInDropDown);
            IList<IWebElement> dropDownItems = BaseDriver.driver.FindElements(
                By.CssSelector(cssListOfValuesInDropDown)
            );

            int index = 0;
            foreach (var item in dropDownItems)
            {
                if (index == indexToClick)
                {
                    string selectedValue = item.Text;
                    item.Click();
                    return selectedValue;
                }

                index++;
            }
            Assert.Fail($"Index {indexToClick} is not present in drop down");
            return null;
        }

        public static void NavigateToUrl(string url, string waitUntilElementDisplayed)
        {
            BaseDriver.driver.Navigate().GoToUrl(url);
            WaitUntilElementIsDisplayed(waitUntilElementDisplayed);
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        public static void VerifyAndDeleteFilesWithNameLike(string rootFolderPath, string fileType)
        {
            try
            {
                string[] fileList = Directory.GetFiles(rootFolderPath, fileType);
                if (fileList.Length != 0)
                {
                    foreach (string file in fileList)
                    {
                        System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                        File.Delete(file);
                    }

                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            catch
            {
                throw new Exception(
                    $"File with file type : {fileType} and filename like : "
                        + $"folder : {rootFolderPath}"
                );
            }
        }

        public static int GetNumberOfPages(string css)
        {
            int returnValue;
            //In case there is only one page
            if (!GeneralMethods.IsElementDisplayed_StringCss(css))
            {
                return 1;
            }

            var pageNumberString = BaseDriver.driver.FindElement(By.CssSelector(css)).Text;
            var numberOfPages = pageNumberString.Split('/')[1];

            if (!int.TryParse(numberOfPages, out returnValue))
            {
                throw new Exception($"Couldn't parse {pageNumberString} to int ");
            }

            return returnValue;
        }

        public static void WaitUntilFilteredIsDone(
            int numberOfClipsBeforeFilter,
            int timeout,
            string css
        )
        {
            for (int limit = 0; limit < timeout; limit++)
            {
                var afterFilter = GeneralMethods.GetNumberOfPages(css);

                if (numberOfClipsBeforeFilter == 1 && afterFilter == 1)
                {
                    return;
                }
                else if (afterFilter < numberOfClipsBeforeFilter)
                {
                    return;
                }

                Thread.Sleep(1000);
            }

            throw new Exception($"Filter by user video didn't succeed  after {timeout} seconds");
        }

        public static void WaitUntilElementIsWithDelayDisplayed(string css, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible((By.CssSelector(css))));
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Waiting by CSS  : '{css}' failed , Exception : {ex.ToString()}"
                );
            }
        }

        public static void WaitUntilElementIsWithDelayDisplayedXpath(string xpath, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible((By.XPath(xpath))));
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Waiting by Xpath  : '{xpath}' failed , Exception : {ex.ToString()}"
                );
            }
        }

        public static void AreEventsDisplaying(string css)
        {
            try
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                GeneralMethods.WaitUntilElementIsVisible(css);
            }
            catch
            {
                throw new Exception("Events are not displaying");
            }
        }

        public static bool ClearDefaultValueFromDropDownIfExist(
            string valueId,
            string selectGraphicCss,
            string selectClearIndicatorCss
        )
        {
            if (valueId == null)
            {
                return true;
            }

            var selectClearIndicatorGraphicCss = $"{selectGraphicCss} {selectClearIndicatorCss}";

            IWebElement selectClearIndicator;
            //In case overlay drop down disabled - Video has default dropdown
            try
            {
                selectClearIndicator = BaseDriver.driver.FindElement(
                    By.CssSelector(selectClearIndicatorGraphicCss)
                );
            }
            catch (Exception)
            {
                return true;
            }

            if (valueId.Equals("None"))
            {
                var selectGraphic = BaseDriver.driver.FindElement(By.CssSelector(selectGraphicCss));
                if (
                    !selectGraphic.Text.Contains("Select")
                    && !string.IsNullOrEmpty(selectGraphic.Text)
                )
                {
                    selectClearIndicator = BaseDriver.driver.FindElement(
                        By.CssSelector(selectClearIndicatorGraphicCss)
                    );
                    selectClearIndicator.Click();
                }

                return true;
            }

            return false;
        }

        public static void SelectValueFromDropDownReactByAttribID(
            string cssOpenDropDown,
            string cssListOfValuesInDropDown
        ) // can't write in dropdown + type of drop down select
        {
            Click(cssOpenDropDown);
            WaitUntilElementIsDisplayed(cssListOfValuesInDropDown);
            IList<IWebElement> dropDownItems = BaseDriver.driver.FindElements(
                By.CssSelector(cssListOfValuesInDropDown)
            );
            if (dropDownItems.Count > 0)
            {
                dropDownItems[0].Click();
            }
            else
            {
                throw new Exception(
                    $"Value {cssListOfValuesInDropDown} is not present in drop down"
                );
            }
        }

        public static void WaitInvisibilityOfElementLocated(string css, int secondsWait = 5)
        {
            Thread.Sleep(TimeSpan.FromSeconds(secondsWait));
            try
            {
                _wait.Until(ExpectedConditions.InvisibilityOfElementLocated((By.CssSelector(css))));
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Waiting by CSS  : '{css}' failed , Exception : {ex.ToString()}"
                );
            }
        }

        public static void SelectValueFromDropDownReactByAttribID //Text = "FBTestSec" //
        (
            string graphicId,
            string cssOpenDropDown,
            string cssListOfValuesInDropDown,
            string attributeType
        ) // can't write in dropdown + type of drop down select
        {
            Click(cssOpenDropDown);
            WaitUntilElementIsDisplayed(cssListOfValuesInDropDown);
            IList<IWebElement> dropDownItems = BaseDriver.driver.FindElements(
                By.CssSelector(cssListOfValuesInDropDown)
            );
            foreach (var item in dropDownItems)
            {
                if (item != null)
                {
                    ClickOnElementUserJavaScript(item);
                    break;
                }
                else
                {
                    throw new Exception($"{item} return bull");
                }
            }
        }

        public static void DragAndDrop(string sourceElement, string targetElement)
        {
            WaitUntilElementIsVisible(sourceElement);
            IWebElement source = GetWebElement(sourceElement);
            IWebElement target = GetWebElement(targetElement);

            Actions builder = new Actions(BaseDriver.driver);
            builder
                .MoveToElement(source)
                .ClickAndHold(source)
                .MoveToElement(target)
                .Release()
                .Build()
                .Perform();
        }

        public static string GetTextOfIWebElement(IWebElement element, double timeout = 0)
        {
            return element.Text;
        }

        public static bool IsElementDisplayed_StringCssWithTimeWithExCustom(
            string css,
            int timer = 10
        )
        {
            for (int i = 0; i < timer; i++)
            {
                IList<IWebElement> element = BaseDriver.driver.FindElements(By.CssSelector(css));
                if (element.Count > 0)
                {
                    Console.WriteLine($"CSS : '{css}' is displayed after {i} seconds;");
                    return true;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            return false;
        }

        public static bool ContainsAnyInList(string targetString, List<string> expecteditems)
        {
            foreach (string item in expecteditems)
            {
                if (targetString.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        public static void ClearAndSendKeysUsingKeyBinds(string cssSelector, string text)
        {
            try
            {
                IWebElement element = _wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
                        (By.CssSelector(cssSelector))
                    )
                );
                element.SendKeys(Keys.Control + "a");
                element.SendKeys(Keys.Delete);
                element.SendKeys(text);
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new TimeoutException(
                    $"Timeout waiting for element with CSS selector '{cssSelector}' to exist. {ex.Message}"
                );
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Method has failed css: {cssSelector} text: {text}. {ex.Message}"
                );
            }
        }
    }
}
