namespace NutriaNet.Data.Metas.Builders;

public class TableBuilder
{
    protected Database database = new("Unknow");

    protected Table table = new("Unknow");

    protected List<ConstraintBuilder> constraints = new();


    public TableBuilder()
    {
    }

    public static TableBuilder Create()
    {
        return new TableBuilder();
    }

    public TableBuilder Database(Database Database)
    {
        database = Database;
        return this;
    }

    public TableBuilder Name(string name)
    {
        table.Name = name;
        return this;
    }

    public TableBuilder Column(Action<ColumnBuilder> action)
    {
        var columnBuilder = new ColumnBuilder();
        action(columnBuilder);

        table.AddColumn(columnBuilder.Build());
        return this;
    }

    public TableBuilder ForeignKey(Action<ForeignKeyConstraintBuilder> action)
    {
        var builder = new ForeignKeyConstraintBuilder();
        action(builder);

        constraints.Add(builder);
        return this;
    }

    public TableBuilder NewTable()
    {
        var builder = new TableBuilder();
        return builder;
    }

    public Table Build()
    {
        database.AddTable(table);
        
        BuildConstraint(table);
        return table;
    }

    protected void BuildConstraint(Table table)
    {
        var foreignKeys = constraints.Where(constraint => constraint is ForeignKeyConstraintBuilder)
            .Where(fkb => fkb is ForeignKeyConstraintBuilder)
            .Select(fkb => fkb as ForeignKeyConstraintBuilder)
            .Select(fkb => BuildForeignKey(fkb!))
            .ToList();

        table.AddConstraints(foreignKeys);
    }

    protected ForeignKeyConstraint BuildForeignKey(ForeignKeyConstraintBuilder foreignKey)
    {
        return new ForeignKeyConstraint(
            foreignKey.GetName(),
            foreignKey.ForeignKeys.Select(GetColumn).ToList(),
            database.GetTable(foreignKey.ReferenceTable),
            GetForeignKeyReferences(foreignKey.ReferenceTable, foreignKey.References)
        );
    }

    protected Column GetColumn(string name)
    {
        return table.GetColumn(name);
    }

    protected List<Column> GetForeignKeyReferences(string tableName, IEnumerable<string> columns)
    {
        var table = database.GetTable(tableName);
        return columns.Select(column => table.GetColumn(column)).ToList();
    }
}
