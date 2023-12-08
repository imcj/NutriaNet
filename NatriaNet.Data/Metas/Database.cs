using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Metas;

public class Database
{
    public string Name { get; set; }

    public HashSet<Table> Tables { get; } = new();

    public Database(string name)
    {
        Name = name;
    }

    public void AddTable(Table table)
    {
        Tables.Add(table);
    }

    public Table GetTable(string name) => Tables.Single(t => t.Name == name);
}
