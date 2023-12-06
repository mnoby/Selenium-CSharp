using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium.Firefox;

namespace SeleniumProject.TestSeleniumGrid
{
    //[TestFixture("chrome", "119", "Windows 11")]
    //[TestFixture("chrome", "118", "Windows 11")]
    //[TestFixture("chrome", "117", "Windows 11")]
    //[TestFixture("chrome", "116", "Windows 11")]
    //[TestFixture("chrome", "115", "Windows 11")]
    //[TestFixture("chrome", "114", "Windows 11")]
    //[TestFixture("chrome", "113", "Windows 11")]
    //[TestFixture("chrome", "112", "Windows 11")]
    //[TestFixture("chrome", "111", "Windows 11")]
    [TestFixture("firefox", "118", "Windows 11")]
    //[TestFixture("firefox", "117", "Windows 11")]
    //[TestFixture("firefox", "116", "Windows 11")]
    //[TestFixture("firefox", "115", "Windows 11")]
    //[TestFixture("firefox", "114", "Windows 11")]
    //[TestFixture("firefox", "113", "Windows 11")]
    //[TestFixture("firefox", "112", "Windows 11")]
    //[TestFixture("firefox", "111", "Windows 11")]
    //[Parallelizable(ParallelScope.Fixtures)] //UnComment this line to enable the parallel run
    //[TestFixture]
    public class SeleniumGrid
    {
        IWebDriver driver;
        public static string uri = "http://localhost:4444";

        //ThreadLocal<IWebDriver> driver = new();
        private string browser;
        private string version;
        private string os;

        public SeleniumGrid(string browser, string version, string os)
        {
            this.browser = browser;
            this.version = version;
            this.os = os;
        }

        [SetUp]
        [Ignore("Just For Trying the Code")]
        public void Init()
        {
            Console.WriteLine($"cersion >>>>>>> {version}");
            if (browser == "chrome")
            {
                ChromeOptions chromeOptions = new()
                {

                    //BrowserVersion = version,
                    PlatformName = os
                };

                driver = new RemoteWebDriver(new Uri("http://localhost:4444"), chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(60));
                driver.Manage().Window.Maximize();
            }
            else if (browser == "firefox")
            {
                FirefoxOptions firefoxOptions = new()
                {
                    //BrowserVersion = version,
                    PlatformName = os

                };
                driver = new RemoteWebDriver(new Uri("http://localhost:4444"), firefoxOptions.ToCapabilities(), TimeSpan.FromSeconds(60));

            }
            Console.WriteLine(driver);
        }

        [Test]
        [Ignore("Just For Trying the Code")]
        public void Todotest()
        {

            Console.WriteLine("Navigating to todos app.");
            driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

            driver.FindElement(By.Name("li4")).Click();
            Console.WriteLine("Clicking Checkbox");
            driver.FindElement(By.Name("li5")).Click();


            // If both clicks worked, then te following List should have length 2
            IList<IWebElement> elems = driver.FindElements(By.ClassName("done-true"));
            // so we'll assert that this is correct.
            Assert.AreEqual(2, elems.Count);

            Console.WriteLine("Entering Text");
            driver.FindElement(By.Id("sampletodotext")).SendKeys("Yey, Let's add it to list");
            driver.FindElement(By.Id("addbutton")).Click();


            // lets also assert that the new todo we added is in the list
            string spanText = driver.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span")).Text;
            Assert.AreEqual("Yey, Let's add it to list", spanText);

        }

        [TearDown]
        [Ignore("Just For Trying the Code")]

        public void Cleanup()
        {
            // Terminates the remote webdriver session
            driver.Quit();
        }

        //[Test]
        //public void SetUp()
        //{
        //    //new DriverManager().SetUpDriver(new ChromeConfig());

        //    //var chromeOptions = new ChromeOptions()
        //    //{
        //    //    PlatformName = "Windows 11",

        //    //};

        //    ChromeOptions chromeOptions = new();
        //    chromeOptions.PlatformName = "Windows 11";


        //    _driver = new RemoteWebDriver(new Uri("http://localhost:4444"), chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(600));
        //    try
        //    {
        //        _driver.Manage().Window.Maximize(); // WINDOWS, DO NOT WORK FOR LINUX/firefox. If Linux/firefox set window size, max 1920x1080, like driver.Manage().Window.Size = new Size(1920, 1080);
        //                                            // driver.Manage().Window.Size = new Size(1920, 1080); // LINUX/firefox			 
        //        _driver.Navigate().GoToUrl("https://www.google.com/ncr");
        //        IWebElement query = _driver.FindElement(By.Name("q"));
        //        query.SendKeys("webdriver");
        //        query.Submit();
        //    }
        //    finally
        //    {
        //        Console.WriteLine("Video: SUccess");
        //        _driver.Quit();
        //    }
        //}

    }
}
