using NvdReader.Domain.DataSources.SourceEntity;

namespace NvdReader.Infrastructure.Interfaces;

public interface ISourceRepository : IRepository<Source>
{
    Task<List<Source>> SearchBySourceIdentifierAsync(string sourceIdentity, CancellationToken cancellationToken);

    DateTime GetLastModified();
}