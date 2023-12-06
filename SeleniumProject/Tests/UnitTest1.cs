using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SeleniumProject.Utils;
using SeleniumProject.TestDataAccess;

namespace SeleniumProject.Tests
{

    [TestFixture("chrome", "119", "Windows 11")]
    [TestFixture("chrome", "118", "Windows 11")]
    [TestFixture("firefox", "117", "Windows 11")]
    //[TestFixture("chrome", "116", "Windows 11")]
    //[TestFixture("chrome", "115", "Windows 11")]
    
    [Parallelizable(ParallelScope.Fixtures)]

    public class UnitTest : Inits
    {
        public UnitTest(string browser, string version, string platform) : base(browser, version, platform)
        { }

        [Test]
        public void AccessHomePage()
        {
            //var url = ConfigurationManager.AppSettings["URL"];
            //Console.WriteLine(string.Format("URL '{0}'", url));
            //driver.Navigate().GoToUrl(url);

            driver.Navigate().GoToUrl(URL.practiceAutomation);
            homePage.VerifyHomePage("Hello");
        }

        [Test]
        public void LogInPractice()
        {
            var creds = new
            {
                username = "student",
                password = "Password123"
            };

            var asserts = new
            {
                expectedUrl = """https://practicetestautomation.com/logged-in-successfully/""",
                text1 = "Congratulations",
                text2 = "successfully logged in"
            };


            AccessHomePage();
            homePage.CLickPracticeMenu();
            practicePage.clickTestLogin();
            wait.Until(ExpectedConditions.ElementIsVisible(testLoginPage.pageTitle));
            testLoginPage.Login(creds.username, creds.password);
            testLoginPage.verifyLoginSucess(asserts.expectedUrl, driver.Url, asserts.text1, asserts.text2);
        }

        [Test]
        public void KeyUpTest()
        {
            AccessHomePage();
            homePage.CLickPracticeMenu();
            practicePage.clickTestLogin();
            wait.Until(ExpectedConditions.ElementIsVisible(testLoginPage.pageTitle));

            driver.FindElement(testLoginPage.usernameField).Click();
            actions
                 .KeyDown(Keys.Shift)
                 .SendKeys("s")
                 .KeyUp(Keys.Shift)
                 .SendKeys("tudent")
                 .Perform();

            Console.WriteLine($">>>>>>>> Username Value: {driver.FindElement(testLoginPage.usernameField).GetAttribute("value")}");
            Assert.AreEqual(driver.FindElement(testLoginPage.usernameField).GetAttribute("value"), "Student", "First Letter is UpperCase!");
        }


        [Test]
        public void ScrollToElement()
        {
            AccessHomePage();
            homePage.HoverSubscribeBtn("background-color");
        }

        [Test]
        public void DropdownActions()
        {
            driver.Navigate().GoToUrl(URL.heroLookUp);
            wait.Until(ExpectedConditions.ElementIsVisible(homePage2.headerTxt));
            homePage2.ClickMenus("Dropdown");
            Assert.That(driver.PageSource, Does.Contain("Dropdown List"));
            dropdownPage.DropdownAction("Option 2");
        }

        [Test]
        public void CheckboxActions()
        {
            driver.Navigate().GoToUrl(URL.heroLookUp);
            wait.Until(ExpectedConditions.ElementIsVisible(homePage2.headerTxt));
            homePage2.ClickMenus("Checkboxes");
            Assert.That(driver.PageSource, Does.Contain("Checkboxes"));

            //Check All the Unthick CheckBoxes
            checkboxAction.ThickCheckboxes();

            Thread.Sleep(3000);
            //Check All the thicked CheckBoxes
            checkboxAction.UnthickCheckboxes();
        }

        [Test]
        [Category("Handling Alerts")]
        public void HandlingAlerts()
        {
            driver.Navigate().GoToUrl(URL.heroLookUp);
            wait.Until(ExpectedConditions.ElementIsVisible(homePage2.headerTxt));
            homePage2.ClickMenus("JavaScript Alerts");
            Assert.That(driver.PageSource, Does.Contain("JavaScript Alerts"));

            alertPage.ClickJsAlert();
            Thread.Sleep(2000);
            alertPage.ClickJsConfirm();
            Thread.Sleep(2000);
            alertPage.ClickJsPrompt();
        }



        [Test]
        public void DataDrivenExcel()
        {
            var userData = ExcelDataAccess.GetTestData("UnitTest1"); // Store userdatas which has key = UnitTest1
            var asserts = new
            {
                expectedUrl = """https://practicetestautomation.com/logged-in-successfully/""",
                text1 = "Congratulations",
                text2 = "successfully logged in"
            };
            AccessHomePage();
            homePage.CLickPracticeMenu();
            practicePage.clickTestLogin();
            wait.Until(ExpectedConditions.ElementIsVisible(testLoginPage.pageTitle));

            testLoginPage.Login(userData.username, userData.password);
            testLoginPage.verifyLoginSucess(asserts.expectedUrl, driver.Url, asserts.text1, asserts.text2);



        }
        /* script below still have not implemented POM yet
        //[Test]
        //public void SliderActions()
        //{
        //    decimal amount = 3.5m, sliderMax = 5.0m, sliderMin = 0.0m;
        //    driver.Navigate().GoToUrl(URL.heroLookUp);
        //    driver.FindElement(By.XPath("//a[contains(text(),'Horizontal Slider')]")).Click();
        //    Assert.That(driver.PageSource, Does.Contain("Horizontal Slider"));

        //    IWebElement slider = driver.FindElement(By.XPath("//*[@class=\"sliderContainer\"]/input"));
        //    res = GetPixelsToMove(slider, amount, sliderMax, sliderMin);
        //    actions
        //        .ClickAndHold(slider)
        //        .MoveByOffset(-slider.Size.Width / 2, 0)
        //        .MoveByOffset(res, 0)
        //        .Release().Perform();

        //    IWebElement range = driver.FindElement(By.XPath("//*[@class=\"sliderContainer\"]/span"));
        //    //double sss = (double)amount;
        //    Assert.AreEqual(amount.ToString(), range.Text.Replace('.', ','));
        //}

        //[Test]
        //public void WindowHandle()
        //{
        //    driver.Navigate().GoToUrl(URL.heroLookUp);
        //    string baseWindow = driver.CurrentWindowHandle;
        //    Assert.AreEqual(driver.WindowHandles.Count, 1);
        //    Console.WriteLine($">>>>>>>>WINDOW {baseWindow}");

        //    driver.FindElement(By.XPath("//a[contains(text(),'Multiple Windows')]")).Click();
        //    Assert.That(driver.PageSource, Does.Contain("Opening a new window"));

        //    driver.FindElement(By.XPath("//a[contains(text(),'Click Here')]")).Click();
        //    wait.Until(d => driver.WindowHandles.Count == 2);

        //    foreach (string window in driver.WindowHandles)
        //    {
        //        if (baseWindow != window)
        //        {
        //            driver.SwitchTo().Window(window);
        //            break;
        //        }
        //    }

        //    wait.Until(ExpectedConditions.TitleContains("New Window"));
        //    Assert.That(driver.PageSource, Does.Contain("New Window"));
        //    //driver.Close();

        //    // Create New Tab on the same window and Switch To it
        //    driver.SwitchTo().NewWindow(WindowType.Tab);
        //    Thread.Sleep(5000);

        //    // Create New Window and Switch TO it
        //    driver.SwitchTo().NewWindow(WindowType.Window);
        //    driver.Navigate().GoToUrl(URL.heroLookUp);
        //    driver.SwitchTo().Window(baseWindow);
        //    Thread.Sleep(5000);


        //}

        //[Test]
        //public void FrameHandle()
        //{
        //    driver.Navigate().GoToUrl(URL.heroLookUp);

        //    driver.FindElement(By.XPath("//a[contains(text(),'Frames')]")).Click();
        //    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'iFrame')]"))).Click();
        //    Assert.That(driver.PageSource, Does.Contain("An iFrame containing the TinyMCE WYSIWYG Editor"));


        //    driver.FindElement(By.XPath("//button/span[contains(text(),'File')]")).Click();
        //    Thread.Sleep(5000);

        //}

        //[Test]
        //public void DragNDropHandle()
        //{
        //    driver.Navigate().GoToUrl(URL.heroLookUp);

        //    driver.FindElement(By.XPath("//a[contains(text(),'Drag and Drop')]")).Click();
        //    Assert.That(driver.PageSource, Does.Contain("Drag and Drop"));

        //    IWebElement boxA = driver.FindElement(By.Id("column-a"));
        //    IWebElement boxB = driver.FindElement(By.Id("column-b"));

        //    Assert.That(boxA.Text, Is.EqualTo("A"));
        //    Assert.That(boxB.Text, Is.EqualTo("B"));

        //    //Drag box A and Drop to Box B
        //    actions
        //        .ClickAndHold(boxA)
        //        .MoveToElement(boxB)
        //        .Release().Perform();
        //    Assert.Multiple(() =>
        //    {
        //        Assert.That(boxA.Text, Is.EqualTo("B"));
        //        Assert.That(boxB.Text, Is.EqualTo("A"));
        //    });
        //    //Thread.Sleep(5000);
        //}

        //[Test]
        //public void WorkingWithCookies()
        //{
        //    driver.Navigate().GoToUrl(URL.heroLookUp);
        //    driver.FindElement(By.XPath("//a[contains(text(),'Drag and Drop')]")).Click();

        //    // Add Cookies to the current Browser
        //    driver.Manage().Cookies.AddCookie(new Cookie("key", $"{uuid}"));
        //    //Thread.Sleep(30000);

        //    // Get the Added Cookies
        //    Cookie cookie = driver.Manage().Cookies.GetCookieNamed("key");
        //    Console.WriteLine($">>>>>>>>>> COOKIE ${cookie.Value}");

        //    Assert.That(cookie.Value.ToString(), Is.EqualTo(uuid.ToString()));


        //    //Delete the Added Cookies
        //    driver.Manage().Cookies.DeleteCookie(cookie);

        //    var cookieDict1 = new System.Collections.Generic.Dictionary<string, object>(){
        //        {"name", "strict" }, {"value", "strict"}, {"sameSite","Strict"}};
        //    var cookie1 = Cookie.FromDictionary(cookieDict1);

        //    var cookieDict2 = new System.Collections.Generic.Dictionary<string, object>(){
        //        {"name", "lax" }, {"value", "lax"}, {"sameSite","Lax"}};
        //    var cookie2 = Cookie.FromDictionary(cookieDict2);


        //    driver.Manage().Cookies.AddCookie(cookie1);
        //    driver.Manage().Cookies.AddCookie(cookie2);

        //    Console.WriteLine($">>>>>>>>>>>>>>>> COOKIE 1 {cookie1.SameSite}");
        //    Console.WriteLine($">>>>>>>>>>>>>>>> COOKIE 2 {cookie2.SameSite}");
        //    Thread.Sleep(100000);

        //}

        //[Test]
        //public void FluentWait()
        //{
        //    driver.Navigate().GoToUrl(URL.heroLookUp);
        //    DefaultWait<IWebDriver> fWait = new(driver)
        //    {
        //        Timeout = TimeSpan.FromSeconds(30),
        //        PollingInterval = TimeSpan.FromSeconds(2)
        //    };
        //    fWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        //    fWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Drag and Dropp')]")));
        //}
        */
    }


}