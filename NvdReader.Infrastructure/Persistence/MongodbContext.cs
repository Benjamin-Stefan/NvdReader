using MongoDB.Driver;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Infrastructure.Persistence;

public class MongodbContext(IMongoDatabase mongoDatabase) : IMongodbContext
{
    public IMongoDatabase Database { get; init; } = mongoDatabase;

    public IMongoCollection<T> GetCollection<T>(string name) where T : class
    {
        return Database.GetCollection<T>(name);
    }
}