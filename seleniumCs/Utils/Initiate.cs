using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;


public class Initiate
{
	protected IWebDriver driver;
	protected WebDriverWait wait;

	public Initiate()
	{
		BrowserInitialize();
	}

	private void BrowserInitialize() 
	{
		ChromeOptions option = new();
		option.AddArgument("--headless");
		option.AddArgument("--disable-gpu");
		driver = new ChromeDriver(option);
		driver.Manage().Window.Maximize();	
		driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
	}

	public void NavigateToUrl(string url)
	{
		driver.Navigate().GoToUrl(url);	
	}

	public void Close()
	{
		driver.Quit();
	}

	public void ExplicitWaitVisible(object element)
	{
		wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
		var waitingEl = wait.Until(ExpectedConditions.ElementIsVisible((By)element));
        Assert.IsTrue(waitingEl.Displayed);
	}

    public void ExplicitWaitClickable(object element)
    {
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(ExpectedConditions.ElementToBeClickable((By)element)).Click();
    }
}
