using Microsoft.Data.Sqlite;
using NutriaNet.Data.Metas.Commands;
using NutriaNet.Data.Metas.Commands.Builder;

namespace NutriaNet.Data.Tests.Metas.Commands;

public class SqliteCommandTextTest
{
    [Fact]
    public void TestCreateTable()
    {
        var person = CreateTableCommandBuilder.Create("Person")
            .Column(col => col.Name("Id").Type(Data.Metas.ColumnType.Int))
            .Column(col => col.Name("Name").Type(Data.Metas.ColumnType.Varchar).Length(255))
            .Column(col => col.Name("FriendId").Type(Data.Metas.ColumnType.Int))
            .ForeignKeyConstraint(fk => fk.Name("FK_Friend").Columns("FriendId").Reference("Person").ReferenceColumns("Id"))
            .Index(i => i.Name("Index_FriendId").Column("Id"))
            .Build();

        var commandText = new SqliteCommandText();
        var text = commandText.CreateTable(person);

        var expect = @"CREATE TABLE Person (
    Id INTEGER,
    Name VARCHAR(255),
    FriendId INTEGER,
    CONSTRAINT FK_Friend
        FOREIGN KEY (FriendId)
        REFERENCES Person(Id)
);
CREATE INDEX Index_FriendId ON Person(Id);"
            .Replace("\r\n", "\n");

        Assert.Equal(expect, text);

        var connection = new SqliteConnection("Data source=:memory:");
        connection.Open();
        var cmd = connection.CreateCommand();
        cmd.CommandText = text;
        cmd.ExecuteNonQuery();

        connection.Close();
    }
}
