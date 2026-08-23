using System.Text.Json.Serialization;
using LemonSqueezySharp.Models.Common;

namespace LemonSqueezySharp.Models.Users;

/// <summary>
/// Strongly-typed alias for a LemonSqueezy user resource.
/// </summary>
public sealed class User : JsonApiResource<UserAttributes>
{
}

/// <summary>
/// User attribute payload — returned by <c>/v1/users/me</c>.
/// See https://docs.lemonsqueezy.com/api/users.
/// </summary>
public sealed class UserAttributes
{
    /// <summary>User full name (<c>name</c>).</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>User email address (<c>email</c>).</summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>Accent colour associated with the user (<c>color</c>).</summary>
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    /// <summary>URL of the user avatar (<c>avatar_url</c>).</summary>
    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    /// <summary>Whether the user has uploaded a custom avatar (<c>has_custom_avatar</c>).</summary>
    [JsonPropertyName("has_custom_avatar")]
    public bool? HasCustomAvatar { get; set; }

    /// <summary>Timestamp the user was created (<c>created_at</c>).</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Timestamp the user was last updated (<c>updated_at</c>).</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
