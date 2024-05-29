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
    private IWebElement LoginButton => _driver.FindElement(By.XPath("/html/body/main/section/form/div/div[3]/button"));
    private IWebElement DashboardElement = null;

    public void Login(string username, string password)
    {
        EmailField.SendKeys(username);
        PasswordField.SendKeys(password);
        LoginButton.Click();
    }
}