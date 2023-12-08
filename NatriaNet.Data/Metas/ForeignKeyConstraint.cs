namespace NutriaNet.Data.Metas;

public class ForeignKeyConstraint : Constraint
{
    public string Name { get; }

    public IEnumerable<Column> Columns { get; }

    public Table ReferenceTable { get; set; }

    public IEnumerable<Column> References { get; set; }

    public ForeignKeyConstraint(string name, IEnumerable<Column> foreignKeys, Table referenceTable, IEnumerable<Column> references)
    {
        Name = name;
        Columns = foreignKeys;
        ReferenceTable = referenceTable;
        References = references;
    }

    public ForeignKeyConstraint(IEnumerable<Column> foreignKeys, Table referenceTable, IEnumerable<Column> references)
    {
        Columns = foreignKeys;
        ReferenceTable = referenceTable;
        References = references;
        Name = "";
    }
}
