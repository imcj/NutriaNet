namespace NutriaNet.Data.Metas.Builders;

public class ColumnBuilder
{
    protected string name;

    protected ColumnType columnType;

    protected int length = -1;

    protected bool primaryKey = false;

    protected bool nullable = false;

    protected bool isAutoIncrement = false;

    public ColumnBuilder Name(string name)
    {
        this.name = name;
        return this;
    }

    public ColumnBuilder Type(ColumnType columnType)
    {
        this.columnType = columnType;
        return this;
    }

    public ColumnBuilder Length(int length)
    {
        this.length = length;
        return this;
    }

    public ColumnBuilder PrimaryKey(bool primaryKey)
    {
        this.primaryKey = primaryKey;
        return this;
    }

    public ColumnBuilder IsAutoIncrement(bool isAutoIncrement)
    {
        this.isAutoIncrement = isAutoIncrement;
        return this;
    }

    public ColumnBuilder Nullable(bool nullable)
    {
        this.nullable = nullable;
        return this;
    }

    public Column Build()
    {
        var column = new Column(name, columnType, length, primaryKey)
        {
            IsAutoIncrement = isAutoIncrement,
            Nullable = nullable
        };
        return column;
    }
}
