using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Application.Validators;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Application.Interactors;

public sealed class NvdCveHistoryInteractor(ILogger<NvdCveHistoryInteractor> logger, INvdCveHistorySyncService syncService, ICveHistoryRepository repository) : INvdInteractor
{
    public async Task HandelAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> HandelAsync");

        var hasData = await repository.HasDataAsync(cancellationToken);
        if (hasData > 0)
        {
            var lastModified = repository.GetLastChange();

            NvdValidator.IsDateInRange(lastModified, true);

            var data = await syncService.UpdateSync(lastModified, cancellationToken);
            foreach (var cveChange in data)
            {
                var exists = await repository.SearchAsync(cveChange.Change.CveChangeId, cancellationToken);
                if (exists.Count == 0)
                {
                    await repository.CreateAsync(cveChange.Change, cancellationToken);
                }
                else
                {
                    cveChange.Change.Id = exists.FirstOrDefault()!.Id;

                    await repository.UpdateAsync(cveChange.Change.CveChangeId, cveChange.Change, cancellationToken);
                }
            }
        }
        else
        {
            var data = await syncService.FreshStartAsync(cancellationToken);
            var cveData = data.Select(t => t.Change);

            await repository.CreateManyAsync(cveData, cancellationToken);
        }
    }
}
