using NvdReader.Domain.Products.CpeEntity;

namespace NvdReader.Application.Interfaces;

public interface INvdCpeSyncService
{
    Task<IEnumerable<Product>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> FreshStartAsync(CancellationToken cancellationToken);
}