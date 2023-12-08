using System.Collections.Immutable;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NutrialNet.Expand.Infrastructure.Text.Json;

public class DiscriminatorJsonConverterFactory : JsonConverterFactory
{
    protected static Types types;

    static DiscriminatorJsonConverterFactory()
    {
        types = new Types();
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return true;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var targetType = typeof(DiscriminatorConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter?)Activator.CreateInstance(targetType, types);
    }
}
