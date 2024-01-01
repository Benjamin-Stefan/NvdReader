using Microsoft.FeatureManagement;
using NvdReader.Application.Interactors;

namespace NvdReader.Api.BackgroundTasks;

public class NvdBackgroundTask(ILogger<NvdBackgroundTask> logger, IFeatureManager manager, IServiceProvider services) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var isEnabled = await manager.IsEnabledAsync("NvdTask");
        if (isEnabled == false)
        {
            return;
        }

        logger.LogInformation("Call -> ExecuteAsync");

        while (cancellationToken.IsCancellationRequested == false)
        {
            using (var scope = services.CreateScope())
            {
                var nvdSourceInteractor = scope.ServiceProvider.GetRequiredService<NvdSourceInteractor>();
                await nvdSourceInteractor.HandelAsync(cancellationToken);

                var nvdCveInteractor = scope.ServiceProvider.GetRequiredService<NvdCveInteractor>();
                await nvdCveInteractor.HandelAsync(cancellationToken);

                var nvdCveHistoryInteractor = scope.ServiceProvider.GetRequiredService<NvdCveHistoryInteractor>();
                await nvdCveHistoryInteractor.HandelAsync(cancellationToken);

                var nvdCpeInteractor = scope.ServiceProvider.GetRequiredService<NvdCpeInteractor>();
                await nvdCpeInteractor.HandelAsync(cancellationToken);

                var nvdCpeMatchInteractor = scope.ServiceProvider.GetRequiredService<NvdCpeMatchInteractor>();
                await nvdCpeMatchInteractor.HandelAsync(cancellationToken);
            }

            await Task.Delay(TimeSpan.FromHours(2), cancellationToken);
        }
    }
}