using NvdReader.Application.Interfaces;
using NvdReader.Domain.Products.CpeEntity;
using NvdReader.Domain.Products.CpeMatchEntity;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Application.Services;

public class CpeService(ICpeRepository cpeRepository, ICpeMatchRepository cpeMatchRepository) : ICpeService
{
    public async Task<List<Cpe>?> GetCpeByNameAsync(string cpeNameId, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cpeNameId);

        var results = await cpeRepository.SearchByCpeNameAsync(cpeNameId, cancellationToken);

        return results;
    }

    public async Task<List<MatchString>?> GetCpeMatchByMatchCriteriaAsync(string matchCriteriaId, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(matchCriteriaId);

        var results = await cpeMatchRepository.SearchByMatchCriteriaAsync(matchCriteriaId, cancellationToken);

        return results;
    }
}