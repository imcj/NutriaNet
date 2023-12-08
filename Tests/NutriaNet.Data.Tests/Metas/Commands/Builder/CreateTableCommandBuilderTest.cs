using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Commands.Builder;

namespace NutriaNet.Data.Tests.Metas.Commands.Builder;

public class CreateTableCommandBuilderTest
{

    [Fact]
    public void TestCreateTableCommandBuilder()
    {
        var builder = CreateTableCommandBuilder.Create("Person")
            .Column(column => column.Name("Name").Type(ColumnType.Varchar).Length(255))
            .ForeignKeyConstraint(c => c.Name("FK_Friend").Reference("Person", "Id").Column("FriendId"))
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
        Assert.Equal("Person", constraint.ReferenceTable.Name);
        Assert.Equal("Id", constraint.References.First().Name);

        var index = (IndexConstraint)command.Constraints.Last();

        Assert.Equal("Index_FriendId", index.Name);
        Assert.Equal("FriendId", index.Columns.First());
        Assert.False(index.IsUnique);
    }
}
