using System.Text;

namespace NutriaNet.Data.Metas.Extensions;

public static class ColumnMySqlCommandTextExtensions
{
    public static string ToMySqlCommandText(this Column column)
    {
        var builder = new StringBuilder();

        string? textType = null;
        int? length = null;
        int? precision = null;
        int? scale = null;

        switch (column.Type)
        {
            case ColumnType.TinyInt:
                textType = "TINYINT";
                length = column.Length > 0 ? column.Length : 1;
                break;
            case ColumnType.SmallInt:
                textType = "SMALLINT";
                length = column.Length > 0 ? column.Length : 2;
                break;
            case ColumnType.MediumInt:
                textType = "MEDIUMInt";
                length = column.Length > 0 ? column.Length : 3;
                break;
            case ColumnType.Int:
                textType = "INT";
                length = column.Length > 0 ? column.Length : 4;
                break;
            case ColumnType.BigInt:
                textType = "BIGINT";
                length = column.Length > 0 ? column.Length : 8;
                break;
            case ColumnType.Float:
                textType = "FLOAT";
                break;
                
            case ColumnType.Double:
                break;
            default:
                throw new Exception("not supported type");
        }

        if (null == textType)
        {
            throw new Exception("not supported type");
        }
        builder.Append(textType);

        if (null !=  length)
        {
            builder.Append($"({length})");
        }

        return builder.ToString();
    }
}
