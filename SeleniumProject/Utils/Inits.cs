using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using SeleniumProject.Pages;
using SeleniumProject.Pages2;
using OpenQA.Selenium.Interactions;

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

    protected HomePage homePage;
    protected HomePage2 homePage2;
    protected PracticePage practicePage;
    protected TestLoginPage testLoginPage;
    protected DropdownPage dropdownPage;
    protected CheckboxAction checkboxAction;
    protected AlertPage alertPage;
    Reporter reporter;

    [SetUp]
    public void BeforeTest()
    {
        BrowserInit();

        test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        wait = new(driver, TimeSpan.FromSeconds(10));
        actions = new(driver);
        homePage = new HomePage(driver, wait, actions);
        homePage2 = new HomePage2(driver, wait);
        practicePage = new PracticePage(driver, wait);
        testLoginPage = new TestLoginPage(driver, wait);
        dropdownPage = new DropdownPage(driver, wait);
        checkboxAction = new CheckboxAction(driver, wait);
        alertPage = new AlertPage(driver, wait);
        reporter = new Reporter(driver, extent, test, uuid);
    }

    [OneTimeSetUp]
    public void SetUp()
    {
        //reporter.ReportInit();
        uuid = Guid.NewGuid();

        var path = Environment.CurrentDirectory;
        var actualPath = path[..path.LastIndexOf("bin")];
        var projectPath = new Uri(actualPath).LocalPath;
        Directory.CreateDirectory(projectPath.ToString() + "Reports");
        var fileName = this.GetType().ToString() + $"-{uuid.ToString()[..4]}" + ".html";
        var reportPath = projectPath + @"Reports\";
        var htmlReporter = new ExtentSparkReporter(reportPath + fileName);
        extent = new ExtentReports();
        extent.AttachReporter(htmlReporter);
        extent.AddSystemInfo("Host Name", "LocalHost");
        extent.AddSystemInfo("Environment", "QA");
        extent.AddSystemInfo("UserName", "TestUser");

    }

    [OneTimeTearDown]
    public void AfterTest()
    {
        reporter.FlushReporter();
    }

    [TearDown]
    public void TearDown()
    {
        reporter.GenerateReport();
        driver.Quit();
    }
    private void BrowserInit()
    {
        driver = new WebDriverFactory().Create(BrowserType.Chrome);
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    //public IWebDriver GetDriver()
    //{
    //    return driver;
    //}
    //////private void BrowserInitialize()
    ////{
    ////    ChromeOptions option = new();
    ////    //option.AddArgument("--headless");
    ////    option.AddArgument("--disable-gpu");
    ////    //driver = new ChromeDriver();
    ////    driver = new ChromeDriver(option);
    ////    driver.Manage().Window.Maximize();
    ////    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    ////}

}
