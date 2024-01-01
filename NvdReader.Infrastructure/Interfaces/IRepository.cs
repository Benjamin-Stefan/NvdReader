namespace NvdReader.Infrastructure.Interfaces;

public interface IRepository<T>
{
    Task<long> HasDataAsync(CancellationToken cancellationToke);

    Task<List<T>> GetAsync(string id, CancellationToken cancellationToken);

    Task CreateAsync(T entity, CancellationToken cancellationToken);

    Task CreateManyAsync(IEnumerable<T> entity, CancellationToken cancellationToken);

    Task<List<T>> SearchAsync(string entityIdentifier, CancellationToken cancellationToken);

    // Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task UpdateAsync(string id, T entity, CancellationToken cancellationToken);
}