using MongoDB.Driver;
using NvdReader.Domain.DataSources.SourceEntity;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Infrastructure.Repository.DataSources;

public class SourceRepository(IMongodbContext context) : BaseRepository<Source>(context), ISourceRepository
{
    public async Task<List<Source>> SearchBySourceIdentifierAsync(string sourceIdentity, CancellationToken cancellationToken)
    {
        var filter = Builders<Source>.Filter.AnyStringIn(em => em.SourceIdentifiers, sourceIdentity);

        var searchResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await searchResult.ToListAsync(cancellationToken);
    }

    public new async Task<List<Source>> SearchAsync(string entityIdentifier, CancellationToken cancellationToken)
    {
        var filter = Builders<Source>.Filter.Eq(eq => eq.Name, entityIdentifier);

        var searchResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await searchResult.ToListAsync(cancellationToken);
    }

    public new async Task UpdateAsync(string identifier, Source entity, CancellationToken cancellationToken)
    {
        var filter = Builders<Source>.Filter.Eq(eq => eq.Name, identifier);

        await Collection.FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public DateTime GetLastModified()
    {
        var data = Collection
            .AsQueryable()
            .OrderByDescending(t => t.LastModified)
            .Take(1)
            .First();

        return data.LastModified;
    }
}
