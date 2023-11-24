using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using seleniumCs.Utils;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
//using OpenQA.Selenium;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using System.Drawing.Imaging;

namespace Utils.Initiates;

[SetUpFixture]
public abstract class Initiates
{
    protected IWebDriver? driver;
    protected WebDriverWait? wait;
    public static ExtentReports _extent;
    public static ExtentTest _test;
    public Guid uuid;


    [OneTimeSetUp]
    protected void SetUp()
    {
        uuid = Guid.NewGuid();
        BrowserInitialize();
        StartReporter();
    }

    private void StartReporter()
    {
        string shortUID = uuid.ToString()[..4];
        var dir = TestContext.CurrentContext.TestDirectory;
        string actualPath = dir[..dir.LastIndexOf("bin")] + @"Reports\";
        var fileName = this.GetType().ToString() + $"-{shortUID}" + ".html";
        var htmlReporter = new ExtentSparkReporter(actualPath + fileName);


        _extent = new ExtentReports();
        _extent.AttachReporter(htmlReporter);
    }
    private void BrowserInitialize()
    {
        ChromeOptions option = new();
        //option.AddArgument("--headless");
        option.AddArgument("--disable-gpu");
        //driver = new ChromeDriver();
        driver = new ChromeDriver(option);
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    //public void NavigateToUrl(string url)
    //{
    //	driver.Navigate().GoToUrl(url);	
    //}
    [TearDown]
    public void AfterTest()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        var errorMessage = TestContext.CurrentContext.Result.Message;
        var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
        ? ""
        : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
        Status logstatus;

        switch (status)
        {
            case TestStatus.Failed:
                logstatus = Status.Fail;
                //var screenShotPath = Capture("ScreenShotName");
                //_test.Log(Status.Fail, stacktrace + errorMessage);
                //_test.Log(logstatus, "Snapshot below: " + _test.AddScreenCaptureFromPath(screenShotPath));
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
        if (logstatus == Status.Fail)
        {
            //var screenShotPath = Capture(driver, "zz");
            _test.Log(logstatus, $"Test ended with: {logstatus}" + $"<br>Stack Trace: {stacktrace}" + $"<br>Error Message: {errorMessage}");
        }
        else
        {
            _test.Log(logstatus, $"Test ended with: {logstatus} " + $"<br>Stack Trace: {stacktrace}");
        }
        //_extent.Flush();
    }

    [OneTimeTearDown]
    protected void TearDown()
    {
        //driver.Quit();
        _extent.Flush();
    }
    //   public void Close()
    //{
    //	
    //}

    private String Capture(IWebDriver driver, string screenShotName)
    {
        string shortUID = uuid.ToString()[..4];
        ITakesScreenshot? ts = (ITakesScreenshot)driver;
        Screenshot? screenshot = ts.GetScreenshot();
        string pth = TestContext.CurrentContext.TestDirectory;
        string finalpth = pth[..pth.LastIndexOf("bin")] + @"Reports\ErrorScreenshots\";
        var fileName = this.GetType().Name.ToString() + $"{screenShotName}-{shortUID}.png";
        string localpath = new Uri(finalpth + fileName).LocalPath;
        screenshot.SaveAsFile(localpath);
        //screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
        return localpath;
    }



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
