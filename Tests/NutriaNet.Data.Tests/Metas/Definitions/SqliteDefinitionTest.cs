using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Builders;
using NutriaNet.Data.Metas.Definitions;

namespace NutriaNet.Data.Tests.Metas.Definitions;

public class SqliteDefinitionTest
{
    [Fact]
    public void TestCreateTable()
    {
        var table = new Table(
            "Teacher",
            new List<Column>()
            {
                new Column("Name", ColumnType.Text, 0)
            }
        );

        var definition = new SqliteDefinition();
        var sql = definition.CreateTable(table);
        var except = @"CREATE TABLE Teacher (
    Name TEXT
)";

        Assert.Equal(except, sql);
    }

    [Fact]
    public void TestHasForeignKey()
    {
        var people = TableBuilder
            .Create()
            .Name("People")
            .Column(col => col.Name("Id").Type(ColumnType.Int))
            .Column(col => col.Name("WifeId").Type(ColumnType.Int))
            .ForeignKey(fk => fk.Name("FK_WIFE").Column("Id").Reference("People", "WifeId"))
            .Build();

        var definition = new SqliteDefinition();
        var sql = definition.CreateTable(people);
    }
}
