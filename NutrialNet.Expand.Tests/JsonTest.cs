using NutrialNet.Expand.Domain.Models;
using NutrialNet.Expand.Infrastructure.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NutrialNet.Expand.Tests;

public class JsonTest
{
    [Fact]
    public async Task TestPolymorphic()
    {
        var json = @"{
    ""discriminator"": ""CreateTableStatement"",
    ""key"": ""User""
}";

        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(), new DiscriminatorJsonConverterFactory() }
        };

        var mem = new MemoryStream(Encoding.UTF8.GetBytes(json));

        //JsonSerializer.DeserializeAsync(mem);

        var createTable = await JsonSerializer.DeserializeAsync<IDefinitionStatement>(mem, options);
    }
}
