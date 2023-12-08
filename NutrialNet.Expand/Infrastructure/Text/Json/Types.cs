using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NutrialNet.Expand.Infrastructure.Text.Json;

public class Types
{
    protected IImmutableDictionary<string, Type> typesFullKey;

    protected IImmutableDictionary<string, ImmutableList<Type>> typesSimpleKey;

    public Types(IEnumerable<Assembly>? assemblies = null)
    {
        var theAssemblies = assemblies?.ToList() ?? new();
        var thisAssembly = GetType().Assembly;

        if (!theAssemblies.Any(a => a == thisAssembly))
        {
            theAssemblies.Add(thisAssembly);
        }

        var allTypes = theAssemblies.SelectMany(a => a.GetTypes());

        typesFullKey = allTypes.ToImmutableDictionary(t => t.FullName!, t => t);
        typesSimpleKey = allTypes.GroupBy(g => g.Name!).ToImmutableDictionary(g => g.Key, g => g.ToImmutableList());
    }

    public Type? Get(string name)
    {
        return typesSimpleKey[name].FirstOrDefault();
    }
}
