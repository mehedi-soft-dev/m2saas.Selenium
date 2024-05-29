using M2Saas.Crm.Website.Drivers;
using M2Saas.Crm.Website.Models.PageObjects;
using M2Saas.Crm.Website.Models.TestData;
using OpenQA.Selenium;

namespace M2Saas.Crm.Website.Tests;

public class LoginTests
{
    private IWebDriver _driver;
    private LoginPage _loginPage;
    private static string testDataPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Models", "TestData", "LoginData.xlsx");

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _driver = WebDriverManager.GetDriver("chrome");
    }

    [SetUp]
    public void Setup()
    {
        _loginPage = new LoginPage(_driver);
        _driver.Navigate().GoToUrl("https://frontend-dev.onnorokom.cloud/Login");
    }

    [Test]
    public void Login_WithValidCredentials_ShouldSucceed()
    {
        var validUser = LoginData.ValidUser;
        _loginPage.Login(validUser.Username, validUser.Password);
    }

    [Test]
    public void Login_WithInvalidCredentials_ShouldFailed()
    {
        var validUser = LoginData.InvalidUser;
        _loginPage.Login(validUser.Username, validUser.Password);
    }

    [Test, TestCaseSource(nameof(GetTestData))]
    public void Login_Test(LoginData loginData)
    {
        _loginPage.Login(loginData.Username, loginData.Password);
        var actionResult = CheckLoginSuccess(_driver);
        Assert.AreEqual(true, actionResult);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (_driver != null)
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }

    public static IEnumerable<LoginData> GetTestData()
    {
        return LoginData.GetTestDataFromExcel(testDataPath);
    }

    private bool CheckLoginSuccess(IWebDriver driver)
    {
        try
        {
            var dashboardElement = driver.FindElement(By.ClassName("title"));
            return dashboardElement.Displayed && dashboardElement.Text == "Dashboard";
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}