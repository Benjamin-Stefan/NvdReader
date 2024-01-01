using MongoDB.Driver;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Infrastructure.Repository;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected IMongodbContext Context;
    protected IMongoCollection<T> Collection;

    protected BaseRepository(IMongodbContext context)
    {
        Context = context;
        Collection = Context.GetCollection<T>(typeof(T).Name);
    }

    public Task<long> HasDataAsync(CancellationToken cancellationToken)
    {
        return Collection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);
    }

    public Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        return Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public Task CreateManyAsync(IEnumerable<T> entity, CancellationToken cancellationToken)
    {
        return Collection.InsertManyAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task<List<T>> GetAsync(string id, CancellationToken cancellationToken)
    {
        var result = await Collection.FindAsync(Builders<T>.Filter.Eq("Id", id), cancellationToken: cancellationToken);
        
        return await result.ToListAsync(cancellationToken);
    }

    public async Task<List<T>> SearchAsync(string entityIdentifier, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq("Id", entityIdentifier);

        var searchResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await searchResult.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(string id, T entity, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);

        await Collection.FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
    }
}
