using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Logging;
using NvdReader.Domain.Options;
using NvdReader.Infrastructure.Interfaces;

namespace NvdReader.Infrastructure.ExternalClients.Nvd;

public class NvdApiClient : INvdApiClient
{
    private readonly RateLimitedHttpClient _httpClient;
    private readonly ILogger<NvdApiClient> _logger;

    public NvdApiClient(ILogger<NvdApiClient> logger, HttpClient httpClient, ApiClientOptions clientOptions)
    {
        _logger = logger;

        var options = NvdApiClientOptions.GetOptions(clientOptions.ApiKey);

        _httpClient = new RateLimitedHttpClient(httpClient, options.MaxRequests, options.TimeSpan, clientOptions.ApiKey);
    }

    public async Task<TResponse?> GetAsync<TResponse>(string path, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Call -> GetAsync with {Path}", path);
        Guard.IsNotNullOrWhiteSpace(path, nameof(path));

        var endpointUri = new Uri(path);

        var response = await _httpClient.GetFromJsonAsync<TResponse>(endpointUri, cancellationToken);

        return response;
    }
}
