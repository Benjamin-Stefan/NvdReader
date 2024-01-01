using NvdReader.Domain.Vulnerabilities.CveHistoryEntity;

namespace NvdReader.Application.Interfaces;

public interface INvdCveHistorySyncService
{
    Task<IEnumerable<CveChange>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken);
    Task<IEnumerable<CveChange>> FreshStartAsync(CancellationToken cancellationToken);
}