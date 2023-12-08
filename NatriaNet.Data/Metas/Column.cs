
namespace NutriaNet.Data.Metas;

public class Column
{

    public string Name { get; }

    public ColumnType Type { get; }

    public int Length { get; }

    public bool PrimaryKey { get; set; } = false;

    public bool IsAutoIncrement { get; set; } = false;

    public bool Nullable { get; set; } = false;



    public Column(string name, ColumnType type, int length, bool primaryKey = false)
    {
        Name = name;
        Type = type;
        Length = length;
        PrimaryKey = primaryKey;
    }
}
