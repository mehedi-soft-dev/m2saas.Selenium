using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace M2Saas.Crm.Website.Helpers;

public class ExcelHelper
{
    public static List<Dictionary<string, string>> ReadExcelData(string filePath, string sheetName)
    {
        var data = new List<Dictionary<string, string>>();

        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook workbook = new XSSFWorkbook(fs);
            ISheet sheet = workbook.GetSheet(sheetName);

            if (sheet != null)
            {
                var headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    var rowData = new Dictionary<string, string>();

                    for (int j = 0; j < cellCount; j++)
                    {
                        var cell = row.GetCell(j);
                        rowData[headerRow.GetCell(j).ToString()] = cell?.ToString();
                    }

                    data.Add(rowData);
                }
            }
        }

        return data;
    }
}