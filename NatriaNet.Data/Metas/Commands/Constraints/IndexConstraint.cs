namespace NutriaNet.Data.Metas.Commands.Constraints;

public class IndexConstraint : Constraint
{
    public string? Name { get; }

    public IEnumerable<string> Columns { get; set; } = new List<string>();

    public bool IsUnique { get; set; } = false;

    public IndexConstraint(string? name, IEnumerable<string> columns, bool isUnique)
    {
        Name = name;
        Columns = columns;
        IsUnique = isUnique;
    }
}
