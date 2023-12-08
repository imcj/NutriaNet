namespace NutriaNet.Data.Meta;

public class Entity
{
    public string Name { get; }

    public IEnumerable<Property> Properties { get; }

    public Entity(string name, IEnumerable<Property> properties)
    {
        Name = name;
        Properties = properties;
    }
}
