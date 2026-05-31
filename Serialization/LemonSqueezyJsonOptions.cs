using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonSqueezy.NET.Serialization;

/// <summary>
/// Shared <see cref="JsonSerializerOptions"/> tuned for the LemonSqueezy JSON:API.
/// </summary>
public static class LemonSqueezyJsonOptions
{
    /// <summary>
    /// Singleton <see cref="JsonSerializerOptions"/> reused for every request and response.
    /// </summary>
    public static JsonSerializerOptions Default { get; } = Create();

    private static JsonSerializerOptions Create()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
        };

        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
        options.Converters.Add(new UnixDateTimeConverter());

        return options;
    }
}
