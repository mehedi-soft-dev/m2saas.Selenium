using OpenQA.Selenium;

namespace M2Saas.Crm.Website.Models.PageObjects;

public class LoginPage
{
    private readonly IWebDriver _driver;

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    private IWebElement EmailField => _driver.FindElement(By.XPath("//*[@id=\"Email\"]"));
    private IWebElement PasswordField => _driver.FindElement(By.XPath("//*[@id=\"Password\"]"));
    private IWebElement LoginButton => _driver.FindElement(By.XPath("//*[@id=\"login-submit\"]"));

    public bool Login(string username, string password)
    {
        EmailField.SendKeys(username);
        PasswordField.SendKeys(password);
        LoginButton.Click();

        return CheckLoginSuccess(_driver);
    }

    private bool CheckLoginSuccess(IWebDriver driver)
    {
        try
        {
            var dashboardElement = driver.FindElement(By.ClassName("page-header"));
            return dashboardElement.Displayed && dashboardElement.Text == "Dashboard";
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}