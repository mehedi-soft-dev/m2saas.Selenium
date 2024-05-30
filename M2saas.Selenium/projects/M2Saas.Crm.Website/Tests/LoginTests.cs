using M2Saas.Crm.Website.Drivers;
using M2Saas.Crm.Website.Helpers;
using M2Saas.Crm.Website.Models.PageObjects;
using M2Saas.Crm.Website.Models.TestData;
using OpenQA.Selenium;

namespace M2Saas.Crm.Website.Tests;

public class LoginTests
{
    private IWebDriver _driver;
    private LoginPage _loginPage;
    private string _logingPageUrl = "https://casckurmitola-beta.osl.ac/Account/Login";
    private static string _testDataPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Models", "TestData", "LoginData.xlsx");
    private static string _loginTestResult = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestResult", "LoginPage", "LoginTestResult.xlsx");
    private ExcelHelper _excelHelper;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _driver = WebDriverManager.GetDriver("chrome");
    }

    [SetUp]
    public void Setup()
    {
        _loginPage = new LoginPage(_driver);
        _driver.Navigate().GoToUrl(_logingPageUrl);
        _excelHelper = new ExcelHelper(_loginTestResult);
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
        var actionResult = _loginPage.Login(loginData.Username, loginData.Password);
        _excelHelper.WriteResult(DateTime.Now, _logingPageUrl, loginData.Username, loginData.Password, actionResult);
        //Assert.AreEqual(true, actionResult);
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
        return LoginData.GetTestDataFromExcel(_testDataPath);
    }
}