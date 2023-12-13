namespace NutriaNet.Data.Metas.Commands;

public class CreateTableCommand : ICommand
{
    public string Name { get; set; }

    public string? Comment { get; set; }

    public ICollection<Column> Columns { get; set; } = new List<Column>();

    public ICollection<Constraints.Constraint> Constraints { get; set; } = new List<Constraints.Constraint>();

}
