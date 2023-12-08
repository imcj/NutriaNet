namespace NutriaNet.DataImporter.Domain.Model;

public interface IDataRepository
{

    Task<object> AddAsync(object data);
}
