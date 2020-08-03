using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using ApprovalTests.Reporters.Windows;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;

namespace CreditCards.UITests
{
    [TestClass]
    public class CreditCardWebAppShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string AboutUrl = "http://localhost:44108/Home/About";
        private const string HomeTitle = "Home Page - Credit Cards";
        private const string ApplyUrl = "http://localhost:44108/Apply";

        [TestMethod]
        public void CreditCardWebAppShouldPage()
        {
            string actualResults;
            //string expectedResults = "Home Page-Credit Cards";

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(HomeUrl);
            driver.Manage().Window.Maximize();
            DemoHelper.Pause();
            driver.Manage().Window.Minimize();
            DemoHelper.Pause();
            driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
            DemoHelper.Pause();
            driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
            DemoHelper.Pause();
            driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
            DemoHelper.Pause();
            driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
            DemoHelper.Pause();

            driver.Manage().Window.FullScreen();

            actualResults = driver.Title;

            Assert.AreEqual(HomeTitle, actualResults);
            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void CreditCardWebAppShouldPage2()
        {
            //string actualResults;
            //string expectedResults = "Home Page-Credit Cards";

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(HomeUrl);
            driver.Manage().Window.Maximize();
            DemoHelper.Pause();
            //actualResults = driver.Title;

            Console.WriteLine(driver.Title);
            Assert.AreEqual("Home Page - Credit Cards", driver.Title);
            Console.WriteLine(HomeUrl, driver.Url);
            Assert.AreEqual(HomeUrl, driver.Url);
            driver.Close();
            driver.Quit();
        }

        [TestMethod]

        public void ReloadHomePage()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(HomeUrl);
            driver.Manage().Window.Maximize();
            DemoHelper.Pause();

            driver.Navigate().Refresh();

            Assert.AreEqual("Home Page - Credit Cards", driver.Title);
            Console.WriteLine(HomeTitle, driver.Url);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]

        public void ReloadHomePageOnBack()
        {
            //navigating backwards
            IWebDriver driver = new ChromeDriver();

            DemoHelper.Pause();

            driver.Navigate().GoToUrl(HomeUrl);

            IWebElement generationTokenElement =
            driver.FindElement(By.Id("GenerationToken"));

            string initialToken = generationTokenElement.Text;

            DemoHelper.Pause();
            driver.Navigate().GoToUrl(AboutUrl);
            DemoHelper.Pause();
            driver.Navigate().Back();


            Assert.AreEqual(HomeTitle, driver.Title);
            Console.WriteLine(HomeUrl, driver.Url);
            Assert.AreEqual(HomeUrl, driver.Url);

            //TODO: Assert that the page was reloaded
            string reloadToken = driver.FindElement(By.Id("GenerationToken")).Text;

            Assert.AreNotEqual(initialToken, reloadToken);

            driver.Close();
            driver.Quit();

        }

        [TestMethod]
        public void ReloadHomePageOnForward()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(AboutUrl);
            DemoHelper.Pause();

            driver.Navigate().GoToUrl(HomeUrl);

            DemoHelper.Pause();

            driver.Navigate().Back();

            DemoHelper.Pause();

            driver.Navigate().Forward();

            Assert.AreEqual(HomeTitle, driver.Title);
            Console.WriteLine(HomeUrl, driver.Url);
            Assert.AreEqual(HomeUrl, driver.Url);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void DisplayProductsAndRates()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);

            DemoHelper.Pause();

            ReadOnlyCollection<IWebElement> tableCells = driver.FindElements(By.TagName("td"));


            Assert.AreEqual("Easy Credit Card", tableCells[0].Text);
            Assert.AreEqual("20% APR", tableCells[1].Text);

            Assert.AreEqual("Silver Credit Card", tableCells[2].Text);
            Assert.AreEqual("18% APR", tableCells[3].Text);

            Assert.AreEqual("Gold Credit Card", tableCells[4].Text);
            Assert.AreEqual("17% APR", tableCells[5].Text);


            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void BeSubmittedWhenValid()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(ApplyUrl);


            driver.FindElement(By.Id("FirstName")).SendKeys("Stephen");

            driver.FindElement(By.Id("LastName")).SendKeys("Magwindiri");

            driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("AAAAA2");

            driver.FindElement(By.Id("Age")).SendKeys("18");

            driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("5000");

            driver.FindElement(By.Id("Married")).Click();

            DemoHelper.Pause(5000);

            IWebElement businessSourceSelectElement =
                driver.FindElement(By.Id("BusinessSource"));

            SelectElement BusinessSource = new SelectElement(businessSourceSelectElement);

            //checked default element selected option is correct
            Assert.AreEqual("I'd Rather Not Say", BusinessSource.SelectedOption.Text);

            //Get all of the options

            foreach (IWebElement option in BusinessSource.Options)
            {
                Console.WriteLine($"Value: {option.GetAttribute("value")} Text: {option.Text}");
            }

            Assert.AreEqual(5, BusinessSource.Options.Count);

            //Select Options
            BusinessSource.SelectByValue("Email");
            DemoHelper.Pause();

            BusinessSource.SelectByText("Internet Search");
            DemoHelper.Pause();

            BusinessSource.SelectByIndex(4);

            driver.FindElement(By.Id("TermsAccepted")).Click();

            //driver.FindElement(By.Id("SubmitApplication")).Click();

            driver.FindElement(By.Id("Married")).Submit();

            DemoHelper.Pause(5000);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void BeSubmittedWhenValidationErrorsCorrected()
        {
            const string firstName = "Stephen";
            const string invalidAge = "17";
            const string validAge = "18";
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(ApplyUrl);


            driver.FindElement(By.Id("FirstName")).SendKeys(firstName);

            driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("AAAAA2");

            driver.FindElement(By.Id("Age")).SendKeys(invalidAge);

            driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("5000");

            driver.FindElement(By.Id("Married")).Click();

            IWebElement businessSourceSelectElement =
                driver.FindElement(By.Id("BusinessSource"));

            SelectElement BusinessSource = new SelectElement(businessSourceSelectElement);
            BusinessSource.SelectByValue("Email");

            driver.FindElement(By.Id("TermsAccepted")).Click();

            driver.FindElement(By.Id("SubmitApplication")).Click();
            DemoHelper.Pause();

            var validationErrors =
                driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"));
            Assert.AreEqual(2, validationErrors.Count);
            Assert.AreEqual("Please provide a last name", validationErrors[0].Text);
            Assert.AreEqual("You must be at least 18 years old", validationErrors[1].Text);

            //fix errors
            driver.FindElement(By.Id("LastName")).SendKeys("Magwindiri");

            driver.FindElement(By.Id("Age")).Clear();
            driver.FindElement(By.Id("Age")).SendKeys(validAge);

            //Resubmit the form
            driver.FindElement(By.Id("SubmitApplication")).Click();

            //Check form submitted
            //Assert.StartsWith("Application Complete", driver.Title);
            Assert.AreEqual("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
            Assert.IsNotNull(driver.FindElement(By.Id("ReferenceNumber")).Text);
            Assert.AreEqual("Stephen Magwindiri", driver.FindElement(By.Id("FullName")).Text);
            Assert.AreEqual("18", driver.FindElement(By.Id("Age")).Text);
            Assert.AreEqual("5000", driver.FindElement(By.Id("Income")).Text);
            Assert.AreEqual("Married", driver.FindElement(By.Id("RelationshipStatus")).Text);
            Assert.AreEqual("Email", driver.FindElement(By.Id("BusinessSource")).Text);
            //Assert.StartsWith


            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void OpenContactFooterLinkInNewTab()
        {
            //Switch tabs
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);

            driver.FindElement(By.Id("ContactFooter")).Click();
            DemoHelper.Pause();

            ReadOnlyCollection<string> allTabs = driver.WindowHandles;

            string homePageTab = allTabs[0];
            string contactTab = allTabs[1];

            driver.SwitchTo().Window(contactTab);

            // need to find the equivalence of this Xunit for MsTest
            //Assert.Endswith("/Home/Contact", driver.Url);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void AlertIfChatClosed()
        {
            //Dealing with Alert boxes
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);

            driver.FindElement(By.Id("LiveChat")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            Assert.AreEqual("Live chat is currently closed.", alert.Text);

            DemoHelper.Pause();

            alert.Accept();

            DemoHelper.Pause();

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void NavigateToAboutUsWhenCancelClicked()
        {
            //Ignore this one
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);
            Assert.AreEqual(HomeTitle, driver.Title);

            driver.FindElement(By.Id("LearnAboutUs")).Click();

            DemoHelper.Pause();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());

            alertBox.Accept();

            Assert.AreEqual(HomeTitle, driver.Title);

            DemoHelper.Pause();

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void NotNavigateToAboutUsWhenCancelClicked()
        {
            //Dealing with Alert boxes
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);
            Assert.AreEqual(HomeTitle, driver.Title);

            driver.FindElement(By.Id("LearnAboutUs")).Click();

            DemoHelper.Pause();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());

            alertBox.Dismiss();

            Assert.AreEqual(HomeTitle, driver.Title);

            DemoHelper.Pause();

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void NotDisplayCookieUseMessage()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);

            driver.Manage().Cookies.AddCookie(new Cookie("CookiesBeingUsed", "true"));

            driver.Navigate().Refresh();

            ReadOnlyCollection<IWebElement> message = 
                driver.FindElements(By.Id("CookiesBeingUsed"));

            Assert.IsNotNull(message);


            driver.Close();
            driver.Quit();

        }

        [TestMethod]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void RenderHomePage()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);

            ITakesScreenshot screenShotDriver = (ITakesScreenshot)driver;

            Screenshot screenshot = screenShotDriver.GetScreenshot();

            screenshot.SaveAsFile("homepage.Jpeg", ScreenshotImageFormat.Jpeg);

            FileInfo file = new FileInfo("homepage.Jpeg");

            Approvals.Verify(file);

            driver.Close();
            driver.Quit();
        }
    }
}
