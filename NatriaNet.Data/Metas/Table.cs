namespace NutriaNet.Data.Metas;

public class Table
{
    protected string _name;

    public string Name { get => _name; set => _name = value; }

    public List<Column> Columns { get; } = new();

    public List<Constraint> Constraints { get; } = new();

    public Table(string name) : this(name, new())
    {
    }

    public Table(string name, List<Column> columns)
    {
        _name = name;
        Columns = columns;
    }

    public void AddColumn(Column column)
    {
        Columns.Add(column);
    }

    public void AddConstraints(IEnumerable<Constraint> constraints) => Constraints.AddRange(constraints);

    public Column GetColumn(string name) => Columns.Single(c => c.Name == name);
}
