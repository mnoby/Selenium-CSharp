using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
//using OpenQA.Selenium;
using NUnit.Framework.Interfaces;
using seleniumCs.Resources;

namespace seleniumCs.Utils;

[SetUpFixture]
public abstract class Inits
{
    protected IWebDriver? driver;
    protected WebDriverWait? wait;
    public static ExtentReports extent;
    public static ExtentTest test;
    protected Guid uuid;
    //readonly Reporter report = new();
    //protected ExtentTest aa;


    //[OneTimeSetUp]
    //protected void SetUp()
    //{
    //    //test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
    //    //aa = test;
    //    uuid = Guid.NewGuid();
    //    //BrowserInitialize();
    //    BrowserInit();
    //    StartReporter();
    //}

    //private void BrowserInit()
    //{
    //    driver = new WebDriverFactory().Create(BrowserType.Chrome);
    //    driver.Manage().Window.Maximize();
    //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    //}
    ////private void BrowserInitialize()
    ////{
    ////    ChromeOptions option = new();
    ////    //option.AddArgument("--headless");
    ////    option.AddArgument("--disable-gpu");
    ////    //driver = new ChromeDriver();
    ////    driver = new ChromeDriver(option);
    ////    driver.Manage().Window.Maximize();
    ////    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    ////}

    //public void StartReporter()
    //{
    //    string shortUID = uuid.ToString()[..4];
    //    var dir = TestContext.CurrentContext.TestDirectory;
    //    string actualPath = dir[..dir.LastIndexOf("bin")] + @"Reports\";
    //    var fileName = this.GetType().ToString() + $"-{shortUID}" + ".html";
    //    var htmlReporter = new ExtentSparkReporter(actualPath + fileName);


    //    extent = new ExtentReports();
    //    extent.AttachReporter(htmlReporter);
    //}

    //[TearDown]
    ////[Ignore("Just Ignore")]
    //public void TestResult()
    //{
    //    //report.GenerateReport();

    //    var status = TestContext.CurrentContext.Result.Outcome.Status;
    //    var errorMessage = TestContext.CurrentContext.Result.Message;
    //    var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
    //    ? ""
    //    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
    //    Status logstatus;

    //    switch (status)
    //    {
    //        case TestStatus.Failed:
    //            logstatus = Status.Fail;
    //            //var screenShotPath = Capture("ScreenShotName");
    //            //test.Log(Status.Fail, stacktrace + errorMessage);
    //            //test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
    //            break;
    //        case TestStatus.Inconclusive:
    //            logstatus = Status.Warning;
    //            break;
    //        case TestStatus.Skipped:
    //            logstatus = Status.Skip;
    //            break;
    //        default:
    //            logstatus = Status.Pass;
    //            break;
    //    }
    //    if (logstatus == Status.Fail)
    //    {
    //        //var screenShotPath = Capture(driver, "zz");
    //        test.Log(logstatus, $"Test ended with: {logstatus}" + $"<br>Stack Trace: {stacktrace}" + $"<br>Error Message: {errorMessage}");
    //    }
    //    else
    //    {
    //        test.Log(logstatus, $"Test ended with: {logstatus} " + $"<br>Stack Trace: {stacktrace}");
    //    }
    //}

    //[OneTimeTearDown]
    ////[Ignore("Just Ignore")]
    //protected void TearDown()
    //{
    //    extent.Flush();
    //}

    //private String Capture(IWebDriver driver, string screenShotName)
    //{
    //    string shortUID = uuid.ToString()[..4];
    //    ITakesScreenshot? ts = (ITakesScreenshot)driver;
    //    Screenshot? screenshot = ts.GetScreenshot();
    //    string pth = TestContext.CurrentContext.TestDirectory;
    //    string finalpth = pth[..pth.LastIndexOf("bin")] + @"Reports\ErrorScreenshots\";
    //    var fileName = this.GetType().Name.ToString() + $"{screenShotName}-{shortUID}.png";
    //    string localpath = new Uri(finalpth + fileName).LocalPath;
    //    screenshot.SaveAsFile(localpath);
    //    //screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
    //    return localpath;
    //}



    //   public void ExplicitWaitVisible(object element)
    //{
    //	wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
    //	var waitingEl = wait.Until(ExpectedConditions.ElementIsVisible((By)element));
    //       Assert.IsTrue(waitingEl.Displayed);
    //}

    //   public void ExplicitWaitClickable(object element)
    //   {
    //       wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
    //       wait.Until(ExpectedConditions.ElementToBeClickable((By)element)).Click();
    //   }
}
