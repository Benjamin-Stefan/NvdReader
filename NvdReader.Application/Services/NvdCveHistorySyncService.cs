using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Domain.Vulnerabilities.CveHistoryEntity;
using NvdReader.Domain.Vulnerabilities.Http;
using NvdReader.Infrastructure.ExternalClients.Nvd;
using NvdReader.Infrastructure.Interfaces;
using NvdReader.Shared.Extensions;

namespace NvdReader.Application.Services;

public class NvdCveHistorySyncService(ILogger<NvdCveHistorySyncService> logger, INvdApiClient nvdApiClient) : INvdCveHistorySyncService
{
    public async Task<IEnumerable<CveChange>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> UpdateSync with {LastSync}", lastSync);
        var hashSet = new HashSet<CveChange>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new CveHistoryRequest
            {
                StartIndex = startIndex,
                ChangeStartDate = lastSync,
                ChangeEndDate = DateTime.Now
            };

            var data = await nvdApiClient.CveHistoryAsync(request, cancellationToken);
            
            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { CveChanges.Length: > 0 })
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
                logger.LogDebug("Add {Count} more CVE changes", data.CveChanges.Length);
                hashSet.AddRange(data.CveChanges);
            }
            else
            {
                logger.LogWarning("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }

    public async Task<IEnumerable<CveChange>> FreshStartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> FreshStartAsync");
        var hashSet = new HashSet<CveChange>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new CveHistoryRequest
            {
                StartIndex = startIndex
            };

            var data = await nvdApiClient.CveHistoryAsync(request, cancellationToken);

            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { CveChanges.Length: > 0 })
            {
                moreData = true;
                startIndex += data.ResultsPerPage;
                logger.LogInformation("Next data with StartIndex: {StartIndex}", startIndex);
            }
            else
            {
                moreData = false;
            }

            if (data != null)
            {
                logger.LogInformation("Add {Count} more CVE changes", data.CveChanges.Length);
                hashSet.AddRange(data.CveChanges);
            }
            else
            {
                logger.LogInformation("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }
}