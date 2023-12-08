using NutriaNet.Data.Metas.Definitions;
using System.Text;

namespace NutriaNet.Data.Metas.Commands;

public class SqliteCommandText : ICommandText
{
    public string CreateTable(CreateTableCommand command)
    {
        var text = @$"CREATE TABLE {command.Name} (
    {CreateColumns(command.Columns, command.Constraints.Any())}
    {CreateConstraints(command.Constraints)}
);";
        var builder = new StringBuilder();
        builder.AppendLine(text);
        builder.Append(CreateIndexes(command));
        return builder.ToString().Replace("\r\n", "\n");
    }

    protected string CreateColumns(IEnumerable<Column> columns, bool hasConstraints)
    {
        var builder = new StringBuilder(string.Join(",\n    ", columns.Select(CreateColumn)));
        builder.Append(",");
        return builder.ToString();
    }

    protected string CreateColumn(Column column)
    {
        var ddl = new SqliteDefinition();

        var type = ddl.GetColumnType(column.Type);

        var text = $"{column.Name} {type}{CreateColumnTypeLength(column)}";
        return text;
    }

    protected string CreateColumnTypeLength(Column column) =>
        column.Type switch
        {
            ColumnType.Varchar => $"({column.Length})",
            _ => ""
        };

    protected string CreateConstraints(IEnumerable<Constraint> constraints)
    {
        return string.Join("    ,\n", constraints.Where(c => c is ForeignKeyConstraint).Select(CreateConstraint));
    }

    protected string CreateConstraint(Constraint constraint)
    {
        if (constraint is ForeignKeyConstraint fk)
        {
            return CreateForeignKeyConstraint(fk);
        }
        return "";
    }

    protected string CreateForeignKeyConstraint(ForeignKeyConstraint constraint)
    {
        var builder = new StringBuilder();
        var padLeft = 4;
        if (!string.IsNullOrEmpty(constraint.Name))
        {
            builder.AppendLine($"CONSTRAINT {constraint.Name}");
            padLeft = 8;
        }

        builder.Append(@$"{"".PadLeft(padLeft)}FOREIGN KEY ({string.Join(", ", constraint.Columns.Select(column => column.Name))})
{"".PadLeft(padLeft)}REFERENCES {constraint.ReferenceTable.Name}({string.Join(", ", constraint.References.Select(column => column.Name))})");

        return builder.ToString();
    }

    protected string CreateIndexes(CreateTableCommand command)
    {
        return $"{string.Join("\n", command.Constraints.Where(c => c is IndexConstraint).Select(c => (IndexConstraint)c).Select(CreateIndex))}";
    }

    protected string CreateIndex(IndexConstraint index)
    {
        return $"CREATE INDEX {index.Name} ON {index.TableName}({string.Join(", ", index.Columns)});";
    }
}
