using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Tests.Metas.Commands;

public class SqliteTest
{

    [Fact]
    public void TestSqlite()
    {
        var connection = new SqliteConnection("Data source=:memory:");
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = @"CREATE TABLE Person (
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
)";
        cmd.ExecuteNonQuery();
    }
}
