using MongoDB.Driver;
using NvdReader.Domain.Products.CpeEntity;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Infrastructure.Repository.Products;

public class CpeRepository(IMongodbContext context) : BaseRepository<Cpe>(context), ICpeRepository
{
    public async Task<List<Cpe>> SearchByCpeNameAsync(string cpeNameId, CancellationToken cancellationToken)
    {
        var filter = Builders<Cpe>.Filter.Eq(eq => eq.CpeNameId, cpeNameId);

        var searchResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await searchResult.ToListAsync(cancellationToken);
    }

    public new async Task UpdateAsync(string identifier, Cpe entity, CancellationToken cancellationToken)
    {
        var filter = Builders<Cpe>.Filter.Eq(eq => eq.CpeNameId, identifier);

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