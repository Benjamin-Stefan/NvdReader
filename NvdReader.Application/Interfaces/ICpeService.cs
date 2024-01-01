using NvdReader.Domain.Products.CpeEntity;
using NvdReader.Domain.Products.CpeMatchEntity;

namespace NvdReader.Application.Interfaces;

public interface ICpeService
{
    Task<List<Cpe>?> GetCpeByNameAsync(string cpeNameId, CancellationToken cancellationToken);

    Task<List<MatchString>?> GetCpeMatchByMatchCriteriaAsync(string matchCriteriaId, CancellationToken cancellationToken);
}