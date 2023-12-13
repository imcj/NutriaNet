namespace NutriaNet.Data.Metas.Commands.Builder;

public class CreateIndexCommandBuilder
{
    protected string? name;

    protected bool isUnique = false;

    protected List<string> columns = new List<string>();

    public CreateIndexCommandBuilder Create()
    {
        return new CreateIndexCommandBuilder();
    }

    public CreateIndexCommandBuilder Name(string name)
    {
        this.name = name;
        return this;
    }

    public CreateIndexCommandBuilder Column(string column)
    {
        columns.Add(column);
        return this;
    }

    public CreateIndexCommandBuilder IsUnique(bool isUnique)
    {
        this.isUnique = isUnique;
        return this;
    }

    public CreateIndexCommandBuilder Columns(params string[] columns)
    {
        this.columns.AddRange(columns);
        return this;
    }

    public Constraints.IndexConstraint Build()
    {
        return new(name, columns, isUnique);
    }
}
