namespace NutriaNet.Data.Tests.Metas.Databases;

public class MySQLFixture
{
    public string GetEnvDefault(string name, string defaultValue)
    {
        var value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrEmpty(value))
        {
            return defaultValue;
        }

        return value;
    }
}
