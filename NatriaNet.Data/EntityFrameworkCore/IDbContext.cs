namespace NutriaNet.Data.EntityFrameworkCore;

public interface IDbContext
{
    object CreateEntity(string name);
}
