using Microsoft.Extensions.Logging;
using NSubstitute;
using NvdReader.Application.Services;
using NvdReader.Domain.Products.CpeMatchEntity;
using NvdReader.Domain.Products.Http;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Tests.Application.Services;

public class NvdCpeMatchSyncServiceTests
{
    [Fact]
    public async Task UpdateTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCpeMatchSyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        MatchStrings[] data = [new MatchStrings { MatchString = new MatchString { MatchCriteriaId = "MatchCriteriaId1" } }];
        var responseFirst = new CpeMatchResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, MatchStrings = data };
        var responseSecond = new CpeMatchResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, MatchStrings = [] };
        _ = nvdClient.GetAsync<CpeMatchResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst,_ =>  responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdCpeMatchSyncService(logger, nvdClient);

        // Act
        var result = await service.UpdateSync(DateTime.Now, cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<CpeMatchResponse>(Arg.Any<string>(), cts.Token);
    }

    [Fact]
    public async Task FreshStartTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCpeMatchSyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        MatchStrings[] data = [new MatchStrings { MatchString = new MatchString { MatchCriteriaId = "MatchCriteriaId1" } }];
        var responseFirst = new CpeMatchResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, MatchStrings = data };
        var responseSecond = new CpeMatchResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, MatchStrings = [] };
        _ = nvdClient.GetAsync<CpeMatchResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst,_ =>  responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdCpeMatchSyncService(logger, nvdClient);

        // Act
        var result = await service.FreshStartAsync(cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<CpeMatchResponse>(Arg.Any<string>(), cts.Token);
    }
}
