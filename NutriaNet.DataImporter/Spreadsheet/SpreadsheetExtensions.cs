using NPOI.SS.UserModel;
using NutriaNet.DataImporter.Domain.Model;

namespace NutriaNet.DataImporter.Spreadsheet;

public static class SpreadsheetExtensions
{
    public static Workbook ToWorkbook(this IWorkbook workbook)
    {
        for (int i = 0, size = workbook.NumberOfSheets; i < size; i++)
        {

        }

        return new Workbook();
    }

    public static Sheet ToSheet(this ISheet sheet)
    {
        return new Sheet();
    }

    public static Row ToRow(this IRow row)
    {
        return null;
    }
}
