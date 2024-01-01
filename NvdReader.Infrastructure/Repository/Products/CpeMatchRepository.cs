using MongoDB.Driver;
using NvdReader.Domain.Products.CpeMatchEntity;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Infrastructure.Repository.Products;

public class CpeMatchRepository(IMongodbContext context) : BaseRepository<MatchString>(context), ICpeMatchRepository
{
    public async Task<List<MatchString>> SearchByMatchCriteriaAsync(string matchCriteriaId, CancellationToken cancellationToken)
    {
        var filter = Builders<MatchString>.Filter.Eq(eq => eq.MatchCriteriaId, matchCriteriaId);

        var searchResult = await Collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await searchResult.ToListAsync(cancellationToken);
    }

    public new async Task UpdateAsync(string identifier, MatchString entity, CancellationToken cancellationToken)
    {
        var filter = Builders<MatchString>.Filter.Eq(eq => eq.MatchCriteriaId, identifier);

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