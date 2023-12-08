namespace NutriaNet.Data.Metas.Builders;

public class IndexConstraintBuilder
{
    protected string? name;

    protected string? tableName;

    protected List<string> columns = new();

    protected bool isUnique = false;

    public static IndexConstraintBuilder Create(string? name = null)
    {   
        return new IndexConstraintBuilder() { name = name };
    }

    public IndexConstraintBuilder Name(string name)
    {
        this.name = name;
        return this;
    }

    public IndexConstraintBuilder TableName(string tableName)
    {
        this.tableName = tableName;
        return this;
    }

    public IndexConstraintBuilder Column(string name)
    {
        columns.Add(name);
        return this;
    }

    public IndexConstraintBuilder Column(params string[] columns)
    {
        this.columns.AddRange(columns);
        return this;
    }

    public IndexConstraintBuilder Unique(bool isUnique)
    {
        this.isUnique = isUnique;
        return this;
    }

    public IndexConstraint Build()
    {
        return new(
            name,
            columns,
            tableName,
            isUnique
        );
    }
}
