using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Tests.Metas.Fixtures;

public class SqliteFixture
{
    public Table CreatePeopleTable()
    {
        var table = TableBuilder.Create()
            .Name("People")
            .Column(col => col.Name("Id").Type(ColumnType.Int))
            .Build();

        return table;

    }
}
