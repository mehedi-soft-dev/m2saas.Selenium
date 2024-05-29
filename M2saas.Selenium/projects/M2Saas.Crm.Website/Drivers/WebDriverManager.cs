using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace M2Saas.Crm.Website.Drivers;

public class WebDriverManager
{
    private static IWebDriver _driver;

    public static IWebDriver GetDriver(string browser)
    {
        if (_driver == null)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    _driver = new ChromeDriver();
                    break;
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                default:
                    throw new ArgumentException("Unsupported browser: " + browser);
            }
        }

        return _driver;
    }

    public static void QuitDriver()
    {
        if (_driver != null)
        {
            _driver.Quit();
            _driver = null;
        }
    }
}