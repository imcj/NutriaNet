namespace NutrialNet.Expand.Domain.Models.Columns;

public class ExpandColumn
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string Key { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Discriminator { get; set; } = "ExpandColumn";
}
