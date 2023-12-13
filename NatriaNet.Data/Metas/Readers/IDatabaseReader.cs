namespace NutriaNet.Data.Metas.Readers;

public interface IDatabaseReader
{
    Task<Table> TableAsync(string tableName);
}
