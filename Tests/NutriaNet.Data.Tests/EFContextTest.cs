using Microsoft.EntityFrameworkCore;
using NutriaNet.Data.Code;
using NutriaNet.Data.Meta;

namespace NutriaNet.Data.Tests;

public class EFContextTest
{
    [Fact]
    public void ShouldEntityToSuccessForCreate()
    {

        var nutria = AppAssemblies.Assemblies.Where(a => a.GetName().Name.Contains("Nutria"));

        var permission = new Entity(
            "Permission", 
            new Property[]
            {
                new ("Id", "long"),
                new ("Key", "string"),
                new ("Name", "string")
            }
        );

        var resource = new Entity(
            "Resource",
            new Property[]
            {
                new ("Id", "long"),
                new ("Key", "string"),
                new ("Name", "string"),
                new ("Permissions", "ICollection<Permission>")
            }
        );

        var database = new Database("AccessControl", new Entity[] { permission, resource });

        var builder = new EntityFrameworkBuilder();
        var context = builder.Build(database);

        var ef = new EFContext();
        ef.AddAssemblyLoadContext("AccessControl", context);

        var options = new DbContextOptionsBuilder()
            .UseInMemoryDatabase("AccessControl")
            .Options;

        var dbContext = ef.GetDbContext("AccessControl", options);

        dynamic p = dbContext.CreateEntity("Permission");

        var db = (dbContext as DbContext)!;

        p.Key = "Read";
        p.Name = "∂¡»°";

        db.Add(p);
        db.SaveChanges();
    }
}