using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System.Drawing.Printing;
using System;
using Utils.Initiates;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Diagnostics;
using OpenQA.Selenium.Interactions;

namespace seleniumCs
{
    [TestFixture]
    public class UnitTest : Initiates
    {
        private ChromeDriver _driver;
        private WebDriverWait _wait;
        private Actions _actions;
        private string _url;

        [SetUp]
        public void Setup()
        {

            //Below code is to get the drivers folder path dynamically.

            //You can also specify chromedriver.exe path dircly ex: C:/MyProject/Project/drivers

            // string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            _driver = new ChromeDriver();
            _wait = new(_driver, TimeSpan.FromSeconds(10));
            _actions = new(_driver);
            _url = "https://practicetestautomation.com/";



            //url = new URL(); 
            // rivate IWebElement practiceBtn => driver.FindElement(By.XPath("//a[contains(text(),'Practice')]"));


            //If you want to Execute Tests on Firefox uncomment the below code

            // Specify Correct location of geckodriver.exe folder path. Ex: C:/Project/drivers

            //driver= new FirefoxDriver(path + @"\drivers\");

        }

        //[OneTimeTearDown]
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void AccessHomePage()
        {
            //_driver.Navigate().GoToUrl("");
            //_driver.Url = url.AutomationTesting;
            _driver.Navigate().GoToUrl(_url);
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var title = _driver.Title;

            try
            {
                By menus = By.Id("menu-primary-items");
                _wait.Until(ExpectedConditions.ElementIsVisible(menus));
                Assert.That(_driver.PageSource, Does.Contain("Hello"));
                Console.WriteLine("Success -- Access to Home Page");

            }
            catch (Exception ex)
            {
                throw new TimeoutException("IT TAKES TOO LONG BROO !", ex);
            }
        }

        [Test]
        public void LogInPractice()
        {
            AccessHomePage();
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Practice')]"))).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("post-title")));
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Test Login Page')]"))).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login")));
            Assert.AreEqual(_driver.FindElement(By.XPath("//*[@id=\"login\"]/h2")).Text, "Test login");

            _driver.FindElement(By.Name("username")).SendKeys("student");
            _driver.FindElement(By.Name("password")).SendKeys("Password123");
            _driver.FindElement(By.Id("submit")).Click();

            string currentURL = _driver.Url;
            IWebElement logout = _driver.FindElement(By.XPath("//a[contains(text(),'Log out')]"));
            Assert.Multiple(() =>
            {
                Assert.That(currentURL, Does.Contain("https://practicetestautomation.com/logged-in-successfully/"));
                Assert.That(_driver.PageSource, Does.Contain("Congratulations").Or.Contain("successfully logged in"), "The Page is Containing Congratulations Or Successfully Logged in");
                Assert.That(logout.Displayed,"Log Out Button Has Been Displayed");
            });

        }

        [Test]
        public void KeyUpTest()
        {
            AccessHomePage();
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Practice')]"))).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("post-title")));
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Test Login Page')]"))).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login")));
            Assert.AreEqual(_driver.FindElement(By.XPath("//*[@id=\"login\"]/h2")).Text, "Test login");

            IWebElement username = _driver.FindElement(By.Name("username"));
            username.Click();
            _actions
                 .KeyDown(Keys.Shift)
                 .SendKeys("s")
                 .KeyUp(Keys.Shift)
                 .SendKeys("tudent")
                 .Perform();

            Console.WriteLine($">>>>>>>> Username Value: {username.GetAttribute("value")}");
            Assert.AreEqual(username.GetAttribute("value"), "Student", "First Letter is UpperCase!");
        }


        [Test]
        public void ScrollToElement()
        {
            AccessHomePage();
            IWebElement subscribeButton = _driver.FindElement(By.XPath("//*[@data-automation-id=\"subscribe-submit-button\"]"));
            var firstSubscribeColor = subscribeButton.GetCssValue("background-color");
            _actions.MoveToElement(subscribeButton);
            _actions.Perform();

            var secondSubscribeColor = subscribeButton.GetCssValue("background-color");
            Assert.AreNotEqual(secondSubscribeColor, firstSubscribeColor, "The Button Color Has Been Changed due to mouse hover!");
            Thread.Sleep(5000);
        }
    }
}