using System.Net;
using LemonSqueezySharp.Clients;
using LemonSqueezySharp.Models.Files;
using LemonSqueezySharp.Tests.TestHelpers;
using File = LemonSqueezySharp.Models.Files.File;

namespace LemonSqueezySharp.Tests.Clients;

public class FilesClientTests
{
    private const string SingleFilePayload = """
        {
          "data": {
            "id": "300",
            "type": "files",
            "attributes": {
              "variant_id": 10,
              "identifier": "0b2b50d2-1234-4567-89ab-cdef01234567",
              "name": "starter-pack.zip",
              "extension": "zip",
              "download_url": "https://files.lemonsqueezy.com/secure/abc.zip",
              "size": 10485760,
              "size_formatted": "10 MB",
              "version": "1.0.0",
              "sort": 1,
              "status": "published",
              "test_mode": false
            }
          }
        }
        """;

    private const string FileListPayload = """
        {
          "data": [
            { "id": "1", "type": "files", "attributes": { "name": "a.zip", "size": 100 } },
            { "id": "2", "type": "files", "attributes": { "name": "b.pdf", "size": 200 } }
          ],
          "links": { "next": "https://api.lemonsqueezy.com/v1/files?page[number]=2" },
          "meta": { "page": { "number": 1, "size": 2, "total": 3 } }
        }
        """;

    private static (FilesClient Client, StubHttpMessageHandler Handler) CreateSut()
    {
        var handler = new StubHttpMessageHandler();
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.lemonsqueezy.com/v1/"),
        };
        return (new FilesClient(httpClient), handler);
    }

    [Fact]
    public async Task GetAsync_returns_file_by_id()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, SingleFilePayload);

        var result = await client.GetAsync("300");

        result.Data!.Id.Should().Be("300");
        result.Data.Attributes!.Name.Should().Be("starter-pack.zip");
        result.Data.Attributes.Extension.Should().Be("zip");
        result.Data.Attributes.Size.Should().Be(10_485_760);
        result.Data.Attributes.SizeFormatted.Should().Be("10 MB");
        result.Data.Attributes.DownloadUrl.Should().StartWith("https://files.lemonsqueezy.com/");
        handler.Requests[0].RequestUri!.AbsolutePath.Should().EndWith("/v1/files/300");
    }

    [Fact]
    public async Task GetAsync_with_blank_id_throws_ArgumentException()
    {
        var (client, _) = CreateSut();

        var act = () => client.GetAsync("");

        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    [Fact]
    public async Task ListAsync_serialises_variant_filter()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, FileListPayload);

        _ = await client.ListAsync(new FileListFilter
        {
            VariantId = "10",
            PageSize = 10,
        });

        var query = handler.Requests[0].RequestUri!.Query;
        query.Should().Contain("filter%5Bvariant_id%5D=10");
        query.Should().Contain("page%5Bsize%5D=10");
    }

    [Fact]
    public async Task ListAsync_without_filter_targets_bare_files_endpoint()
    {
        var (client, handler) = CreateSut();
        handler.EnqueueJson(HttpStatusCode.OK, FileListPayload);

        var result = await client.ListAsync();

        result.Items.Should().HaveCount(2);
        handler.Requests[0].RequestUri!.PathAndQuery.Should().Be("/v1/files");
    }
}
