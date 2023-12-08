using NutriaNet.DataImporter.Domain.Model;

namespace NutriaNet.DataImporter.Applications;

public interface ISpreadsheetService
{
    Task Import<T>(Stream stream, ColumnMapping mapping);
}
