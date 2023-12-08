using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Tests.Metas.Builder;

public class ColumnBuilderTest
{
    [Fact]
    public void TestColumnBuilder()
    {
        var builder = new ColumnBuilder();
        var column = builder.Name("Name")
            .Type(ColumnType.Text)
            .Build();

        Assert.Equal("Name", column.Name);
        Assert.Equal(ColumnType.Text, column.Type);
    }
}
