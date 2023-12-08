using NutriaNet.DataImporter.Interfaces.DTO;

namespace NutriaNet.DataImporter.Interfaces.Facade;

public interface ISpreadsheetServiceFacade
{
    Task ImportSingleSheet<T>(ImportConfigurationDTO dto);
}
