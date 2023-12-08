namespace NutriaNet.Data.Metas;

public class ForeignKeyReference
{
    public Table Table { get; }

    public Column Column { get; }

    public ForeignKeyReference(Table table, Column column)
    {
        Table = table;
        Column = column;
    }
}
