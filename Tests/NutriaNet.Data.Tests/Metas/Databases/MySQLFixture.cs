using MySql.Data.MySqlClient;
using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Commands.Builder;
using System.Data.Common;
using NutriaNet.Data.Metas.Commands.Extensions;

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

    public async Task CreateSimpleTable(DbConnection connection)
    {
        var builder = CreateTableCommandBuilder
            .Create("Person")
            .Column(col => col
                .Name("Id")
                .Type(ColumnType.Int)
                .Length(2)
                .PrimaryKey(true)
                .Nullable(false)
                .IsAutoIncrement(true)
            );

        var cmd = builder.Build();
        var text = cmd.ToCommandText(DatabaseProduct.MySql57);

        using var c = connection.CreateCommand();
        c.CommandText = text;
        
        c.ExecuteNonQuery();
        
    }
}
