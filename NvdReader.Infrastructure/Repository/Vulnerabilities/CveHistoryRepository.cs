using MongoDB.Driver;
using NvdReader.Domain.Vulnerabilities.CveHistoryEntity;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Infrastructure.Repository.Vulnerabilities;

public class CveHistoryRepository(IMongodbContext context) : BaseRepository<Change>(context), ICveHistoryRepository
{
    public async Task<List<Change>> SearchByCveAsync(string cveId, CancellationToken cancellationToken)
    {
        var filter = Builders<Change>.Filter.Eq(eq => eq.CveId, cveId);

        var searchResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await searchResult.ToListAsync(cancellationToken);
    }

    public new async Task<List<Change>> SearchAsync(string entityIdentifier, CancellationToken cancellationToken)
    {
        var filter = Builders<Change>.Filter.Eq(eq => eq.CveChangeId, entityIdentifier);

        var searchResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await searchResult.ToListAsync(cancellationToken);
    }

    public new async Task UpdateAsync(string identifier, Change entity, CancellationToken cancellationToken)
    {
        var filter = Builders<Change>.Filter.Eq(eq => eq.CveChangeId, identifier);

        await Collection.FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public DateTime GetLastChange()
    {
        var data = Collection
            .AsQueryable()
            .OrderByDescending(t => t.Created)
            .Take(1)
            .First();

        return data.Created;
    }
}