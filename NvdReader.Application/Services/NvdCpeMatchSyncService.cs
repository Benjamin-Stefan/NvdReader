using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Domain.Products.CpeMatchEntity;
using NvdReader.Domain.Products.Http;
using NvdReader.Infrastructure.ExternalClients.Nvd;
using NvdReader.Infrastructure.Interfaces;
using NvdReader.Shared.Extensions;

namespace NvdReader.Application.Services;

public class NvdCpeMatchSyncService(ILogger<NvdCpeMatchSyncService> logger, INvdApiClient nvdApiClient) : INvdCpeMatchSyncService
{
    public async Task<IEnumerable<MatchStrings>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> UpdateSync with {LastSync}", lastSync);
        var hashSet = new HashSet<MatchStrings>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new CpeMatchRequest
            {
                StartIndex = startIndex,
                LastModStartDate = lastSync,
                LastModEndDate = DateTime.Now
            };

            var data = await nvdApiClient.CpeMatchAsync(request, cancellationToken);
            
            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { MatchStrings.Length: > 0 })
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
                logger.LogDebug("Add {Count} more", data.MatchStrings.Length);
                hashSet.AddRange(data.MatchStrings);
            }
            else
            {
                logger.LogWarning("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }

    public async Task<IEnumerable<MatchStrings>> FreshStartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> FreshStartAsync");
        var hashSet = new HashSet<MatchStrings>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new CpeMatchRequest
            {
                StartIndex = startIndex
            };

            var data = await nvdApiClient.CpeMatchAsync(request, cancellationToken);
            
            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { MatchStrings.Length: > 0 })
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
                logger.LogDebug("Add {Count} more", data.MatchStrings.Length);
                hashSet.AddRange(data.MatchStrings);
            }
            else
            {
                logger.LogWarning("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }
}
