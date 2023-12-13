namespace NutriaNet.Data.Metas.Commands.Builder;

public class ForeignKeyConstraintCommandBulder
{
    protected string? name;

    protected List<string> columns = new ();

    protected string? reference;

    protected List<string> referenceColumns = new();

    public ForeignKeyConstraintCommandBulder Name(string name)
    {
        this.name = name;
        return this;
    }

    public ForeignKeyConstraintCommandBulder Columns(params string[] columns)
    {
        this.columns.AddRange(columns);
        return this;
    }

    public ForeignKeyConstraintCommandBulder Reference(string reference)
    {
        this.reference = reference;
        return this;
    }

    public ForeignKeyConstraintCommandBulder ReferenceColumns(params string[] columns)
    {
        this.referenceColumns.AddRange(columns);
        return this;
    }

    public Constraints.ForeignKeyConstraint Build()
    {
        return new(name, columns, reference!, referenceColumns);
    }
}
