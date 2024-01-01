using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Application.Validators;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Application.Interactors;

public sealed class NvdCpeInteractor(ILogger<NvdCpeInteractor> logger, INvdCpeSyncService syncService, ICpeRepository repository) : INvdInteractor
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
            foreach (var product in data)
            {
                var exists = await repository.SearchAsync(product.Cpe.CpeNameId, cancellationToken);
                if (exists.Count == 0)
                {
                    await repository.CreateAsync(product.Cpe, cancellationToken);
                }
                else
                {
                    product.Cpe.Id = exists.FirstOrDefault()!.Id;

                    await repository.UpdateAsync(product.Cpe.CpeNameId, product.Cpe, cancellationToken);
                }
            }
        }
        else
        {
            var products = await syncService.FreshStartAsync(cancellationToken);
            var entity = products.Select(t => t.Cpe);

            await repository.CreateManyAsync(entity, cancellationToken);
        }
    }
}
