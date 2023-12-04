//using AventStack.ExtentReports;
//using AventStack.ExtentReports.Reporter;
//using NUnit.Framework;
//using NUnit.Framework.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using System.IO;
//using AventStack.ExtentReports.Model;
//using System.Net.NetworkInformation;
//using System.Xml.Linq;
//using System.Security.Cryptography;

//namespace SeleniumProject.Utils
//{
//    [SetUpFixture]
//    public class Reporter1
//    {
//        protected ExtentReports _extent;
//        protected ExtentTest _test;
//        public IWebDriver _driver;
//        //UnitTest unitTest;
//        Guid _uuid = Guid.NewGuid();

       
//        [OneTimeSetUp]
//        protected void Setup()
//        {
//            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
//            var actualPath = path[..path.LastIndexOf("bin")];
//            var projectPath = new Uri(actualPath).LocalPath;
//            Directory.CreateDirectory(projectPath.ToString() + "Reports");
//            var fileName = this.GetType().ToString() + $"-{_uuid.ToString()[..4]}" + ".html";
//            var reportPath = projectPath + @"Reports\";
//            var htmlReporter = new ExtentSparkReporter(reportPath + fileName);
//            //var htmlReporter = new ExtentSparkReporter(reportPath);
//            _extent = new ExtentReports();
//            _extent.AttachReporter(htmlReporter);
//            _extent.AddSystemInfo("Host Name", "LocalHost");
//            _extent.AddSystemInfo("Environment", "QA");
//            _extent.AddSystemInfo("UserName", "TestUser");
//        }

//        [OneTimeTearDown]
//        protected void TearDown()
//        {
//            _extent.Flush();
//        }

//        [SetUp]
//        public void BeforeTest()
//        {
//            //ChromeDriverService service = ChromeDriverService.CreateDefaultService("webdriver.chrome.driver", @"D:\\Automation\\WebDrivers\\chromedriver.exe");
//            //_driver = new ChromeDriver();
//            //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
//            //_driver.Manage().Window.Maximize();
//            _driver = new WebDriverFactory().Create(BrowserType.Chrome);
//            _driver.Manage().Window.Maximize();
//            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
//            //unitTest = new UnitTest(_driver);
//            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
//        }

//        [TearDown]

//        public void AfterTest()
//        {
//            var status = TestContext.CurrentContext.Result.Outcome.Status;
//            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? ""
//: string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
//            Status logstatus;
//            switch (status)
//            {
//                case TestStatus.Failed:
//                    logstatus = Status.Fail;
//                    DateTime time = DateTime.Now;
//                    string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
//                    string screenShotPath = Capture(_driver, fileName);
//                    _test.Log(Status.Fail, "Fail");
//                    _test.Log(Status.Fail, "Snapshot below: " + _test.AddScreenCaptureFromPath("Screenshots\\" + screenShotPath));
//                    break;
//                case TestStatus.Inconclusive:
//                    logstatus = Status.Warning;
//                    break;
//                case TestStatus.Skipped:
//                    logstatus = Status.Skip;
//                    break;
//                default:
//                    logstatus = Status.Pass;
//                    break;
//            }
//            _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
//            _extent.Flush();
//            //_driver.Quit();
//        }
//        public IWebDriver GetDriver()
//        {
//            return _driver;
//        }

//        public static string Capture(IWebDriver driver, string screenShotName)
//        {
//            ITakesScreenshot ts = (ITakesScreenshot)driver;
//            Screenshot screenshot = ts.GetScreenshot();
//            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
//            var actualPath = pth[..pth.LastIndexOf("bin")];
//            var reportPath = new Uri(actualPath).LocalPath;
//            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
//            var finalpth = pth[..pth.LastIndexOf("bin")] + "Reports\\Screenshots\\" + screenShotName;
//            var localpath = new Uri(finalpth).LocalPath;
//            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
//            return reportPath;
//        }
//    }
//}
