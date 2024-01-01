using NvdReader.Domain.Vulnerabilities.CveEntity;
using NvdReader.Domain.Vulnerabilities.CveHistoryEntity;

namespace NvdReader.Application.Interfaces;

public interface ICveService
{
    Task<Cve?> GetCveByIdAsync(string cveId, CancellationToken cancellationToken);

    Task<List<Change>?> GetChangeByCveIdAsync(string cveId, CancellationToken cancellationToken);
}