using MongoDB.Driver;

namespace NvdReader.Infrastructure.Interfaces;

public interface IMongodbContext
{
    IMongoCollection<T> GetCollection<T>(string name) where T : class;
}