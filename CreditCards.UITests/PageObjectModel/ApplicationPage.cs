using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCards.UITests.PageObjectModel
{
    class ApplicationPage
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "http://localhost:44108/Apply";
        private const string PageTitle = "Credit Card Application - Credit Cards";

        //Constructor parameter

        public ApplicationPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public ReadOnlyCollection<string> ValidationErrorMessages
        {
            get
            {
                return Driver.FindElements(
                    By.CssSelector(".validation-summary-errors > ul > li"))
                    .Select(x => x.Text)
                    .ToList()
                    .AsReadOnly();
            }
        }

        public void ClearAge() => Driver.FindElement(By.Id("Age")).Clear();

        public void EnterFirstName(string firstName) => Driver.FindElement(By.Id("FirstName")).SendKeys("Stephen");

        public void EnterLastName(string lastName) => Driver.FindElement(By.Id("LastName")).SendKeys("Magwindiri");

        public void EnterFrequentFlyerNumber(string number) => Driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("AAAAA2");

        public void EnterAge(string age) => Driver.FindElement(By.Id("Age")).SendKeys(age);

        public void EnterGrossAnnualIncome(string income) => Driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("5000");

        public void ChooseMaritalStatusMarried() => Driver.FindElement(By.Id("Married")).Click();

        public void ChooseBusinessSourceTV()
        {
            IWebElement businessSourceSelectElement =
                Driver.FindElement(By.Id("BusinessSource"));

            SelectElement BusinessSource = new SelectElement(businessSourceSelectElement);
        }

        public void AcceptTerms() => Driver.FindElement(By.Id("TermsAccepted")).Click();
        public ApplicationCompletePage SubmitApplication()
        {
            Driver.FindElement(By.Id("SubmitApplication")).Click();
            return new ApplicationCompletePage(Driver);
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

            if (!pageHasLoaded)
            {
                throw new System.Exception($"failed to load the page. Page Url = '{Driver.Url}' page Source: \r\n {Driver.PageSource}");
            }

        }
    }
}
