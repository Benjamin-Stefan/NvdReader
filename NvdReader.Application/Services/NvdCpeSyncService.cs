using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Domain.Products.CpeEntity;
using NvdReader.Domain.Products.Http;
using NvdReader.Infrastructure.ExternalClients.Nvd;
using NvdReader.Infrastructure.Interfaces;
using NvdReader.Shared.Extensions;

namespace NvdReader.Application.Services;

public class NvdCpeSyncService(ILogger<NvdCpeSyncService> logger, INvdApiClient nvdApiClient) : INvdCpeSyncService
{
    public async Task<IEnumerable<Product>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> UpdateSync with {LastSync}", lastSync);
        var hashSet = new HashSet<Product>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new CpeRequest
            {
                StartIndex = startIndex,
                LastModStartDate = lastSync,
                LastModEndDate = DateTime.Now
            };

            var data = await nvdApiClient.CpeAsync(request, cancellationToken);
            
            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { Products.Length: > 0 })
            {
                moreData = true;
                startIndex += data.ResultsPerPage;
                logger.LogDebug("Next data with StartIndex: {StartIndex}", startIndex);
            }
            else
            {
                moreData = false;
            }

            if (data != null)
            {
                logger.LogDebug("Add {Count} more", data.Products.Length);
                hashSet.AddRange(data.Products);
            }
            else
            {
                logger.LogWarning("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }

    public async Task<IEnumerable<Product>> FreshStartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> FreshStartAsync");
        var hashSet = new HashSet<Product>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new CpeRequest
            {
                StartIndex = startIndex
            };

            var data = await nvdApiClient.CpeAsync(request, cancellationToken);
            
            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { Products.Length: > 0 })
            {
                moreData = true;
                startIndex += data.ResultsPerPage;
                logger.LogDebug("Next data with StartIndex: {StartIndex}", startIndex);
            }
            else
            {
                moreData = false;
            }

            if (data != null)
            {
                logger.LogDebug("Add {Count} more", data.Products.Length);
                hashSet.AddRange(data.Products);
            }
            else
            {
                logger.LogWarning("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }
}
