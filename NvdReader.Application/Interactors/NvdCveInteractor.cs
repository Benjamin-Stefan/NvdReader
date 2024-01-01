using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Application.Validators;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Application.Interactors;

public sealed class NvdCveInteractor(ILogger<NvdCveInteractor> logger, INvdCveSyncService syncService, ICveRepository repository) : INvdInteractor
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
            foreach (var vulnerability in data)
            {
                var exists = await repository.SearchAsync(vulnerability.Cve.Id, cancellationToken);
                if (exists.Count == 0)
                {
                    await repository.CreateAsync(vulnerability.Cve, cancellationToken);
                }
                else
                {
                    await repository.UpdateAsync(vulnerability.Cve.Id, vulnerability.Cve, cancellationToken);
                }
            }
        }
        else
        {
            var data = await syncService.FreshStartAsync(cancellationToken);
            var cveData = data.Select(t => t.Cve);

            await repository.CreateManyAsync(cveData, cancellationToken);
        }
    }
}
