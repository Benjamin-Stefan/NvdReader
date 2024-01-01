using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Application.Validators;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Application.Interactors;

public class NvdSourceInteractor(ILogger<NvdSourceInteractor> logger, INvdSourceSyncService syncService, ISourceRepository repository) : INvdInteractor
{
    public async Task HandelAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> HandelAsync");

        var hasData = await repository.HasDataAsync(cancellationToken);
        if (hasData > 0)
        {
            var lastModified = repository.GetLastModified();

            NvdValidator.IsDateInRange(lastModified, true);

            var data = await syncService.UpdateSync(lastModified, cancellationToken);
            foreach (var source in data)
            {
                if (string.IsNullOrWhiteSpace(source.Name))
                {
                    continue;
                }

                var exists = await repository.SearchAsync(source.Name, cancellationToken);
                if (exists.Count == 0)
                {
                    await repository.CreateAsync(source, cancellationToken);
                }
                else
                {
                    source.Id = exists.FirstOrDefault()!.Id;

                    await repository.UpdateAsync(source.Name, source, cancellationToken);
                }
            }
        }
        else
        {
            var data = await syncService.FreshStartAsync(cancellationToken);

            await repository.CreateManyAsync(data, cancellationToken);
        }
    }
}
