using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

namespace SeleniumProject.Utils
{
    public class Reporter
    {
        ExtentReports _extent;
        ExtentTest _test;
        IWebDriver _driver;

        //UnitTest unitTest;
        Guid _uuid;

        public Reporter(IWebDriver driver, ExtentReports extent, ExtentTest test, Guid uuid)
        {
            this._driver = driver;
            this._extent = extent;
            this._test = test;
            this._uuid = uuid;
        }

        //not used for now
        public void ReportInit()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            //var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path[..path.LastIndexOf("bin")];
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            var fileName = this.GetType().ToString() + $"-{_uuid.ToString()[..4]}" + ".html";
            var reportPath = projectPath + @"Reports\";
            var htmlReporter = new ExtentSparkReporter(reportPath + fileName);
            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Host Name", "LocalHost");
            _extent.AddSystemInfo("Environment", "QA");
            _extent.AddSystemInfo("UserName", "TestUser");
        }

        public void FlushReporter()
        {
            _extent.Flush();
        }

        public void BeforeTest()
        {
            _driver = new WebDriverFactory().Create(BrowserType.Chrome);
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        public void GenerateReport()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? ""
: string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
                    string screenShotPath = Capture(_driver, fileName);
                    _test.Log(Status.Fail, "Fail");
                    _test.Log(Status.Fail, "Snapshot below: " + _test.AddScreenCaptureFromPath(screenShotPath));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            _test.Log(logstatus, "Test ended with: " + logstatus + "<br> Stacktrace: " + stacktrace);
            _extent.Flush();
            //_driver.Quit();
        }
        public IWebDriver GetDriver()
        {
            return _driver;
        }

        public static string Capture(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = Environment.CurrentDirectory;
            var actualPath = pth[..pth.LastIndexOf("bin")];
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth[..pth.LastIndexOf("bin")] + "Reports\\Screenshots\\" + screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath);
            return localpath;
        }

    }
    
}
