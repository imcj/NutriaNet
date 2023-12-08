namespace NutriaNet.Data.Metas.Commands;

public class CreateIndexCommand
{
    public string Name { get; }

    public string Table { get; }

    public IEnumerable<string> Columns { get; } = new List<string>();

    public CreateIndexCommand(string name, string table, IEnumerable<string> columns)
    {
        Name = name;
        Table = table;
        Columns = columns;
    }
}
