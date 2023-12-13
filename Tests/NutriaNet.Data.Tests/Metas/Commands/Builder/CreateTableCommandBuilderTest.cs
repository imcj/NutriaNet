using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Commands.Builder;

using ForeignKeyConstraint = NutriaNet.Data.Metas.Commands.Constraints.ForeignKeyConstraint;
using IndexConstraint = NutriaNet.Data.Metas.Commands.Constraints.IndexConstraint;

namespace NutriaNet.Data.Tests.Metas.Commands.Builder;

public class CreateTableCommandBuilderTest
{

    [Fact]
    public void TestCreateTableCommandBuilder()
    {
        var builder = CreateTableCommandBuilder.Create("Person")
            .Column(column => column.Name("Name").Type(ColumnType.Varchar).Length(255))
            .ForeignKeyConstraint(c => c.Name("FK_Friend").Reference("Person").ReferenceColumns("Id").Columns("FriendId"))
            .Index(i => i.Name("Index_FriendId").Column("FriendId").Unique(false))
            .Comment("Comment describe the table");

        var command = builder.Build();

        Assert.NotNull(command);
        Assert.Equal("Person", command.Name);

        var column = command.Columns.First();
        Assert.Equal("Name", column.Name);
        Assert.Equal(255, column.Length);
        Assert.Equal(ColumnType.Varchar, column.Type);

        var constraint = (ForeignKeyConstraint)command.Constraints.First();

        var foreignKeyColumn = constraint.Columns.First();
        Assert.Equal("FK_Friend", constraint.Name);
        Assert.Equal("Person", constraint.Reference);
        Assert.Equal("Id", constraint.ReferenceColumns.First());

        var index = (IndexConstraint)command.Constraints.Last();

        Assert.Equal("Index_FriendId", index.Name);
        Assert.Equal("FriendId", index.Columns.First());
        Assert.False(index.IsUnique);
    }
}
