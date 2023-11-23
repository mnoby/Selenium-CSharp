using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using seleniumCs.Utils;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Reflection;
//using OpenQA.Selenium;
using NUnit.Framework.Interfaces;

namespace Utils.Initiates;

[SetUpFixture]
public abstract class Initiates
{
	//protected IWebDriver driver;
	//protected WebDriverWait wait;
	public static ExtentReports _extent;
	public static ExtentTest _test;
    private readonly Guid uuid = Guid.NewGuid();


	[OneTimeSetUp]
    protected void SetUp()
    {
        //BrowserInitialize();
        StartReporter();
    }

    private void StartReporter()
	{
        string uid = uuid.ToString().Substring(0,4);
        var dir = TestContext.CurrentContext.TestDirectory;
        string actualPath = dir.Substring(0, dir.LastIndexOf("bin")) + @"Reports\";
        var fileName = this.GetType().ToString() + $"-{uid}" + ".html";
        var htmlReporter = new ExtentSparkReporter(actualPath + fileName);
        

        _extent = new ExtentReports();
        _extent.AttachReporter(htmlReporter);
    }
    //private void BrowserInitialize() 
    //{
    //	ChromeOptions option = new();
    //	option.AddArgument("--headless");
    //	option.AddArgument("--disable-gpu");
    //	driver = new ChromeDriver(option);
    //	driver.Manage().Window.Maximize();	
    //	driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    //}

    //public void NavigateToUrl(string url)
    //{
    //	driver.Navigate().GoToUrl(url);	
    //}
    [TearDown]
    public void AfterTest()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
        ? ""
        : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
        Status logstatus;

        switch (status)
        {
            case TestStatus.Failed:
                logstatus = Status.Fail;
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

        _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
        _extent.Flush();
    }

    [OneTimeTearDown]
    protected void TearDown()
    {
        _extent.Flush();
    }
 //   public void Close()
	//{
	//	
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
