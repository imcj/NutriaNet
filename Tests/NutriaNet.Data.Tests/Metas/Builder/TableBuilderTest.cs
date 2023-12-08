using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Builders;

namespace NutriaNet.Data.Tests.Metas.Builder;

public class TableBuilderTest
{
    [Fact]
    public void TestTableBuilder()
    {
        var builder = new TableBuilder();
        var table = builder
            .Name("People")
            .Column(cb => cb.Name("Id").Type(ColumnType.Int))
            .Column(cb => cb.Name("Name").Type(ColumnType.Text))
            .Column(cb => cb.Name("Age").Type(ColumnType.Int))
            .ForeignKey(fk => fk.Name("FK_WIFE").Column("Id").Reference("People", "Id"))
            .Build();

        Assert.Equal("People", table.Name);
        Assert.Equal("Name", table.Columns.First().Name);
    }
}
