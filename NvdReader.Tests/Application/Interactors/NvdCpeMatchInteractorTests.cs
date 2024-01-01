using Microsoft.Extensions.Logging;
using NSubstitute;
using NvdReader.Application.Interactors;
using NvdReader.Application.Interfaces;
using NvdReader.Domain.Products.CpeMatchEntity;
using NvdReader.Infrastructure.Exceptions;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Tests.Application.Interactors;

public class NvdCpeMatchInteractorTests
{
    [Fact]
    public async Task CreateDataTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCpeMatchInteractor>>();
        var service = Substitute.For<INvdCpeMatchSyncService>();
        service.UpdateSync(Arg.Any<DateTime>(), cts.Token).Returns(new List<MatchStrings> { new() { MatchString = new MatchString { MatchCriteriaId = "MatchCriteriaId1" } } });

        var repository = Substitute.For<ICpeMatchRepository>();
        repository.GetLastModified().Returns(DateTime.Now);
        repository.HasDataAsync(cts.Token).Returns(1);
        repository.SearchAsync(Arg.Any<string>(), cts.Token).Returns([]);

        var interactor = new NvdCpeMatchInteractor(logger, service, repository);

        // Act
        await interactor.HandelAsync(cts.Token);

        // Assert
        await repository.Received().CreateAsync(Arg.Any<MatchString>(), cts.Token);
        await repository.DidNotReceive().CreateManyAsync(Arg.Any<IEnumerable<MatchString>>(), cts.Token);
        await repository.Received().SearchAsync(Arg.Any<string>(), cts.Token);
        await repository.DidNotReceive().UpdateAsync(Arg.Any<string>(), Arg.Any<MatchString>(), cts.Token);
        await repository.Received().HasDataAsync(cts.Token);
        repository.Received().GetLastModified();
        await service.Received().UpdateSync(Arg.Any<DateTime>(), cts.Token);
        await service.DidNotReceive().FreshStartAsync(cts.Token);
    }

    [Fact]
    public async Task UpdateDataTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCpeMatchInteractor>>();
        var service = Substitute.For<INvdCpeMatchSyncService>();
        service.UpdateSync(Arg.Any<DateTime>(), cts.Token).Returns(new List<MatchStrings> { new() { MatchString = new MatchString { MatchCriteriaId = "MatchCriteriaId1" } } });

        var repository = Substitute.For<ICpeMatchRepository>();
        repository.GetLastModified().Returns(DateTime.Now);
        repository.HasDataAsync(cts.Token).Returns(1);
        repository.SearchAsync(Arg.Any<string>(), cts.Token).Returns([new MatchString { MatchCriteriaId = "MatchCriteriaId1" }]);

        var interactor = new NvdCpeMatchInteractor(logger, service, repository);

        // Act
        await interactor.HandelAsync(cts.Token);

        // Assert
        await repository.DidNotReceive().CreateAsync(Arg.Any<MatchString>(), cts.Token);
        await repository.DidNotReceive().CreateManyAsync(Arg.Any<IEnumerable<MatchString>>(), cts.Token);
        await repository.Received().SearchAsync(Arg.Any<string>(), cts.Token);
        await repository.Received().UpdateAsync(Arg.Any<string>(), Arg.Any<MatchString>(), cts.Token);
        await repository.Received().HasDataAsync(cts.Token);
        repository.Received().GetLastModified();
        await service.Received().UpdateSync(Arg.Any<DateTime>(), cts.Token);
        await service.DidNotReceive().FreshStartAsync(cts.Token);
    }

    [Fact]
    public async Task FreshStartTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCpeMatchInteractor>>();
        var service = Substitute.For<INvdCpeMatchSyncService>();

        var repository = Substitute.For<ICpeMatchRepository>();
        repository.HasDataAsync(cts.Token).Returns(0);

        var interactor = new NvdCpeMatchInteractor(logger, service, repository);

        // Act
        await interactor.HandelAsync(cts.Token);

        // Assert
        await repository.DidNotReceive().CreateAsync(Arg.Any<MatchString>(), cts.Token);
        await repository.Received().CreateManyAsync(Arg.Any<IEnumerable<MatchString>>(), cts.Token);
        await repository.DidNotReceive().SearchAsync(Arg.Any<string>(), cts.Token);
        await repository.DidNotReceive().UpdateAsync(Arg.Any<string>(), Arg.Any<MatchString>(), cts.Token);
        await repository.Received().HasDataAsync(cts.Token);
        repository.DidNotReceive().GetLastModified();
        await service.DidNotReceive().UpdateSync(Arg.Any<DateTime>(), cts.Token);
        await service.Received().FreshStartAsync(cts.Token);
    }

    [Fact]
    public async Task LastModifiedOver120DaysTest()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        var logger = Substitute.For<ILogger<NvdCpeMatchInteractor>>();
        var service = Substitute.For<INvdCpeMatchSyncService>();

        var repository = Substitute.For<ICpeMatchRepository>();
        repository.GetLastModified().Returns(DateTime.Now.AddDays(-121));
        repository.HasDataAsync(cts.Token).Returns(1);

        var interactor = new NvdCpeMatchInteractor(logger, service, repository);

        // Act
        await Assert.ThrowsAsync<DateNotInRangeException>( () => interactor.HandelAsync(cts.Token));

        // Assert
        await repository.DidNotReceive().CreateAsync(Arg.Any<MatchString>(), cts.Token);
        await repository.DidNotReceive().CreateManyAsync(Arg.Any<IEnumerable<MatchString>>(), cts.Token);
        await repository.DidNotReceive().SearchAsync(Arg.Any<string>(), cts.Token);
        await repository.DidNotReceive().UpdateAsync(Arg.Any<string>(), Arg.Any<MatchString>(), cts.Token);
        await repository.Received().HasDataAsync(cts.Token);
        repository.Received().GetLastModified();
        await service.DidNotReceive().UpdateSync(Arg.Any<DateTime>(), cts.Token);
        await service.DidNotReceive().FreshStartAsync(cts.Token);
    }
}
