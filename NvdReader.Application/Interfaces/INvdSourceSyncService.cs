using NvdReader.Domain.DataSources.SourceEntity;

namespace NvdReader.Application.Interfaces;

public interface INvdSourceSyncService
{
    Task<IEnumerable<Source>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken);
    Task<IEnumerable<Source>> FreshStartAsync(CancellationToken cancellationToken);
}