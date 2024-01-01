using Microsoft.Extensions.Logging;
using NSubstitute;
using NvdReader.Application.Services;
using NvdReader.Domain.Products.CpeEntity;
using NvdReader.Domain.Products.Http;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Tests.Application.Services;

public class NvdCpeSyncServiceTests
{
    [Fact]
    public async Task UpdateTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCpeSyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        Product[] data = [new Product { Cpe = new Cpe { CpeNameId = "CpeNameId1" } }];
        var responseFirst = new CpeResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, Products = data };
        var responseSecond = new CpeResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, Products = [] };
        _ = nvdClient.GetAsync<CpeResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst, _ => responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdCpeSyncService(logger, nvdClient);

        // Act
        var result = await service.UpdateSync(DateTime.Now, cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<CpeResponse>(Arg.Any<string>(), cts.Token);
    }

    [Fact]
    public async Task FreshStartTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCpeSyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        Product[] data = [new Product { Cpe = new Cpe { CpeNameId = "CpeNameId1" } }];
        var responseFirst = new CpeResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, Products = data };
        var responseSecond = new CpeResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, Products = [] };
        _ = nvdClient.GetAsync<CpeResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst, _ => responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdCpeSyncService(logger, nvdClient);

        // Act
        var result = await service.FreshStartAsync(cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<CpeResponse>(Arg.Any<string>(), cts.Token);
    }
}
