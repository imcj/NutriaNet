using NutriaNet.Data.Metas.Builders;

namespace NutriaNet.Data.Metas.Commands.Builder;

public class CreateTableCommandBuilder
{
    protected string name = "";

    protected string? comment;

    protected List<Column> columns = new();

    protected List<Constraint> constraints = new();

    public CreateTableCommandBuilder Name(string name)
    {
        this.name = name;
        return this;
    }

    public CreateTableCommandBuilder Comment(string description)
    {
        this.comment = description;
        return this;
    }

    public CreateTableCommandBuilder Column(Action<ColumnBuilder> action)
    {
        var builder = new ColumnBuilder();
        action(builder);
        var column = builder.Build();
        columns.Add(column);
        return this;
    }

    public CreateTableCommandBuilder ForeignKeyConstraint(Action<ForeignKeyConstraintBuilder> action)
    {
        var builder = new ForeignKeyConstraintBuilder();
        action(builder);
        var constraint = builder.Build();
        constraints.Add(constraint);

        return this;
    }

    public CreateTableCommandBuilder Index(Action<IndexConstraintBuilder> action)
    {

        var builder = IndexConstraintBuilder.Create();
        action(builder);
        var index = builder.Build();
        constraints.Add(index);

        return this;
    }

    public CreateTableCommand Build()
    {
        var command = new CreateTableCommand()
        {
            Name = name,
            Comment = comment,
            Columns = columns,
            Constraints = constraints
        };

        return command;
    }

    public static CreateTableCommandBuilder Create(string name)
    {
        var builder = new CreateTableCommandBuilder()
        {
            name = name
        };

        return builder;
    }
}
