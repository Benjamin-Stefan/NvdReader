using NvdReader.Application.Interfaces;
using NvdReader.Domain.Vulnerabilities.CveEntity;
using NvdReader.Domain.Vulnerabilities.CveHistoryEntity;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Application.Services;

public class CveService(ICveRepository cveRepository, ICveHistoryRepository cveHistoryRepository) : ICveService
{
    public async Task<Cve?> GetCveByIdAsync(string cveId, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cveId);

        var results = await cveRepository.GetAsync(cveId, cancellationToken);

        return results.FirstOrDefault();
    }

    public async Task<List<Change>?> GetChangeByCveIdAsync(string cveId, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cveId);

        var results = await cveHistoryRepository.SearchByCveAsync(cveId, cancellationToken);

        return results;
    }
}