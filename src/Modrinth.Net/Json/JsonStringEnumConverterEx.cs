using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Modrinth.Json;

/// <inheritdoc />
public class JsonStringEnumConverterEx<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.PublicProperties)] TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    // Credit to:
    // https://github.com/dotnet/runtime/issues/31081#issuecomment-848697673

    private readonly Dictionary<TEnum, string> _enumToString = new();
    private readonly Dictionary<string, TEnum> _stringToEnum = new();

    /// <inheritdoc />
    public JsonStringEnumConverterEx()
    {
        var type = typeof(TEnum);
        var values = Enum.GetValues<TEnum>();

        foreach (var value in values)
        {
            var enumMember = type.GetMember(value.ToString()).FirstOrDefault();
            if (enumMember != null)
            {
                var attr = enumMember.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                    .Cast<EnumMemberAttribute>()
                    .FirstOrDefault();

                _stringToEnum.Add(value.ToString(), value);

                if (attr?.Value != null)
                {
                    _enumToString.Add(value, attr.Value);
                    _stringToEnum.Add(attr.Value, value);
                }
                else
                {
                    _enumToString.Add(value, value.ToString());
                }
            }
            else
            {
                _stringToEnum.Add(value.ToString(), value);
                _enumToString.Add(value, value.ToString());
            }
        }
    }

    /// <inheritdoc />
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();

        if (_stringToEnum.TryGetValue(stringValue!, out var enumValue)) return enumValue;

        return default;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(_enumToString[value]);
    }
}