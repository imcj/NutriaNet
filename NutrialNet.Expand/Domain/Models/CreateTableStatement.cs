using NutrialNet.Expand.Domain.Models.Columns;

namespace NutrialNet.Expand.Domain.Models;

public class CreateTableStatement : IDefinitionStatement
{
    public string Key { get; set; }

    public string Name { get; set; }

    public IEnumerable<ExpandColumn> Columns { get; set; }
}
