
using SeleniumProject.Utils;

namespace SeleniumProject.Pages2
{
    public class CheckboxAction : Inits
    {
        int _index;
        readonly By checkboxEl = By.XPath("//input[@type=\"checkbox\"]");

        public CheckboxAction(IWebDriver driver, WebDriverWait wait, string browser, string version, string platform) : base(browser, version, platform)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public void ThickCheckboxes()
        {
            _index = 1;
            var checkboxes = driver.FindElements(checkboxEl);

            foreach (var box in checkboxes)
            {
                IWebElement check = driver.FindElement(By.XPath($"//input[@type=\"checkbox\"][{_index}]"));
                if (box.GetAttribute("checked") == null)
                {
                    check.Click();
                    _index++;
                }
                Assert.That(box.GetAttribute("checked"), Is.EqualTo("true"));
            }
            Console.WriteLine("SUCCESS ! -- The Checkboxes Has Been Thicked");
        }

        public void UnthickCheckboxes()
        {
            _index = 1;
            var checkboxes = driver.FindElements(checkboxEl);

            foreach (var box in checkboxes)
            {
                IWebElement check = driver.FindElement(By.XPath($"//input[@type=\"checkbox\"][{_index}]"));
                if (box.GetAttribute("checked") == "true")
                {
                    check.Click();
                    _index++;
                }
                Assert.That(box.GetAttribute("checked"), Is.Null);
            }
            Console.WriteLine("SUCCESS ! -- The Checkboxes Has Been Unthicked");
        }
    }

}
