
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CreditCards.UITests.PageObjectModel
{
    class HomePage
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "http://localhost:44108/Home";
        private const string PageTitle = "Home Page - Credit Cards";

        //Constructor parameter

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        //Product property
        public ReadOnlyCollection<(string name, string interestRate)> Products
        {
            get
            {
                var products = new List<(string name, string interestRate)>();
                var productCells = Driver.FindElements(By.TagName("td"));

                for (int i = 0; i < productCells.Count - 1; i += 2)
                {
                    string name = productCells[i].Text;
                    string interestRate = productCells[i + 1].Text;
                    products.Add((name, interestRate));
                }

                return products.AsReadOnly();
            }
        }

        public string GenerationToken => Driver.FindElement(By.Id("GenerationToken")).Text;

        //TODO
        public bool IsCookieMessagePresent => Driver.FindElements(By.Id("CookiesBeingUsed")).Any();

        public void ClickContactFooterLink() => Driver.FindElement(By.Id("ContactFooter")).Click();

        public void ClickLiveChatFooterLink() => Driver.FindElement(By.Id("LiveChat")).Click();

        public void ClickLearnAboutUs() => Driver.FindElement(By.Id("LearnAboutUs")).Click();

        public ApplicationPage ClickApplyNewLowRateLink()
        {
            Driver.FindElement(By.Name("ApplyLowRate")).Click();
            return new ApplicationPage(Driver);
        }

        public ApplicationPage ClickApplyEasyApplicationLink()
        {
            Driver.FindElement(By.LinkText("Easy: Apply Now!")).Click();
            return new ApplicationPage(Driver);
        }

        public void WaitForEasyApplicationCarouselPage()
        {
            WebDriverWait wait =
            new WebDriverWait(Driver, TimeSpan.FromSeconds(11));

            IWebElement applyLink =
                wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoad();
        }

        //Method to ensure page is loading
        public void EnsurePageLoad()
        {
            bool pageHasLoaded = (Driver.Url == PageUrl) && (Driver.Title == PageTitle);

            if(!pageHasLoaded)
            {
                throw new System.Exception($"failed to load the page. Page Url = '{Driver.Url}' page Source: \r\n {Driver.PageSource}");
            }
        }
    }
}
