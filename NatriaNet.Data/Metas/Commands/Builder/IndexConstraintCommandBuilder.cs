namespace NutriaNet.Data.Metas.Commands.Builder;

using IndexConstraint = Constraints.IndexConstraint;

public class IndexConstraintCommandBuilder
{
    protected string? name;

    protected List<string> columns = new();

    protected bool isUnique = false;

    public static IndexConstraintCommandBuilder Create(string? name = null)
    {   
        return new IndexConstraintCommandBuilder() { name = name };
    }

    public IndexConstraintCommandBuilder Name(string name)
    {
        this.name = name;
        return this;
    }

    public IndexConstraintCommandBuilder Column(string name)
    {
        columns.Add(name);
        return this;
    }

    public IndexConstraintCommandBuilder Column(params string[] columns)
    {
        this.columns.AddRange(columns);
        return this;
    }

    public IndexConstraintCommandBuilder Unique(bool isUnique)
    {
        this.isUnique = isUnique;
        return this;
    }

    public IndexConstraint Build()
    {
        return new(
            name,
            columns,
            isUnique
        );
    }
}
