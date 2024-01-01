namespace NvdReader.Infrastructure.ExternalClients.Nvd;

public class NvdApiClientOptions
{
    private const int MaxRequestsWithApiKey = 50;
    private const int MaxRequestsWithoutApiKey = 5;

    public int MaxRequests { get; init; } = MaxRequestsWithoutApiKey;

    public TimeSpan TimeSpan { get; init; } = new(0, 0, 30);

    public static NvdApiClientOptions GetOptions(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return new NvdApiClientOptions();
        }

        var options = new NvdApiClientOptions
        {
            MaxRequests = MaxRequestsWithApiKey
        };

        return options;
    }
}