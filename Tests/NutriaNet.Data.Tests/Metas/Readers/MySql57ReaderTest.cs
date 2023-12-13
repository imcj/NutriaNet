using NutriaNet.Data.Metas.Readers.MySql;
using NutriaNet.Data.Tests.Metas.Databases;

namespace NutriaNet.Data.Tests.Metas.Readers;

public class MySql57ReaderTest : IClassFixture<MySQLFixture>
{

    public MySQLFixture fixture;

    public MySql57ReaderTest(MySQLFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    [MySqlTest]
    public async Task TestReadTable()
    {
        var connection = fixture.GetDbConnection();
        await connection.OpenAsync();

        await fixture.CreateSimpleTable(connection);

        var mysql = new MySql57Reader(connection);
        var table = await mysql.TableAsync("Person");

        Assert.Equal("Person", table.Name);
        Assert.Equal("Id", table.Columns.FirstOrDefault()?.Name);
    }
}
