using MongoDB.Driver;
using NvdReader.Domain.Vulnerabilities.CveEntity;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Infrastructure.Repository.Vulnerabilities;

public class CveRepository(IMongodbContext context) : BaseRepository<Cve>(context), ICveRepository
{
    public new async Task<List<Cve>> SearchAsync(string entityIdentifier, CancellationToken cancellationToken)
    {
        var filter = Builders<Cve>.Filter.Eq(eq => eq.Id, entityIdentifier);

        var searchResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await searchResult.ToListAsync(cancellationToken);
    }

    public new async Task UpdateAsync(string identifier, Cve entity, CancellationToken cancellationToken)
    {
        var filter = Builders<Cve>.Filter.Eq(eq => eq.Id, identifier);

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
