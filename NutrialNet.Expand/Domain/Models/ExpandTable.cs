using NutrialNet.Expand.Domain.Models.Columns;

namespace NutrialNet.Expand.Domain.Models;

public class ExpandTable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string Key { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<ExpandColumn> Columns { get; set; } = new List<ExpandColumn>();
}
