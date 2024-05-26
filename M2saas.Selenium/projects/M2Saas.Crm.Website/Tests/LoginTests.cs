using M2Saas.Crm.Website.Drivers;
using M2Saas.Crm.Website.Models.PageObjects;
using M2Saas.Crm.Website.Models.TestData;
using OpenQA.Selenium;

namespace M2Saas.Crm.Website.Tests;

public class LoginTests
{
    private IWebDriver _driver;
    private LoginPage _loginPage;
    private static IEnumerable<LoginData> _loginTestData;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        string testDataPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Models", "TestData", "LoginTest.xlsx");
        _loginTestData = LoginData.GetTestDataFromExcel(testDataPath);
    }

    [SetUp]
    public void Setup()
    {
        _driver = WebDriverManager.GetDriver("chrome");
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

    [Test, TestCaseSource(nameof(_loginTestData))]
    public void Login_Test(LoginData loginData)
    {
        _loginPage.Login(loginData.Username, loginData.Password);
        // Add assertions here to verify login outcome
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Dispose();
    }
}