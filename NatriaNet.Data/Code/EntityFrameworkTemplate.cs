using NutriaNet.Data.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Code;

public class EntityFrameworkTemplate
{
    protected Database database;

    public EntityFrameworkTemplate(Database database)
    {
        this.database = database;
    }

    public string GetCode()
    {
        var builder = new StringBuilder();
        builder.AppendLine(GetUsing());
        builder.AppendLine(GetEntities());
        builder.AppendLine(GetDbContext());
        return builder.ToString();
        
    }

    public string GetUsing()
    {
        return @$"
using System;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NutriaNet.Data.EntityFrameworkCore;

namespace NutriaNet.EntityFramework.{database.Name};
";
    }

    public string GetDbContext()
    {
        return @$"
public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
{{

    protected EntityReflection reflection = new (
        new Type[] {{
{string.Join(", ", database.Entities.Select(entity => $"            typeof({entity.Name})"))}
        }}
    );

    public DbContext(DbContextOptions options) : base(options) {{}}
{
string.Join(
    "\n",
    string.Join(
        "\n\n",
        database
            .Entities
            .Select(entity => $"    public DbSet<{entity.Name}> {entity.Name} {{ get; set; }}"
        )
    )
)}

    public object CreateEntity(string name)
    {{
        return reflection.CreateInstance(name);
    }}
}}
";
    }


    public string GetEntities()
    {
        var entities = database.Entities.Select(x => GetEntity(x));

        return string.Join("\n", entities);
    }

    public string GetEntity(Entity entity)
    {
        var code = @$"

public class {entity.Name}
{{

{GetEntityProperties(entity)}

}}
";

        return code;
    }

    public string GetEntityProperties(Entity entity)
    {
        return string.Join(
            "\n\n",
            entity.Properties.Select(property => GetEntityProperty(property))
        );
    }

    public string GetEntityProperty(Property property)
    {
        var code = $"    public {property.Type} {property.Name} {{ get; set; }}";
        return code;
    }
}
