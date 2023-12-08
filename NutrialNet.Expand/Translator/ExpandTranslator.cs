using NutrialNet.Expand.Domain.Models;
using NutrialNet.Expand.Domain.Models.Columns;
using NutriaNet.Data.Metas;
using NutriaNet.Data.Metas.Builders;

namespace NutrialNet.Expand.Translator;

internal class ExpandTranslator
{
    public Table ToTable(ExpandTable expand)
    {
        var table = TableBuilder
            .Create()
            .Name(expand.Key);

        var columns = expand.Columns.Where(IsNotChildLinked).ToList();
        columns.ForEach(column => table.Column(builder => ToColumn(column, builder)));

        return table.Build();
    }

    public Column CreateColumn(ExpandColumn column)
    {
        throw new NotImplementedException();
    }

    protected bool IsNotChildLinked(ExpandColumn column)
    {
        if (column is LinkedExpandColumn linked)
        {
            return linked.Bidirectionality != null && !linked.IsAllowLinkingToMultipleRecords;
        }
        return true;
    }

    public void ToColumn(ExpandColumn expand, ColumnBuilder builder)
    {
        switch (expand)
        {
            case LinkedExpandColumn:
                FromLinkedExpandColumn((LinkedExpandColumn)expand, builder);
                break;
            case SingleLineTextExpandColumn:
            case LongTextExpandColumn:
                ToText(expand, builder);
                break;

            default:
                throw new NotImplementedException();

        }
    }

    public void FromLinkedExpandColumn(LinkedExpandColumn expand, ColumnBuilder builder)
    {
        if (expand.Bidirectionality == null)
        {
            return;
        }

        builder.Name($"{expand.Key}Id").Type(ColumnType.Big);
    }

    public void ToText(ExpandColumn expand, ColumnBuilder builder)
    {
        if (expand is SingleLineTextExpandColumn singleLine)
        {
            builder
                .Name(singleLine.Key)
                .Type(ColumnType.Varchar)
                .Length(255)
                .Build();
        }
        else if (expand is LongTextExpandColumn longText)
        {
            builder
                .Name(longText.Key)
                .Type(ColumnType.Text)
                .Build();
        }
    }
}
