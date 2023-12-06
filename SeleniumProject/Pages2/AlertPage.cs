using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Pages2
{
    public class AlertPage
    {
        readonly IWebDriver _driver;
        readonly WebDriverWait _wait;

        readonly By jsAlert = By.XPath("//button[contains(text(),'Click for JS Alert')]");
        readonly By jsConfirm = By.XPath("//button[contains(text(),'Click for JS Confirm')]");
        readonly By jsPrompt= By.XPath("//button[contains(text(),'Click for JS Prompt')]");
        readonly By resultLable = By.Id("result");


        public AlertPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public void ClickJsAlert()
        {
            _driver.FindElement(jsAlert).Click();
            _driver.SwitchTo().Alert().Accept();

            Assert.AreEqual(_driver.FindElement(resultLable).Text, "You successfully clicked an alert");
        }

        public void ClickJsConfirm()
        {
            //Accept the Confirmation
            _driver.FindElement(jsConfirm).Click();
            _driver.SwitchTo().Alert().Accept();

            Assert.AreEqual(_driver.FindElement(resultLable).Text, "You clicked: Ok");
            Thread.Sleep(3000);

            //Cancel the Confirmation
            _driver.FindElement(jsConfirm).Click();
            _driver.SwitchTo().Alert().Dismiss();

            Assert.AreEqual(_driver.FindElement(resultLable).Text, "You clicked: Cancel");

        }

        public void ClickJsPrompt()
        {
            _driver.FindElement(jsPrompt).Click();
            _driver.SwitchTo().Alert().SendKeys("Test");
            _driver.SwitchTo().Alert().Accept();

            Assert.That(_driver.FindElement(resultLable).Text, Is.EqualTo("You entered: Test"));
        }
    }
}
