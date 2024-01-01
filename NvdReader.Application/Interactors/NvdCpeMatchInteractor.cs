using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Application.Validators;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Application.Interactors;

public sealed class NvdCpeMatchInteractor(ILogger<NvdCpeMatchInteractor> logger, INvdCpeMatchSyncService syncService, ICpeMatchRepository repository) : INvdInteractor
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
            foreach (var matchStrings in data)
            {
                var exists = await repository.SearchAsync(matchStrings.MatchString.MatchCriteriaId, cancellationToken);
                if (exists.Count == 0)
                {
                    await repository.CreateAsync(matchStrings.MatchString, cancellationToken);
                }
                else
                {
                    matchStrings.MatchString.Id = exists.FirstOrDefault()!.Id;

                    await repository.UpdateAsync(matchStrings.MatchString.MatchCriteriaId, matchStrings.MatchString, cancellationToken);
                }
            }
        }
        else
        {
            var data = await syncService.FreshStartAsync(cancellationToken);
            var cveData = data.Select(t => t.MatchString);

            await repository.CreateManyAsync(cveData, cancellationToken);
        }
    }
}
