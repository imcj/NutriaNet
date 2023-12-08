using System.Data.Common;

namespace NutriaNet.Data.Metas.Readers.MySql;

internal class MySql57Reader : IDatabaseReader
{
    protected DbConnection connection;

    protected MySql57ReaderAssembler assembler = new();

    public MySql57Reader(DbConnection connection)
    {
        this.connection = connection;
    }

    public Task TableAsync(string tableName)
    {
        throw new NotImplementedException();
    }
}
