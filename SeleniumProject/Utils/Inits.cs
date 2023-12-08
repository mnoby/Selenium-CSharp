using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using SeleniumProject.Pages;
using SeleniumProject.Pages2;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;

namespace SeleniumProject.Utils;

[SetUpFixture]
public class Inits
{
    protected IWebDriver driver;
    protected WebDriverWait wait;
    protected ExtentReports extent;
    protected ExtentTest test;
    protected Guid uuid;
    protected Actions actions;
    protected string Browser { get; }
    protected string Version { get; }
    protected string Platform { get; }

    protected HomePage homePage;
    protected HomePage2 homePage2;
    protected PracticePage practicePage;
    protected TestLoginPage testLoginPage;
    protected DropdownPage dropdownPage;
    protected CheckboxAction checkboxAction;
    protected AlertPage alertPage;
    Reporter reporter;

    public Inits(string browser, string version, string platform)
    {
        Browser = browser;
        Version = version;
        Platform = platform;
    }

    [SetUp]
    public void BeforeTest()
    {
        // Init THe WebDriver for NON-selenium grid running method
        //BrowserInit();

        // Init THe WebDriver for selenium grid running method
        RemoteDriverInit();
        test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        wait = new(driver, TimeSpan.FromSeconds(10));
        actions = new(driver);
        homePage = new HomePage(driver, wait, actions);
        homePage2 = new HomePage2(driver, wait, Browser, Version, Platform);
        practicePage = new PracticePage(driver, wait);
        testLoginPage = new TestLoginPage(driver, wait);
        dropdownPage = new DropdownPage(driver, wait, Browser, Version, Platform);
        checkboxAction = new CheckboxAction(driver, wait, Browser, Version, Platform);
        alertPage = new AlertPage(driver, wait);
        reporter = new Reporter(driver, extent, test, uuid);
    }

    [OneTimeSetUp]
    public void SetUp()
    {
        //reporter.ReportInit(); // still failed when call this function due to it is in different class
        uuid = Guid.NewGuid();

        var path = Environment.CurrentDirectory;
        var actualPath = path[..path.LastIndexOf("bin")];
        var projectPath = new Uri(actualPath).LocalPath;
        Directory.CreateDirectory(projectPath.ToString() + "Reports");
        var className = this.GetType().Name;
        var fileName = className + $"_{Browser}" + $"-{uuid.ToString()[..4]}" + ".html";
        //var fileName = this.GetType().ToString() + $"-{uuid.ToString()[..4]}" + ".html";
        var reportPath = projectPath + @"Reports\";
        var htmlReporter = new ExtentSparkReporter(reportPath + fileName);
        extent = new ExtentReports();
        extent.AttachReporter(htmlReporter);
        extent.AddSystemInfo("Host Name", "LocalHost");
        extent.AddSystemInfo("Environment", "QA");
        extent.AddSystemInfo("UserName", "TestUser");
        extent.AddSystemInfo("Platform", Browser);
        extent.AddSystemInfo("Platform", Platform);

    }

    [OneTimeTearDown]
    public void AfterTest()
    {
        reporter.FlushReporter();
    }

    [TearDown]
    public void TearDown()
    {
        reporter.GenerateReport($"{Browser}-{uuid.ToString()[..4]}");
        driver.Quit();
    }
    private void BrowserInit()
    {
        driver = new WebDriverFactory().Create(BrowserType.Chrome);
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    private void RemoteDriverInit()
    {
  
        if (Browser == "chrome")
        {
            ChromeOptions chromeOptions = new()
            {

                //BrowserVersion = version,
                PlatformName = Platform

            };
            chromeOptions.AddArgument("--headless");

            driver = new RemoteWebDriver(new Uri(URL.seleniumGridUri), chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(60));
            driver.Manage().Window.Maximize();
        }
        else if (Browser == "firefox")
        {
            FirefoxOptions firefoxOptions = new()
            {
                //BrowserVersion = version,
                PlatformName = Platform

            };
            firefoxOptions.AddArgument("--headless");
            driver = new RemoteWebDriver(new Uri(URL.seleniumGridUri), firefoxOptions.ToCapabilities(), TimeSpan.FromSeconds(60));
        }

        driver.Manage().Window.Maximize();
    }

}
