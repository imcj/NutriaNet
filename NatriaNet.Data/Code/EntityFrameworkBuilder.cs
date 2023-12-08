using NutriaNet.Data.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Code;

public class EntityFrameworkBuilder
{
    public AssemblyLoadContext Build(Database database)
    {
        var template = new EntityFrameworkTemplate(database);
        var code = template.GetCode();

        var compiler = new Compiler(database.Name);
        var stream = compiler.Compile(code);

        return compiler.MakeAssemblyLoadContext(stream);
    }
}
