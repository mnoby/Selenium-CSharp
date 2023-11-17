using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System.Drawing.Printing;
using System;

namespace seleniumCs
{
    public class Tests:Initiate
    {
        //[SetUp]   
       private IWebDriver driver;
       private URL url;

        [OneTimeSetUp]
        public void Setup()

        {

            //Below code is to get the drivers folder path dynamically.

            //You can also specify chromedriver.exe path dircly ex: C:/MyProject/Project/drivers

            // string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver();
            url = new URL(); 
            // rivate IWebElement practiceBtn => driver.FindElement(By.XPath("//a[contains(text(),'Practice')]"));


            //If you want to Execute Tests on Firefox uncomment the below code

            // Specify Correct location of geckodriver.exe folder path. Ex: C:/Project/drivers

            //driver= new FirefoxDriver(path + @"\drivers\");

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void AccessHomePage()

        {
            //driver.Navigate().GoToUrl("");
            //driver.Url = url.AutomationTesting;
            driver.Navigate().GoToUrl("https://practicetestautomation.com/");
            driver.Manage().Window.Maximize();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("menu-primary-items")));
            Assert.That(driver.PageSource, Does.Contain("Hello"));
            Console.WriteLine("Success -- Access to Home Page");
        }

        [Test]
        public void LogInPractice()
        {
            //AccessHomePage();
            practiceBtn.Click();


        }


    }
}