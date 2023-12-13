using NutriaNet.Data.Metas.Extensions;
using System.Text;

namespace NutriaNet.Data.Metas.Commands;

public class MySqlCommandText : ICommandText
{
    public string CreateTable(CreateTableCommand command)
    {
        var builder = new StringBuilder($"CREATE TABLE {command.Name} (\n");
        builder.Append(CreateColumns(command.Columns));
        builder.Append(");");

        return builder.ToString();
    }
    
    protected string CreateColumns(ICollection<Column> columns)
    {
        return string.Join(",\n", columns.Select(CreateColumn));
    }

    protected string CreateColumn(Column column)
    {
        var builder = new StringBuilder();

        builder.Append(column.Name);
        builder.Append(' ');
        builder.Append(column.ToMySqlCommandText());

        return builder.ToString();
    }

    protected IEnumerable<string> CreateConstraints(CreateTableCommand command)
    {
        return command.Constraints.Select(CreateConstraint);
    }

    protected string CreateConstraint(Constraints.Constraint constraint)
    {
        var builder = new StringBuilder();


        return builder.ToString();
    }

    protected string CreatePrimaryKeyConstraint()
    {
        throw new NotImplementedException();
    }

    protected string CreateForeignKeyConstraint(ForeignKeyConstraint foreignKey)
    {
        throw new NotImplementedException();
    }
}
