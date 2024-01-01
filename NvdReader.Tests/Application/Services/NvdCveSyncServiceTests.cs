using Microsoft.Extensions.Logging;
using NSubstitute;
using NvdReader.Application.Services;
using NvdReader.Domain.Vulnerabilities.CveEntity;
using NvdReader.Domain.Vulnerabilities.Http;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Tests.Application.Services;

public class NvdCveSyncServiceTests
{
    [Fact]
    public async Task UpdateTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCveSyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        Vulnerability[] data = [new Vulnerability { Cve = new Cve { Id = "Cve1" } }];
        var responseFirst = new CveResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, Vulnerabilities = data };
        var responseSecond = new CveResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, Vulnerabilities = [] };
        _ = nvdClient.GetAsync<CveResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst, _ => responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdCveSyncService(logger, nvdClient);

        // Act
        var result = await service.UpdateSync(DateTime.Now, cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<CveResponse>(Arg.Any<string>(), cts.Token);
    }

    [Fact]
    public async Task FreshStartTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCveSyncService>>();
        var nvdClient = Substitute.For<INvdApiClient>();
        Vulnerability[] data = [new Vulnerability { Cve = new Cve { Id = "Cve1" } }];
        var responseFirst = new CveResponse { Format = "", ResultsPerPage = 1, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 1, Vulnerabilities = data };
        var responseSecond = new CveResponse { Format = "", ResultsPerPage = 0, StartIndex = 1, Timestamp = DateTime.Now, Version = "", TotalResults = 0, Vulnerabilities = [] };
        _ = nvdClient.GetAsync<CveResponse>(Arg.Any<string>(), cts.Token).Returns(_ => responseFirst, _ => responseSecond, _ => throw new Exception("To many requests"));

        var service = new NvdCveSyncService(logger, nvdClient);

        // Act
        var result = await service.FreshStartAsync(cts.Token);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());

        await nvdClient.Received().GetAsync<CveResponse>(Arg.Any<string>(), cts.Token);
    }
}
