using MySql.Data.MySqlClient;
using System.Data.Common;

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

    public DbConnection GetDbConnection(string? database = "test")
    {
        var host = GetEnvDefault("MySQL_HOST", "127.0.0.1");
        var user = GetEnvDefault("MySQL_USER", "root");
        var password = GetEnvDefault("MySQL_PASSWORD", "123456");
        database = database == null ? "" : "database=" + GetEnvDefault("MySQL_DATABASE", database) + ";";

        var connectionString = $"Server={host};uid={user};pwd={password};{database}";
        using var connection = new MySqlConnection(connectionString);

        return connection;
    }
}
