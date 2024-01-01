using NvdReader.Application.Interfaces;
using NvdReader.Domain.DataSources.SourceEntity;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Application.Services;

public class SourceService(ISourceRepository sourceRepository) : ISourceService
{
    public async Task<Source?> GetSourceBySourceIdentifierAsync(string sourceIdentifier, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceIdentifier);

        var results = await sourceRepository.SearchBySourceIdentifierAsync(sourceIdentifier, cancellationToken);

        return results.FirstOrDefault();
    }
}