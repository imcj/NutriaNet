namespace NutriaNet.Data.Meta;

public class Database
{
    public string Name { get; set; }

    public IEnumerable<Entity> Entities { get; set; }

    public Database(string name, IEnumerable<Entity> entities)
    {
        Name = name;
        Entities = entities;
    }
}
