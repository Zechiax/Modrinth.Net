using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Modrinth.Models.Enums.Project;
using Modrinth.Models.Enums.Version;

namespace Modrinth.Json;

/// <inheritdoc />
public class JsonStringEnumConverterEx<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    private static readonly Dictionary<TEnum, string> _enumToString;
    private static readonly Dictionary<string, TEnum> _stringToEnum;

    static JsonStringEnumConverterEx()
    {
        _enumToString = new Dictionary<TEnum, string>();
        _stringToEnum = new Dictionary<string, TEnum>();

        var enumMappings = new Dictionary<TEnum, string>
        {
            { (TEnum)(object)ProjectStatus.Approved, "approved" },
            { (TEnum)(object)ProjectStatus.Draft, "draft" },
            { (TEnum)(object)ProjectStatus.Rejected, "rejected" },
            { (TEnum)(object)ProjectStatus.Unlisted, "unlisted" },

            { (TEnum)(object)ProjectType.Mod, "mod" },
            { (TEnum)(object)ProjectType.Project, "resourcepack" },
            

            { (TEnum)(object)VersionStatus.Listed, "listed" },
            { (TEnum)(object)VersionStatus.Archived, "archived" },

            // Add mappings for other enums...
        };

        foreach (var pair in enumMappings)
        {
            _enumToString[pair.Key] = pair.Value;
            _stringToEnum[pair.Value] = pair.Key;
        }
    }

    /// <inheritdoc />
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();
        if (stringValue != null && _stringToEnum.TryGetValue(stringValue, out var enumValue))
        {
            return enumValue;
        }

        throw new JsonException($"Unknown value '{stringValue}' for enum {typeof(TEnum).Name}");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        if (_enumToString.TryGetValue(value, out var stringValue))
        {
            writer.WriteStringValue(stringValue);
        }
        else
        {
            throw new JsonException($"Unknown enum value {value} for {typeof(TEnum).Name}");
        }
    }
}
