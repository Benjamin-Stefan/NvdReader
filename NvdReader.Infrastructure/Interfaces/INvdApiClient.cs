namespace NvdReader.Infrastructure.Interfaces;

public interface INvdApiClient
{
    Task<TResponse?> GetAsync<TResponse>(string path, CancellationToken cancellationToken);
}