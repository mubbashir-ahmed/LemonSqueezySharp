using System.Text.Json.Serialization;
using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.Users;

public sealed class User : JsonApiResource<UserAttributes>
{
}

/// <summary>
/// User attribute payload — returned by <c>/v1/users/me</c>.
/// See https://docs.lemonsqueezy.com/api/users.
/// </summary>
public sealed class UserAttributes
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("has_custom_avatar")]
    public bool? HasCustomAvatar { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
