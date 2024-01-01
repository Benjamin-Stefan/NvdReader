using CommunityToolkit.Diagnostics;
using System.Net.Http.Json;

namespace NvdReader.Infrastructure.ExternalClients;

public class RateLimitedHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly int _maxRequests;
    private readonly TimeSpan _timeSpan;
    private readonly Queue<DateTime> _requestTimestamps = new();

    public RateLimitedHttpClient(HttpClient httpClient, int maxRequests, TimeSpan timeSpan, string apiKey)
    {
        _httpClient = httpClient;
        _maxRequests = maxRequests;
        _timeSpan = timeSpan;

        SetApiKeyHeader(apiKey);
    }

    public void SetApiKeyHeader(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return;
        }

        _httpClient.DefaultRequestHeaders.Add("apiKey", apiKey);
    }

    public void CheckWaiting()
    {
        lock (_requestTimestamps)
        {
            if (_requestTimestamps.Count >= _maxRequests)
            {
                var timeSinceFirstRequest = DateTime.UtcNow - _requestTimestamps.Peek();
                if (timeSinceFirstRequest < _timeSpan)
                {
                    var delay = _timeSpan - timeSinceFirstRequest;
                    Thread.Sleep(delay);
                }

                _requestTimestamps.Dequeue();
            }

            _requestTimestamps.Enqueue(DateTime.UtcNow);
        }
    }

    public Task<T?> GetFromJsonAsync<T>(Uri? requestUri, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(requestUri, nameof(requestUri));

        CheckWaiting();

        return _httpClient.GetFromJsonAsync<T>(requestUri, cancellationToken);
    }
}
