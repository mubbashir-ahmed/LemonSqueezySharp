using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Serialization;

/// <summary>
/// Reads/writes <see cref="DateTimeOffset"/> values that may arrive as ISO-8601 strings
/// or Unix timestamps (seconds since epoch).
/// </summary>
public sealed class UnixDateTimeConverter : JsonConverter<DateTimeOffset?>
{
    /// <inheritdoc />
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;

            case JsonTokenType.Number when reader.TryGetInt64(out var unixSeconds):
                return DateTimeOffset.FromUnixTimeSeconds(unixSeconds);

            case JsonTokenType.String:
                var raw = reader.GetString();
                if (string.IsNullOrWhiteSpace(raw))
                {
                    return null;
                }

                if (DateTimeOffset.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var parsed))
                {
                    return parsed;
                }

                if (long.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var unixFromString))
                {
                    return DateTimeOffset.FromUnixTimeSeconds(unixFromString);
                }

                throw new JsonException($"Unable to parse '{raw}' as a date/time value.");

            default:
                throw new JsonException($"Unexpected token {reader.TokenType} when reading a date/time value.");
        }
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(value.Value.ToString("O", CultureInfo.InvariantCulture));
    }
}
