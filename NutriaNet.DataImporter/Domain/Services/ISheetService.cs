using NutriaNet.DataImporter.Domain.Model;

namespace NutriaNet.DataImporter.Domain.Services;

public interface ISheetService
{
    object GetEntity(SheetMapping mapping, Row row);
}
