using Microsoft.Data.Sqlite;
using NutriaNet.Data.Metas.Definitions;
using NutriaNet.Data.Metas.Readers;
using NutriaNet.Data.Tests.Metas.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Tests.Metas.Readers;

public class SqliteReaderTest: IClassFixture<SqliteFixture>
{

    protected SqliteFixture fixture;

    public SqliteReaderTest(SqliteFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task TestTable()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        //connection.Open();

        var people = fixture.CreatePeopleTable();
        var ddl = new SqliteDefinition();
        var sql = ddl.CreateTable(people);

        var command = new SqliteCommand(sql, connection);
        await command.ExecuteNonQueryAsync();
        
        command = new SqliteCommand("INSERT INTO People VALUES (1);", connection);
        var effect = await command.ExecuteNonQueryAsync();

        var reader = new SqliteReader(connection);
        var table = await reader.TableAsync("People");
    }
}
