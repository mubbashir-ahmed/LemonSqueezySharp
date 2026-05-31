using LemonSqueezy.NET.Models.Common;

namespace LemonSqueezy.NET.Models.Files;

public sealed class File : JsonApiResource<FileAttributes>
{
}

/// <summary>
/// File attribute payload. See https://docs.lemonsqueezy.com/api/files.
/// </summary>
public sealed class FileAttributes
{
}
