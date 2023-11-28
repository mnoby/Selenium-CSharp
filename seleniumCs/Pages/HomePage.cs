using OpenQA.Selenium.Interactions;

namespace seleniumCs;
public class HomePage
{
    IWebDriver? _driver;
    WebDriverWait? _wait;
    Actions _actions;

    readonly By primaryMenu = By.Id("menu-primary-items");
    readonly By practiceBtn = By.XPath("//a[contains(text(),'Practice')]");
    public readonly By subscribeBtn = By.XPath("//*[@data-automation-id=\"subscribe-submit-button\"]");

    public HomePage(IWebDriver driver, WebDriverWait wait, Actions actions)
    {
        this._driver = driver;
        this._wait = wait;
        this._actions = actions;
    }

    public void VerifyHomePage(string pageSource)
    {
        try
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(primaryMenu));
            Assert.That(_driver.PageSource, Does.Contain($"{pageSource}"));
            Console.WriteLine("Success -- Access to Home Page");

        }
        catch (Exception)
        {
            throw new TimeoutException("IT TAKES TOO LONG BROO !");
        }
    }

    public void CLickPracticeMenu()
    {
        _wait.Until(ExpectedConditions.ElementToBeClickable(practiceBtn)).Click();
    }

    public void HoverSubscribeBtn(string cssAttr)
    {
        _actions = new(_driver);
        IWebElement findSubscribeBtn = _driver.FindElement(subscribeBtn);
        var firstSubscribeColor = findSubscribeBtn.GetCssValue(cssAttr);

        _actions
            .MoveToElement(findSubscribeBtn)
        .Perform();

        var secondSubscribeColor = findSubscribeBtn.GetCssValue(cssAttr);
        Assert.AreNotEqual(secondSubscribeColor, firstSubscribeColor, "The Button Color Has Been Changed due to mouse hover!");
        Thread.Sleep(5000);
    }

}
