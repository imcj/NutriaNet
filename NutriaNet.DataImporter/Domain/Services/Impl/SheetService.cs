using NutriaNet.DataImporter.Domain.Model;

namespace NutriaNet.DataImporter.Domain.Services.Impl;

public class SheetService : ISheetService
{
    public object GetEntity(SheetMapping mapping, Row row)
    {
        object entity = mapping.CreateEntity();

        for (int i = 0, size = row.Values.Count; i < size; i++)
        {
            var cell = row.Values[i];
            mapping.AssignValueToObject(cell, i, entity);
        }

        return entity;
    }
}
