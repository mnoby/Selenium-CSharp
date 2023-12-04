namespace SeleniumProject.Pages
{
    public class TestLoginPage
    {
        IWebDriver driver;
        WebDriverWait wait;

        public readonly By pageTitle = By.Id("login");
        public readonly By usernameField = By.Name("username");
        readonly By passwordField = By.Name("password");
        readonly By submitBtn = By.Id("submit");
        readonly By logoutBtn = By.XPath("//a[contains(text(),'Log out')]");

        public TestLoginPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;

        }

        public void Login(string  username, string password)
        {
            
            driver.FindElement(usernameField).SendKeys(username);
            driver.FindElement(passwordField).SendKeys(password);
            driver.FindElement(submitBtn).Click();
        }

        public void verifyLoginSucess(string expectedUrl, string actualUrl, string pageSource1, string pageSource2)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actualUrl, Is.EqualTo(expectedUrl));
                Assert.That(driver.PageSource, Does.Contain(pageSource1).Or.Contain(pageSource2), "The Page is Containing Congratulations Or Successfully Logged in");
                Assert.That(driver.FindElement(logoutBtn).Displayed, "Log Out Button Has Been Displayed");
            });
        }

    }
}
