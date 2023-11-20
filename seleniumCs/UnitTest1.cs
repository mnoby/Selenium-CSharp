using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace seleniumCs
{
    public class Tests
    {
        //[SetUp]
        IWebDriver driver;

        [OneTimeSetUp]

        public void Setup()

        {

            //Below code is to get the drivers folder path dynamically.

            //You can also specify chromedriver.exe path dircly ex: C:/MyProject/Project/drivers

           // string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver();

            //If you want to Execute Tests on Firefox uncomment the below code

            // Specify Correct location of geckodriver.exe folder path. Ex: C:/Project/drivers

            //driver= new FirefoxDriver(path + @"\drivers\");

        }

        [Test]
        public void VerifyLogo()

        {
            driver.Navigate().GoToUrl("https://www.browserstack.com/");
            Assert.That(driver.FindElement(By.ClassName("bstack-mm-logo")).Displayed, Is.True);
            Console.WriteLine("YEEEEE");
        }

        [Test]
        public void VerifyMenuItemcount()
        {
            ReadOnlyCollection<IWebElement> menuItem = driver.FindElements(By.XPath("//ul[contains(@class,'horizontal-list product-menu')]/li"));
            Assert.AreEqual(menuItem.Count, 4);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}