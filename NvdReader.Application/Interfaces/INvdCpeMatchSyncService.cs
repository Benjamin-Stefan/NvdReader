using NvdReader.Domain.Products.CpeMatchEntity;

namespace NvdReader.Application.Interfaces;

public interface INvdCpeMatchSyncService
{
    Task<IEnumerable<MatchStrings>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken);
    Task<IEnumerable<MatchStrings>> FreshStartAsync(CancellationToken cancellationToken);
}