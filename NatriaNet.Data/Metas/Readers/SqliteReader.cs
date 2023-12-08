using Microsoft.Data.Sqlite;

namespace NutriaNet.Data.Metas.Readers;

public class SqliteReader
{

    protected SqliteConnection connection;

    public SqliteReader(string  connectionString)
    {
        connection = new SqliteConnection(connectionString);
    }

    public SqliteReader(SqliteConnection connection)
    {
        this.connection = connection;
    }

    public async Task<Table> TableAsync(string name)
    {
        //var schema = await connection.GetSchemaAsync("Columns", new[] { null, null, name });

        using var cmd = connection.CreateCommand();
        cmd.CommandText = $"PRAGMA table_info({name});";
        using var reader = await cmd.ExecuteReaderAsync();

        var columns = new List<Column>();

        while (reader.Read())
        {
            var column = new Column(reader["name"]?.ToString() ?? "", GetColumnType(reader["type"]?.ToString() ?? ""), 0);
            columns.Add(column);
        }
        
        return new Table(name, columns);
    }

    protected ColumnType GetColumnType(string type)
    {
        return type.ToLower() switch
        {
            "integer" => ColumnType.Big,
            "text" => ColumnType.Text,
            _ => throw new NotImplementedException()
        };
    }
}
