namespace NutriaNet.Data.Metas.Builders;

public class ForeignKeyConstraintBuilder : ConstraintBuilder
{
    protected string name;

    protected List<string> foreignKeys = new();

    protected string referenceTable = "";

    protected List<string> references = new();

    public string ReferenceTable { get => referenceTable; }

    public IEnumerable<string> ForeignKeys { get { return foreignKeys; } }

    public IEnumerable<string> References { get {  return references; } }

    public ForeignKeyConstraintBuilder Name(string name)
    {
        this.name = name;
        return this;
    }

    public string GetName() => name;

    public ForeignKeyConstraintBuilder Column(string name)
    {
        foreignKeys.Add(name);
        return this;
    }

    public ForeignKeyConstraintBuilder Column(IEnumerable<string> foreignKeys)
    {
        this.foreignKeys.AddRange(foreignKeys);
        return this;
    }

    public ForeignKeyConstraintBuilder Reference(string table, string name)
    {
        referenceTable = table;
        references.Add(name);
        return this;
    }

    public ForeignKeyConstraintBuilder Reference(string table, string[] references)
    {
        referenceTable = table;
        this.references.AddRange(references);
        return this;
    }

    public ForeignKeyConstraint Build()
    {
        var columns = foreignKeys.Select(fk => new Column(fk, ColumnType.Int, -1));
        var foreignKey = new ForeignKeyConstraint(
            name,
            columns,
            new Table(referenceTable),
            references.Select(r => new Column(r, ColumnType.Int, -1))
        );

        return foreignKey;
    }
}
