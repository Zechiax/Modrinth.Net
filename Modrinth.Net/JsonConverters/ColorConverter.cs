using System.Drawing;
using Newtonsoft.Json;

namespace Modrinth.JsonConverters;

/// <inheritdoc />
public class ColorConverter : JsonConverter<Color?>
{
    /// <inheritdoc />
    public override Color? ReadJson(JsonReader reader, Type objectType, Color? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.Integer:
            {
                var intValue = (long) reader.Value!;
                return Color.FromArgb((int) intValue);
            }
            case JsonToken.Null:
                return null;
            default:
                throw new JsonSerializationException("Unexpected token type: " + reader.TokenType);
        }
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, Color? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}