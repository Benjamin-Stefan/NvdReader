using NvdReader.Domain.DataSources.Http;
using NvdReader.Domain.Products.Http;
using NvdReader.Domain.Vulnerabilities.Http;
using NvdReader.Infrastructure.Interfaces;
using NvdReader.Shared.Extensions;

namespace NvdReader.Infrastructure.ExternalClients.Nvd;

public static class NvdApiClientExtensions
{
    public static Task<SourceResponse?> SourceAsync(this INvdApiClient client, SourceRequest request, CancellationToken cancellationToken)
        => client.GetAsync<SourceResponse?>(GetUri(NvdEndpoints.NvdSourceApiUrl, request.GetQueryString()), cancellationToken);

    public static Task<CveResponse?> CveAsync(this INvdApiClient client, CveRequest request, CancellationToken cancellationToken)
        => client.GetAsync<CveResponse?>(GetUri(NvdEndpoints.NvdCveApiUrl, request.GetQueryString()), cancellationToken);

    public static Task<CveHistoryResponse?> CveHistoryAsync(this INvdApiClient client, CveHistoryRequest request, CancellationToken cancellationToken)
        => client.GetAsync<CveHistoryResponse?>(GetUri(NvdEndpoints.NvdCveHistoryApiUrl, request.GetQueryString()), cancellationToken);
    
    public static Task<CpeResponse?> CpeAsync(this INvdApiClient client, CpeRequest request, CancellationToken cancellationToken)
        => client.GetAsync<CpeResponse?>(GetUri(NvdEndpoints.NvdCpeApiUrl, request.GetQueryString()), cancellationToken);

    public static Task<CpeMatchResponse?> CpeMatchAsync(this INvdApiClient client, CpeMatchRequest request, CancellationToken cancellationToken)
        => client.GetAsync<CpeMatchResponse?>(GetUri(NvdEndpoints.NvdCpeMatchApiUrl, request.GetQueryString()), cancellationToken);

    private static string GetUri(string uri, string parameter)
    {
        return string.IsNullOrWhiteSpace(parameter) 
            ? uri 
            : $"{uri}?{parameter}";
    }
}
