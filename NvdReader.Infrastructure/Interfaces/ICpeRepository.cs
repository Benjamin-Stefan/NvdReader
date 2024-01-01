using NvdReader.Domain.Products.CpeEntity;

namespace NvdReader.Infrastructure.Interfaces;

public interface ICpeRepository : IRepository<Cpe>
{
    Task<List<Cpe>> SearchByCpeNameAsync(string cpeNameId, CancellationToken cancellationToken);

    DateTime GetLastModified();
}