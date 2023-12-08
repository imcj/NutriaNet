using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Code;

public class AppAssemblies
{
    public static Assembly[] Assemblies { get; } = AppDomain.CurrentDomain.GetAssemblies();

    public Assembly Get(string name) =>
        Assemblies.Single(assembly => assembly.GetName().Name == name);
}
