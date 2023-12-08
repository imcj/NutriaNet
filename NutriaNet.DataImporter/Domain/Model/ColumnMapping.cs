namespace NutriaNet.DataImporter.Domain.Model;

public class ColumnMapping
{
    public string EntityProperty { get; }

    public string Column { get; }

    public int Index { get; }

    public ColumnMapping(string entityProperty, string spreadsheetColumnHeader, int index)
    {
        EntityProperty = entityProperty;
        Column = spreadsheetColumnHeader;
        Index = index;
    }
}
