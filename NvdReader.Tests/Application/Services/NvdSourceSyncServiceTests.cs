using Microsoft.Extensions.Logging;
using NSubstitute;
using NvdReader.Application.Services;
using NvdReader.Domain.DataSources.Http;
using NvdReader.Domain.DataSources.SourceEntity;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Tests.Application.Services;

public class NvdSourceSyncServiceTests
{
    [Fact]
    public async Task UpdateTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdSourceSyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        Source[] data = [new Source { SourceIdentifiers = ["SourceIdentifiers1"] }];
        var responseFirst = new SourceResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, Sources = data };
        var responseSecond = new SourceResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, Sources = [] };
        _ = nvdClient.GetAsync<SourceResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst, _ => responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdSourceSyncService(logger, nvdClient);

        // Act
        var result = await service.UpdateSync(DateTime.Now, cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<SourceResponse>(Arg.Any<string>(), cts.Token);
    }

    [Fact]
    public async Task FreshStartTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdSourceSyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        Source[] data = [new Source { SourceIdentifiers = ["SourceIdentifiers1"] }];
        var responseFirst = new SourceResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, Sources = data };
        var responseSecond = new SourceResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, Sources = [] };
        _ = nvdClient.GetAsync<SourceResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst, _ => responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdSourceSyncService(logger, nvdClient);

        // Act
        var result = await service.FreshStartAsync(cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<SourceResponse>(Arg.Any<string>(), cts.Token);
    }
}
