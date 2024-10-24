using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Modrinth.Json;

/// <inheritdoc />
public class ColorConverter : JsonConverter<Color?>
{
    /// <inheritdoc />
    public override Color? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
            {
                var intValue = reader.GetInt32();
                return Color.FromArgb(intValue);
            }
            case JsonTokenType.Null:
                return null;
            default:
                throw new JsonException("Unexpected token type: " + reader.TokenType);
        }
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Color? value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}