using NvdReader.Domain.Vulnerabilities.CveEntity;

namespace NvdReader.Application.Interfaces;

public interface INvdCveSyncService
{
    Task<IEnumerable<Vulnerability>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken);
    Task<IEnumerable<Vulnerability>> FreshStartAsync(CancellationToken cancellationToken);
}