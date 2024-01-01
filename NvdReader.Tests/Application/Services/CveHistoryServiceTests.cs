using Microsoft.Extensions.Logging;
using NSubstitute;
using NvdReader.Application.Services;
using NvdReader.Domain.Vulnerabilities.CveHistoryEntity;
using NvdReader.Domain.Vulnerabilities.Http;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Tests.Application.Services;

public class NvdCveHistorySyncServiceTests
{
    [Fact]
    public async Task UpdateTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCveHistorySyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        CveChange[] data = [new CveChange { Change = new Change { CveChangeId = "CveChangeId", CveId = "CveId" } }];
        var responseFirst = new CveHistoryResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, CveChanges = data };
        var responseSecond = new CveHistoryResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, CveChanges = [] };
        _ = nvdClient.GetAsync<CveHistoryResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst, _ => responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdCveHistorySyncService(logger, nvdClient);

        // Act
        var result = await service.UpdateSync(DateTime.Now, cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<CveHistoryResponse>(Arg.Any<string>(), cts.Token);
    }

    [Fact]
    public async Task FreshStartTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCveHistorySyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        CveChange[] data = [new CveChange { Change = new Change { CveChangeId = "CveChangeId", CveId = "CveId" } }];
        var responseFirst = new CveHistoryResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, CveChanges = data };
        var responseSecond = new CveHistoryResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, CveChanges = [] };
        _ = nvdClient.GetAsync<CveHistoryResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst, _ => responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdCveHistorySyncService(logger, nvdClient);

        // Act
        var result = await service.FreshStartAsync(cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<CveHistoryResponse>(Arg.Any<string>(), cts.Token);
    }
}
