﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCards.UITests.PageObjectModel
{
    class ApplicationCompletePage
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "http://localhost:44108/Apply";
        private const string PageTitle = "Application Complete - Credit Cards";

        //Constructor parameter

        public ApplicationCompletePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public string Decision => Driver.FindElement(By.Id("Decision")).Text;

        public string ReferenceNumber => Driver.FindElement(By.Id("ReferenceNumber")).Text;

        public string FullName => Driver.FindElement(By.Id("FullName")).Text;

        public string Age => Driver.FindElement(By.Id("Age")).Text;

        public string Income => Driver.FindElement(By.Id("Income")).Text;

        public string RelationshipStatus => Driver.FindElement(By.Id("RelationshipStatus")).Text;

        public string BusinessSource => Driver.FindElement(By.Id("BusinessSource")).Text;

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
