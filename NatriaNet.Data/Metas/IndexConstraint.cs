﻿namespace NutriaNet.Data.Metas;

public class IndexConstraint : Constraint
{
    public string? Name { get; }

    public IEnumerable<string> Columns { get; set; } = new List<string>();

    public string TableName { get; set; }

    public bool IsUnique { get; set; } = false;

    public IndexConstraint(string? name, IEnumerable<string> columns, string tableName, bool isUnique)
    {
        Name = name;
        Columns = columns;
        TableName = tableName;
        IsUnique = isUnique;
    }
}
