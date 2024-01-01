using NvdReader.Domain.DataSources.SourceEntity;

namespace NvdReader.Application.Interfaces;

public interface ISourceService
{
    Task<Source?> GetSourceBySourceIdentifierAsync(string sourceIdentifier, CancellationToken cancellationToken);
}