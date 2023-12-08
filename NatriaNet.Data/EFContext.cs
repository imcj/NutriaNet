using NutriaNet.Data.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.Loader;

namespace NutriaNet.Data;

public class EFContext
{
    protected IDictionary<string, object> dbContexts = new Dictionary<string, object>();

    protected IDictionary<string, AssemblyLoadContext> assemblyLoadContexts = new Dictionary<string, AssemblyLoadContext>();

    protected IDictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

    public void AddAssemblyLoadContext(string name, AssemblyLoadContext assemblyLoadContext)
    {
        assemblyLoadContexts[name] = assemblyLoadContext;
        assemblies[name] = assemblyLoadContext.Assemblies.Single(assembly => assembly.GetName().Name == name);
    }

    public void Remove(string name)
    {
        if (!assemblyLoadContexts.ContainsKey(name))
        {
            return;
        }

        var context = assemblyLoadContexts[name];
        context.Unload();

        assemblyLoadContexts.Remove(name);
    }

    public IDbContext GetDbContext(string name, params object[] parameters)
    {
        var type = assemblies[name].GetType($"NutriaNet.EntityFramework.{name}.DbContext");
        var dbContext = Activator.CreateInstance(type!, parameters)!;

        return (dbContext as IDbContext)!;
    }
}
