namespace NutriaNet.Data.Metas.Readers;

public interface IDatabaseReader
{
    Task TableAsync(string tableName);
}
