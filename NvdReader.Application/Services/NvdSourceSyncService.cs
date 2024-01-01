using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Domain.DataSources.Http;
using NvdReader.Domain.DataSources.SourceEntity;
using NvdReader.Infrastructure.ExternalClients.Nvd;
using NvdReader.Infrastructure.Interfaces;
using NvdReader.Shared.Extensions;

namespace NvdReader.Application.Services;

public class NvdSourceSyncService(ILogger<NvdSourceSyncService> logger, INvdApiClient nvdApiClient) : INvdSourceSyncService
{
    public async Task<IEnumerable<Source>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> UpdateSync with {LastSync}", lastSync);
        var hashSet = new HashSet<Source>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new SourceRequest
            {
                StartIndex = startIndex,
                LastModStartDate = lastSync,
                LastModEndDate = DateTime.Now
            };

            var data = await nvdApiClient.SourceAsync(request, cancellationToken);
            
            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { Sources.Length: > 0 })
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
                logger.LogDebug("Add {Count} more", data.Sources.Length);
                hashSet.AddRange(data.Sources);
            }
            else
            {
                logger.LogWarning("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }

    public async Task<IEnumerable<Source>> FreshStartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> FreshStartAsync");
        var hashSet = new HashSet<Source>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new SourceRequest
            {
                StartIndex = startIndex
            };

            var data = await nvdApiClient.SourceAsync(request, cancellationToken);
            
            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { Sources.Length: > 0 })
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
                logger.LogDebug("Add {Count} more", data.Sources.Length);
                hashSet.AddRange(data.Sources);
            }
            else
            {
                logger.LogWarning("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }
}