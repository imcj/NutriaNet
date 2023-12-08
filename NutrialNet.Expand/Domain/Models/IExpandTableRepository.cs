namespace NutrialNet.Expand.Domain.Models;

internal interface IExpandTableRepository
{
    Task<ExpandTable> GetByKey(string key);
}
