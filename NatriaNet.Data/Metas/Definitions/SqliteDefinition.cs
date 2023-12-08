using System.Text;

namespace NutriaNet.Data.Metas.Definitions;

public class SqliteDefinition : IDefinition
{
    public string CreateTable(Table table)
    {
        return @$"CREATE TABLE {table.Name} (
{string.Join(",\n", table.Columns.Select(col => $"    {col.Name} {GetColumnType(col.Type)}"))}
{ToConstraints(table.Constraints)}
)";
    }

    public string ToConstraints(IEnumerable<Constraint> constraints)
    {
        var builder = new StringBuilder();
        if (constraints.Any())
        {
            builder.Append(",");
        }
        
        var constraintsText = string.Join("", 
            constraints
                .Where(constraint => constraint is ForeignKeyConstraint)
                .Select(constraint => (constraint as ForeignKeyConstraint)!)
                .Select(ToForeignKeyConstraint)
        );

        builder.AppendLine(constraintsText);

        return builder.ToString();
    }

    public string ToForeignKeyConstraints(IEnumerable<ForeignKeyConstraint> constraints)
    {
        return string.Join("\n", constraints.Select(ToForeignKeyConstraint));
    }


    public string ToForeignKeyConstraint(ForeignKeyConstraint constraint)
    {
        var builder = new StringBuilder();
        var indent = 4;
        if (!string.IsNullOrEmpty(constraint.Name))
        {
            builder.Append($"    CONSTRAINT {constraint.Name}\n");
            indent = 8;
        }

        builder.Append($"{" ".PadLeft(indent)}FOREIGN KEY ({string.Join(", ", constraint.Columns.Select(fk => fk.Name))})\n");
        builder.Append($"{" ".PadLeft(indent)}REFERENCES {constraint.ReferenceTable.Name} ({string.Join(", ", constraint.References.Select(reference => reference.Name))})\n");

        return builder.ToString();

    }

    public string GetColumnType(ColumnType type)
    {
        return type switch
        {
            ColumnType.Int => "INTEGER",
            ColumnType.Text => "TEXT",
            ColumnType.Varchar => "VARCHAR",
            _ => throw new NotImplementedException(),
        };
    }
}
