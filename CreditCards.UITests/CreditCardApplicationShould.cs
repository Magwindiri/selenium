using CreditCards.UITests.PageObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCards.UITests
{
    [TestClass]
    [TestCategory("Credit Card Application")]
    public class CreditCardApplicationShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";
        //private readonly ItestOutputHelper outputHelper

       //Navigate between Page object Models
        [TestMethod]
        [Description("Click Apply low Rate")]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            IWebDriver driver = new ChromeDriver();

            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            ApplicationPage applicationPage = homePage.ClickApplyNewLowRateLink();

            applicationPage.EnsurePageLoad();

            driver.Close();
            driver.Quit();
        }

        //Not going to refactor it
        [TestMethod]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);
            DemoHelper.Pause();

            driver.FindElement(By.CssSelector("[data-slide='next']")).Click();

            //DemoHelper.Pause();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            IWebElement applyLink =
                wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
            applyLink.Click();


            //driver.FindElement(By.LinkText("Easy: Apply Now!")).Click();

            DemoHelper.Pause();

            Assert.AreEqual("Credit Card Application - Credit Cards", driver.Title);

            Assert.AreEqual(ApplyUrl, driver.Url);

            driver.Close();
            driver.Quit();
        }

        //Refactoring this test and by encapsulating explicit waits
        [TestMethod]
        public void BeInitiatedFromHomePage_EasyApplication_PrebuiltConditions()
        {
            IWebDriver driver = new ChromeDriver();

            var homePage = new HomePage(driver);
            homePage.NavigateTo();

            homePage.WaitForEasyApplicationCarouselPage();

            ApplicationPage applicationPage = homePage.ClickApplyEasyApplicationLink();

            applicationPage.EnsurePageLoad();

            driver.Close();
            driver.Quit();

        }

        [TestMethod]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);

            driver.Navigate().GoToUrl(HomeUrl);

            //DemoHelper.Pause();

            //driver.FindElement(By.CssSelector("[data-slide='next']")).Click();
            //DemoHelper.Pause(1000);

            //driver.FindElement(By.CssSelector("[data-slide='next']")).Click();

            //DemoHelper.Pause(1000);

            driver.FindElement(By.ClassName("customer-service-apply-now")).Click();

            //Setting up an implicit wait.



            Assert.AreEqual("Credit Card Application - Credit Cards", driver.Title);
            Assert.AreEqual(ApplyUrl, driver.Url);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);

            DemoHelper.Pause();

            IWebElement randomGreatingApplyLink = driver.FindElement(By.PartialLinkText("- Apply Now!"));
            randomGreatingApplyLink.Click();

            DemoHelper.Pause();

            Assert.AreEqual("Credit Card Application - Credit Cards", driver.Title);
            Assert.AreEqual(ApplyUrl, driver.Url);

            DemoHelper.Pause();

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        [Description("Random greating by XPATH")] //Absolute path not usually recommended
        public void BeInitiatedFromHomePage_RandomGreating_UsinXPATH()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);
            DemoHelper.Pause();

            IWebElement randomGreatingApplyLink = driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a"));
            randomGreatingApplyLink.Click();

            DemoHelper.Pause();

            Assert.AreEqual("Credit Card Application - Credit Cards", driver.Title);
            Assert.AreEqual(ApplyUrl, driver.Url);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        [Description("Random greating by XPATH Relative")] //Relative path not usually recommended
        public void BeInitiatedFromHomePage_RandomGreating_Relative_XPATH()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl(HomeUrl);
            DemoHelper.Pause();

            IWebElement randomGreatingApplyLink = driver.FindElement(By.XPath("//a[text() [contains(. ,' - Apply Now!')]]"));
            randomGreatingApplyLink.Click();

            DemoHelper.Pause();

            Assert.AreEqual("Credit Card Application - Credit Cards", driver.Title);
            Assert.AreEqual(ApplyUrl, driver.Url);

            driver.Close();
            driver.Quit();
        }
    }
}
