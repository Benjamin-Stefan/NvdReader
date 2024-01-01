using NvdReader.Domain.Vulnerabilities.CveHistoryEntity;

namespace NvdReader.Infrastructure.Interfaces;

public interface ICveHistoryRepository: IRepository<Change>
{
    Task<List<Change>> SearchByCveAsync(string cveId, CancellationToken cancellationToken);

    DateTime GetLastChange();
}