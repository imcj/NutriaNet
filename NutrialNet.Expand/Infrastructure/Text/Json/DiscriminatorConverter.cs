using System.Text.Json;
using System.Text.Json.Serialization;

namespace NutrialNet.Expand.Infrastructure.Text.Json;

public class DiscriminatorConverter<T> : JsonConverter<T>
{
    protected Types types;

    public DiscriminatorConverter(Types types)
    {
        this.types = types;
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var discriminator = document
            .RootElement
            .GetProperty("discriminator")
            .GetString()
        
        ?? throw new Exception("The discriminator is null");

        var type = types.Get(discriminator) ?? throw new Exception("the target type is null");

        var serialized = (T?)JsonSerializer.Deserialize(
            document,
            type,
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            }
        ) ?? throw new Exception("Cast to target type exception");

        return serialized;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
    }
}
