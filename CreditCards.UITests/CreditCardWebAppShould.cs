﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            ReadOnlyCollection<IWebElement> tableCells =  driver.FindElements(By.TagName("td"));
            

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

    }
}
