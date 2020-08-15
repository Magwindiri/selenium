using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCards.UITests
{
    [TestClass]
    public class JavascriptExamples
    {
        [TestMethod]
        public void ClickOverlayedLink()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:44108/JSOverlay.html");

            DemoHelper.Pause();

            string script = "document.getElementById('HiddenLink').click();";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script);
            //driver.FindElement(By.Id("HiddenLink")).Click();
            DemoHelper.Pause();

            Assert.AreEqual("https://www.pluralsight.com/", driver.Url);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void GetOverlayedLinkText()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:44108/JSOverlay.html");

            DemoHelper.Pause();

            string script = "return document.getElementById('HiddenLink').innerHTML;";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string linkText = (string)js.ExecuteScript(script);
            //driver.FindElement(By.Id("HiddenLink")).Click();
            DemoHelper.Pause();

            Assert.AreEqual("Go to Pluralsight", linkText);

            driver.Close();
            driver.Quit();

        }
    }
}
