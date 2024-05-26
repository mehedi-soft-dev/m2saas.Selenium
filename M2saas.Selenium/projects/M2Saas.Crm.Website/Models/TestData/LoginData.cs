using M2Saas.Crm.Website.Helpers;

namespace M2Saas.Crm.Website.Models.TestData;

public class LoginData
{
    public string Username { get; set; }
    public string Password { get; set; }

    public static LoginData ValidUser => new LoginData
    {
        Username = "ripon@onnorokom.com",
        Password = "KawranBazar12#"
    };

    public static LoginData InvalidUser => new LoginData
    {
        Username = "invalidUsername",
        Password = "invalidPassword"
    };

    public static IEnumerable<LoginData> GetTestDataFromExcel(string filePath)
    {
        var loginDataList = new List<LoginData>();
        var data = ExcelHelper.ReadExcelData(filePath, "LoginData");

        foreach (var row in data)
        {
            loginDataList.Add(new LoginData
            {
                Username = row["Username"],
                Password = row["Password"]
            });
        }

        return loginDataList;
    }
}