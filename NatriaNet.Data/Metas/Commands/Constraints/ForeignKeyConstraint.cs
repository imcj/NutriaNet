namespace NutriaNet.Data.Metas.Commands.Constraints;

public class ForeignKeyConstraint : Constraint
{
    public string? Name {  get; }

    public IEnumerable<string> Columns { get; }

    public string Reference { get; }

    public IEnumerable<string> ReferenceColumns { get; }

    public ForeignKeyConstraint(string? name,  IEnumerable<string> columns, string reference, IEnumerable<string> referenceColumns)
    {
        Name = name;
        Columns = columns;
        Reference = reference;
        ReferenceColumns = referenceColumns;
    }
}
