using System.Collections.Immutable;

namespace NutriaNet.DataImporter.Domain.Model;

public class Row
{
    public ImmutableList<object> Values { get; }

    public Row(ImmutableList<object> values)
    {
        Values = values;
    }
}
