using NutriaNet.Data.Metas.Builders;
using System.Data;
using System.Data.Common;

namespace NutriaNet.Data.Metas.Readers.MySql;

public class MySql57Reader : IDatabaseReader
{
    protected DbConnection connection;

    protected MySql57ReaderAssembler assembler = new();

    public MySql57Reader(DbConnection connection)
    {
        this.connection = connection;
    }

    public async Task<Table> TableAsync(string tableName)
    {
        var sql = @"SELECT
*
FROM
`information_schema`.`Columns`
WHERE TABLE_NAME = @TableName
";

        using var cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        
        var tableNameParameter = cmd.CreateParameter();
        tableNameParameter.ParameterName = "TableName";
        tableNameParameter.Value = tableName;
        tableNameParameter.DbType = DbType.String;
        cmd.Parameters.Add(tableNameParameter);

        using var reader = await cmd.ExecuteReaderAsync();

        var columns = new List<Column>();

        while (reader.Read())
        {
            string name = (string)reader["COLUMN_NAME"];

            var builder = new ColumnBuilder()
                .Name(name)
                .Type(ColumnType.Int);

            columns.Add(builder.Build());
        }

        return new Table(tableName, columns);
    }
}
