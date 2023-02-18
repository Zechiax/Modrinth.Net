namespace Modrinth.JsonConverters;

using System;
using Newtonsoft.Json;

/// <inheritdoc />
public class ColorConverter : JsonConverter<Models.Color?>
{
    /// <inheritdoc />
    public override Models.Color? ReadJson(JsonReader reader, Type objectType, Models.Color? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.Integer:
            {
                var intValue = (long)reader.Value!;
                return new Models.Color(intValue);
            }
            case JsonToken.Null:
                return null;
            default:
                throw new JsonSerializationException("Unexpected token type: " + reader.TokenType);
        }
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, Models.Color? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
