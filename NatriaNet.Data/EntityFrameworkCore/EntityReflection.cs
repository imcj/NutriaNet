using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.EntityFrameworkCore;

public class EntityReflection
{
    protected IDictionary<string, Type> types = new Dictionary<string, Type>();

    public EntityReflection(Type[] types)
    {
        types.ToList().ForEach(type => { this.types[type.Name] = type; });
    }

    public object? CreateInstance(string type)
    {
        return Activator.CreateInstance(types[type]);
    }
}
