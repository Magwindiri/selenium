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
using CreditCards.UITests.PageObjectModel;

namespace CreditCards.UITests
{
    [TestClass]
    public class CreditCardWebAppShould
    {
        private const string HomeUrl = "http://localhost:44108/Home";
        private const string AboutUrl = "http://localhost:44108/Home/About";
        private const string HomeTitle = "Home Page - Credit Cards";
        private const string ApplyUrl = "http://localhost:44108/Apply";

        [TestMethod]
        public void CreditCardWebAppShouldPage()
        {

            IWebDriver driver = new ChromeDriver();

            var homePage = new HomePage(driver);
            homePage.NavigateTo();

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
        public void ReloadHomePageOnBack()
        {
            //navigating backwards
            IWebDriver driver = new ChromeDriver();

            var homePage = new HomePage(driver);
            homePage.NavigateTo();
            
            string initialToken = homePage.GenerationToken;

            driver.Navigate().GoToUrl(AboutUrl);
            driver.Navigate().Back();

            homePage.EnsurePageLoad();

            //TODO: Assert that the page was reloaded
            string reloadToken = homePage.GenerationToken;

            Assert.AreNotEqual(initialToken, reloadToken);

            driver.Close();
            driver.Quit();

        }

        [TestMethod]
        public void DisplayProductsAndRates()
        {
            IWebDriver driver = new ChromeDriver();

            //Creating a new instance of HomePame Object Model
            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            DemoHelper.Pause();



            Assert.AreEqual("Easy Credit Card", homePage.Products[0].name);
            Assert.AreEqual("20% APR", homePage.Products[0].interestRate);

            Assert.AreEqual("Silver Credit Card", homePage.Products[1].name);
            Assert.AreEqual("18% APR", homePage.Products[1].interestRate);

            Assert.AreEqual("Gold Credit Card", homePage.Products[2].name);
            Assert.AreEqual("17% APR", homePage.Products[2].interestRate);


            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void BeSubmittedWhenValid()
        {
            const string FirstName = "Stephen";
            const string LastName = "Magwindiri";
            const string Number = "AAAAA2";
            const string Age = "18";
            const string Income = "5000";


            IWebDriver driver = new ChromeDriver();

            var applicationPage = new ApplicationPage(driver);
            applicationPage.NavigateTo();

            applicationPage.EnterFirstName(FirstName);
            applicationPage.EnterLastName(LastName);
            applicationPage.EnterFrequentFlyerNumber(Number);
            applicationPage.EnterAge(Age);
            applicationPage.EnterGrossAnnualIncome(Income);
            applicationPage.ChooseMaritalStatusMarried();
            applicationPage.ChooseBusinessSourceTV();
            applicationPage.AcceptTerms();

            ApplicationCompletePage applicationCompletePage =
            applicationPage.SubmitApplication();

            applicationCompletePage.EnsurePageLoad();

            //Assert.AreEqual("Application Complete - Credit Cards", driver.Title);
            Assert.AreEqual("ReferredToHuman", applicationCompletePage.Decision);
            Assert.IsNotNull(applicationCompletePage.ReferenceNumber);
            Assert.AreEqual($"{FirstName} {LastName}", applicationCompletePage.FullName);
            Assert.AreEqual(Age, applicationCompletePage.Age);
            Assert.AreEqual(Income, applicationCompletePage.Income);
            Assert.AreEqual("Married", applicationCompletePage.RelationshipStatus);
            Assert.AreEqual("Declined To Comment", applicationCompletePage.BusinessSource);

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

            var applicationPage = new ApplicationPage(driver);
            applicationPage.NavigateTo();


            applicationPage.EnterFirstName(firstName);
        
            applicationPage.EnterFrequentFlyerNumber("AAAAA2");
            applicationPage.EnterAge(invalidAge);
            applicationPage.EnterGrossAnnualIncome("5000");
            applicationPage.ChooseMaritalStatusMarried();
            applicationPage.ChooseBusinessSourceTV();
            applicationPage.AcceptTerms();
            applicationPage.SubmitApplication();

            Assert.AreEqual(2, applicationPage.ValidationErrorMessages.Count);
            Assert.AreEqual("Please provide a last name", applicationPage.ValidationErrorMessages);
            Assert.AreEqual("You must be at least 18 years old", applicationPage.ValidationErrorMessages);

            //fix errors
            driver.FindElement(By.Id("LastName")).SendKeys("Magwindiri");

            driver.FindElement(By.Id("Age")).Clear();
            driver.FindElement(By.Id("Age")).SendKeys(validAge);

            //Resubmit the form
            driver.FindElement(By.Id("SubmitApplication")).Click();

            //Check form submitted
            Assert.AreEqual("Application Complete - Credit Cards", driver.Title);
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

            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            homePage.ClickContactFooterLink();

            ReadOnlyCollection<string> allTabs = driver.WindowHandles;

            string homePageTab = allTabs[0];
            string contactTab = allTabs[1];

            driver.SwitchTo().Window(contactTab);

            // need to find the equivalence of this Xunit for MsTest
            //Assert.IsTrue(Actual.EndsWith("/Home/Contact"), driver.Url);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void AlertIfChatClosed()
        {
            //Dealing with Alert boxes
            IWebDriver driver = new ChromeDriver();

            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            homePage.ClickLiveChatFooterLink();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            Assert.AreEqual("Live chat is currently closed.", alert.Text);

            alert.Accept();

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void NavigateToAboutUsWhenCancelClicked()
        {
            //Ignore this one
            IWebDriver driver = new ChromeDriver();

            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            //driver.Navigate().GoToUrl(HomeUrl);
            // Assert.AreEqual(HomeTitle, driver.Title);
            homePage.ClickLearnAboutUs();

            //driver.FindElement(By.Id("LearnAboutUs")).Click();

            DemoHelper.Pause();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());

            alertBox.Accept();
            //TODO
            Assert.AreEqual(AboutUrl, driver.Url);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void NotNavigateToAboutUsWhenCancelClicked()
        {
            //Dealing with Alert boxes
            IWebDriver driver = new ChromeDriver();

            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            homePage.ClickLearnAboutUs();

            //driver.Navigate().GoToUrl(HomeUrl);
            //Assert.AreEqual(HomeTitle, driver.Title);

           // driver.FindElement(By.Id("LearnAboutUs")).Click();

            //DemoHelper.Pause();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());

            alertBox.Dismiss();

            homePage.EnsurePageLoad();

            //TODO
            
            Assert.AreEqual(HomeUrl, driver.Url);

            DemoHelper.Pause();

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void NotDisplayCookieUseMessage()
        {
            IWebDriver driver = new ChromeDriver();

            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            //driver.Navigate().GoToUrl(HomeUrl);

            driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));

            driver.Navigate().Refresh();

            Assert.IsFalse(homePage.IsCookieMessagePresent);

            driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");

            driver.Navigate().Refresh();

            Assert.IsTrue(homePage.IsCookieMessagePresent);


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
