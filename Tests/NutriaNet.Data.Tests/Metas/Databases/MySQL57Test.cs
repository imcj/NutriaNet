using MySql.Data.MySqlClient;
using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Commands.Builder;
using NutriaNet.Data.Metas.Commands.Extensions;

namespace NutriaNet.Data.Tests.Metas.Databases;

public class MySQL57Test : IClassFixture<MySQLFixture>
{

    protected MySQLFixture fixture;

    public MySQL57Test(MySQLFixture fixture)
    {
        this.fixture = fixture;
    }

    
    [Fact]
    public async Task TestConnect()
    {
        var host = fixture.GetEnvDefault("MySQL_HOST", "127.0.0.1");
        var user = fixture.GetEnvDefault("MySQL_USER", "root");
        var password = fixture.GetEnvDefault("MySQL_PASSWORD", "123456");
        var database = fixture.GetEnvDefault("MySQL_DATABASE", "test");

        using var connection = new MySqlConnection($"Server={host};uid={user};pwd={password};database={database}");
        await connection.OpenAsync();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = "show variables like 'version%'";
        
        using var reader = await cmd.ExecuteReaderAsync();

        var excepted = false;
        while (await reader.ReadAsync())
        {
            excepted = reader.GetString(0) == "version";
            if (excepted)
            {
                break;
            }
        }

        Assert.True(excepted);
        
    }

    [Fact]
    [MySqlTest]
    public async Task TestCreateTable()
    {
        var builder = CreateTableCommandBuilder
            .Create("Person")
            .Column(col => col.Name("Id").Type(ColumnType.Int).Length(2).PrimaryKey(true).Nullable(false).IsAutoIncrement(true));

        var command = builder.Build();
        var text = command.ToCommandText(DatabaseProduct.MySql57);

        using var connection = fixture.GetDbConnection();

        await connection.OpenAsync();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = text;
        await cmd.ExecuteNonQueryAsync();
    }


    [Fact]
    [MySqlTest]
    public async Task TestCreateForeignKey()
    {
        var builder = CreateTableCommandBuilder
            .Create("Person")
            .Column(col => col.Name("Id").Type(ColumnType.Int).Length(2).PrimaryKey(true).Nullable(false).IsAutoIncrement(true))
            .ForeignKeyConstraint(fk => fk.Name("FK_Wife").Columns("WifeId").Reference("Person").ReferenceColumns("Id"));

        var command = builder.Build();
        var text = command.ToCommandText(DatabaseProduct.MySql57);

        using var connection = fixture.GetDbConnection();

        await connection.OpenAsync();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = text;
        await cmd.ExecuteNonQueryAsync();
    }
}
