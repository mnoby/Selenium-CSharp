using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumProject.Utils
{
    public class WebDriverFactory
    {
        public IWebDriver Create(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    //ChromeOptions options = new();
                    //options.AddArgument("--headless");
                    //return new ChromeDriver(options);
                    return new ChromeDriver();

                case BrowserType.Firefox:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();

                case BrowserType.Edge:
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();

                default:
                    throw new ArgumentOutOfRangeException("No Such Browser exists");
            }
        }
    }
}
