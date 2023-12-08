using NutriaNet.DataImporter.Domain.Model;

namespace NutriaNet.DataImporter.Applications.Impl;

public class SpreadsheetService
{

    protected readonly IDataRepository _repository;

    public async Task Import(Sheet sheet)
    {
        await foreach (var row in sheet.GetRows<object>())
        {
            await _repository.AddAsync(row);
        }
    }
}
