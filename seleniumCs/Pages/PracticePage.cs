namespace seleniumCs.Pages
{
    public class PracticePage
    {
        IWebDriver driver;
        WebDriverWait wait;
        readonly By loginBtn = By.XPath("//a[contains(text(),'Test Login Page')]");


        public PracticePage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public void clickTestLogin()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(loginBtn)).Click();
        }

    }
}
