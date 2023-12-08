using MySql.Data.MySqlClient;

namespace NutriaNet.Data.Tests.Metas.Databases;

public class MySQLTest : IClassFixture<MySQLFixture>
{

    protected MySQLFixture fixture;

    public MySQLTest(MySQLFixture fixture)
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
}
