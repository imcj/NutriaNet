using Microsoft.Data.Sqlite;
using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Builders;
using NutriaNet.Data.Metas.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Tests.Metas.Definitions;

public class SqliteTest
{
    [Fact]
    public void CreateTable()
    {
        var table = TableBuilder.Create()
            .Name("People")
            .Column(col => col.Name("Id").Type(ColumnType.Int))
            .Build();

        var connection = new SqliteConnection("Data Source=:memory:");

        var ddl = new SqliteDefinition();
        var sql = ddl.CreateTable(table);

        connection.Open();

        var command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();
    }
}
