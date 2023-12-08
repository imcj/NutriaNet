using NPOI.SS.UserModel;
using NutriaNet.DataImporter.Domain.Model;
using System.Collections.Immutable;

namespace NutriaNet.DataImporter.Spreadsheet;

public class SpreadsheetAssembler
{

    public Row ToRow(IRow row)
    {
        var values = Enumerable
            .Range(0, row.LastCellNum)
            .Select(index =>
            {
                var cell = row.GetCell(index);
                return ToPlainObject(cell);
            })
            .ToImmutableList();

        return new Row(values);
    }

    public object ToPlainObject(ICell cell)
    {
        object target = new Unknown();

        switch (cell.CellType) 
        {
            case CellType.Numeric:
            {
                if (DateUtil.IsCellDateFormatted(cell))
                {
                    target = DateTime.FromOADate(cell.NumericCellValue);
                }
                else
                {
                    target = cell.NumericCellValue;
                }
                break;
            }
        }


        return target;
    }
}
