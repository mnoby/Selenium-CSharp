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
using NUnit.Framework;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using System.Security.Policy;
using System.Drawing;
using RazorEngine.Compilation.ImpromptuInterface.InvokeExt;
using Microsoft.CodeAnalysis;

namespace seleniumCs
{
    [TestFixture]
    public class UnitTest : Initiates
    {
        //private ChromeDriver driver;
        //private WebDriverWait wait;
        private Actions _actions;
        private string _url, _url2;
        private int _index;
        private int res;

        private static int GetPixelsToMove(IWebElement Slider, decimal Amount, decimal SliderMax, decimal SliderMin)
        {
            int pixels;
            decimal tempPixels = Slider.Size.Width;
            tempPixels /= (SliderMax - SliderMin);
            tempPixels *= (Amount - SliderMin);
            pixels = (int)tempPixels;
            return pixels;
        }

        [SetUp]
        public void Setup()
        {

            //Below code is to get the drivers folder path dynamically.

            //You can also specify chromedriver.exe path dircly ex: C:/MyProject/Project/drivers

            // string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            //driver = new ChromeDriver();
            wait = new(driver, TimeSpan.FromSeconds(10));
            _actions = new(driver);
            _url = "https://practicetestautomation.com/";
            _url2 = "https://the-internet.herokuapp.com/";

            //string pth = TestContext.CurrentContext.TestDirectory;
            //string finalpth = pth[..pth.LastIndexOf("bin")] + @"Reports\ErrorScreenshots\";
            //Console.WriteLine($">>>>>>>>>FinalPth {finalpth}");
            //var fileName = this.GetType().Name.ToString() + $"zz.png";
            //Console.WriteLine($">>>>>>>>>fileName {fileName}");
            //string localpath = new Uri(finalpth + fileName).LocalPath;
            //Console.WriteLine($">>>>>>>>>localpath {localpath}");

            //url = new URL(); 
            // rivate IWebElement practiceBtn => driver.FindElement(By.XPath("//a[contains(text(),'Practice')]"));


            //If you want to Execute Tests on Firefox uncomment the below code

            // Specify Correct location of geckodriver.exe folder path. Ex: C:/Project/drivers

            //driver= new FirefoxDriver(path + @"\drivers\");

        }


        [OneTimeTearDown]
        //[TearDown]
        public void Close()
        {
            driver.Quit();
        }

        [Test]
        public void AccessHomePage()
        {
            driver.Navigate().GoToUrl(_url);
            var title = driver.Title;

            try
            {
                By? menus = By.Id("menu-primary-items");
                wait.Until(ExpectedConditions.ElementIsVisible(menus));
                Assert.That(driver.PageSource, Does.Contain("Hello"));
                Console.WriteLine("Success -- Access to Home Page");

            }
            catch (Exception)
            {
                throw new TimeoutException("IT TAKES TOO LONG BROO !");
            }
        }

        [Test]
        public void LogInPractice()
        {
            AccessHomePage();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Practice')]"))).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("post-title")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Test Login Page')]"))).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login")));
            Assert.AreEqual("Test login", driver.FindElement(By.XPath("//*[@id=\"login\"]/h2")).Text);

            driver.FindElement(By.Name("username")).SendKeys("student");
            driver.FindElement(By.Name("password")).SendKeys("Password123");
            driver.FindElement(By.Id("submit")).Click();

            string currentURL = driver.Url;
            IWebElement? logout = driver.FindElement(By.XPath("//a[contains(text(),'Log out')]"));
            Assert.Multiple(() =>
            {
                Assert.That(currentURL, Does.Contain("https://practicetestautomation.com/logged-in-successfully/"));
                Assert.That(driver.PageSource, Does.Contain("Congratulations").Or.Contain("successfully logged in"), "The Page is Containing Congratulations Or Successfully Logged in");
                Assert.That(logout.Displayed, "Log Out Button Has Been Displayed");
            });

        }

        [Test]
        public void KeyUpTest()
        {
            AccessHomePage();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Practice')]"))).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("post-title")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Test Login Page')]"))).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login")));
            Assert.AreEqual(driver.FindElement(By.XPath("//*[@id=\"login\"]/h2")).Text, "Test login");

            IWebElement? username = driver.FindElement(By.Name("username"));
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
            IWebElement? subscribeButton = driver.FindElement(By.XPath("//*[@data-automation-id=\"subscribe-submit-button\"]"));
            var firstSubscribeColor = subscribeButton.GetCssValue("background-color");
            _actions.MoveToElement(subscribeButton);
            _actions.Perform();

            var secondSubscribeColor = subscribeButton.GetCssValue("background-color");
            Assert.AreNotEqual(secondSubscribeColor, firstSubscribeColor, "The Button Color Has Been Changed due to mouse hover!");
            Thread.Sleep(5000);
        }

        [Test]
        public void DropdownActions()
        {
            driver.Navigate().GoToUrl(_url2);
            driver.FindElement(By.XPath("//a[contains(text(),'Dropdown')]")).Click();
            Assert.That(driver.PageSource, Does.Contain("Dropdown List"));

            //driver.FindElement(By.Id("dropdown")).Click();
            SelectElement sel = new(driver.FindElement(By.Id("dropdown")));
            sel.SelectByText("Option 2");
            IWebElement s = driver.FindElement(By.XPath("//option[contains(text(),'Option 2')]"));
            //wait.Until(ExpectedConditions.ElementToBeClickable(s)).Click();
            Thread.Sleep(5000);
            Console.WriteLine(s.GetAttribute("selected"));
            Assert.That(s.Selected, Is.True);
        }

        [Test]
        public void CheckboxActions()
        {
            driver.Navigate().GoToUrl(_url2);
            driver.FindElement(By.XPath("//a[contains(text(),'Checkboxes')]")).Click();
            Assert.That(driver.PageSource, Does.Contain("Checkboxes"));

            //Check All the Unthick CheckBoxes
            _index = 1;
            var checkboxes = driver.FindElements(By.XPath("//input[@type=\"checkbox\"]"));
            foreach (var box in checkboxes)
            {
                IWebElement check = driver.FindElement(By.XPath($"//input[@type=\"checkbox\"][{_index}]"));
                if (box.GetAttribute("checked") == null)
                {
                    check.Click();
                    _index++;
                }
                Assert.That(box.GetAttribute("checked"), Is.EqualTo("true"));
            }
            Console.WriteLine("SUCCESS ! -- The Checkboxes Has Been Thicked");

            Thread.Sleep(3000);
            //Check All the thicked CheckBoxes
            _index = 1;
            foreach (var box in checkboxes)
            {
                IWebElement check = driver.FindElement(By.XPath($"//input[@type=\"checkbox\"][{_index}]"));
                if (box.GetAttribute("checked") == "true")
                {
                    check.Click();
                    _index++;
                }
                Assert.That(box.GetAttribute("checked"), Is.Null);
            }
            Console.WriteLine("SUCCESS ! -- The Checkboxes Has Been Unthicked");


        }

        [Test]
        public void SliderActions()
        {
            decimal amount = 3.5m, sliderMax = 5.0m, sliderMin = 0.0m;
            driver.Navigate().GoToUrl(_url2);
            driver.FindElement(By.XPath("//a[contains(text(),'Horizontal Slider')]")).Click();
            Assert.That(driver.PageSource, Does.Contain("Horizontal Slider"));

            IWebElement slider = driver.FindElement(By.XPath("//*[@class=\"sliderContainer\"]/input"));
            res = GetPixelsToMove(slider, amount, sliderMax, sliderMin);
            _actions
                .ClickAndHold(slider)
                .MoveByOffset(-slider.Size.Width / 2, 0)
                .MoveByOffset(res, 0)
                .Release().Perform();

            IWebElement range = driver.FindElement(By.XPath("//*[@class=\"sliderContainer\"]/span"));
            //double sss = (double)amount;
            Assert.AreEqual(amount.ToString(), range.Text.Replace('.',','));
        }

        [Test]
        public void WindowHandle()
        {
            driver.Navigate().GoToUrl(_url2);
            string baseWindow = driver.CurrentWindowHandle;
            Assert.AreEqual(driver.WindowHandles.Count, 1);
            Console.WriteLine($">>>>>>>>WINDOW {baseWindow}");

            driver.FindElement(By.XPath("//a[contains(text(),'Multiple Windows')]")).Click();
            Assert.That(driver.PageSource, Does.Contain("Opening a new window"));

            driver.FindElement(By.XPath("//a[contains(text(),'Click Here')]")).Click();
            wait.Until(d => driver.WindowHandles.Count == 2);

            foreach(string window in driver.WindowHandles)
            {
                if(baseWindow != window)
                {
                    driver.SwitchTo().Window(window);
                    break;
                }
            }

            wait.Until(ExpectedConditions.TitleContains("New Window"));
            Assert.That(driver.PageSource, Does.Contain("New Window"));
            //driver.Close();

            // Create New Tab on the same window and Switch To it
            driver.SwitchTo().NewWindow(WindowType.Tab);
            Thread.Sleep(5000);

            // Create New Window and Switch TO it
            driver.SwitchTo().NewWindow(WindowType.Window);
            driver.Navigate().GoToUrl(_url2);
            driver.SwitchTo().Window(baseWindow);
            Thread.Sleep(5000);


        }
    }
}