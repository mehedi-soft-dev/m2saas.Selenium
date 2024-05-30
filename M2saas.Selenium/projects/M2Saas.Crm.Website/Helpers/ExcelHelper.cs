using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace M2Saas.Crm.Website.Helpers;

public class ExcelHelper
{
    private string _filePath;
    private IWorkbook _workbook;
    private ISheet _sheet;

    public ExcelHelper(string filePath)
    {
        _filePath = filePath;
        EnsureDirectoryExists();
        InitializeWorkbook();
    }

    public void InitializeWorkbook()
    {
        if(File.Exists(_filePath))
        {
            using var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            _workbook = new XSSFWorkbook(fs);
        }
        else
        {
            _workbook = new XSSFWorkbook();
        }

        _sheet = _workbook.GetSheet("LoginTestResults") ?? _workbook.CreateSheet("LoginTestResults");
    }

    private void EnsureDirectoryExists()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public void WriteResult(params object[] values)
    {
        int rowIndex = _sheet.PhysicalNumberOfRows;
        var row = _sheet.CreateRow(rowIndex);

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] is string)
                row.CreateCell(i).SetCellValue((string)values[i]);
            else if (values[i] is bool)
                row.CreateCell(i).SetCellValue((bool)values[i]);
            else if (values[i] is int)
                row.CreateCell(i).SetCellValue((int)values[i]);
            else if (values[i] is double)
                row.CreateCell(i).SetCellValue((double)values[i]);
            // Add more types as needed
            else
                row.CreateCell(i).SetCellValue(values[i]?.ToString());
        }

        Save();
    }

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

    private void Save()
    {
        using var fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
        _workbook.Write(fs);
    }
}