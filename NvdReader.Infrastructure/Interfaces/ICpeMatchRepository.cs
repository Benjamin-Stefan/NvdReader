using NvdReader.Domain.Products.CpeMatchEntity;

namespace NvdReader.Infrastructure.Interfaces;

public interface ICpeMatchRepository : IRepository<MatchString>
{
    Task<List<MatchString>> SearchByMatchCriteriaAsync(string matchCriteriaId, CancellationToken cancellationToken);

    DateTime GetLastModified();
}