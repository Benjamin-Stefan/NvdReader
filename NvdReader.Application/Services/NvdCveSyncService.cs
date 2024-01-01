using Microsoft.Extensions.Logging;
using NvdReader.Application.Interfaces;
using NvdReader.Domain.Vulnerabilities.CveEntity;
using NvdReader.Domain.Vulnerabilities.Http;
using NvdReader.Infrastructure.ExternalClients.Nvd;
using NvdReader.Infrastructure.Interfaces;
using NvdReader.Shared.Extensions;

namespace NvdReader.Application.Services;

public class NvdCveSyncService(ILogger<NvdCveSyncService> logger, INvdApiClient nvdApiClient) : INvdCveSyncService
{
    public async Task<IEnumerable<Vulnerability>> UpdateSync(DateTime lastSync, CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> UpdateSync with {LastSync}", lastSync);
        var hashSet = new HashSet<Vulnerability>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new CveRequest
            {
                StartIndex = startIndex,
                LastModStartDate = lastSync,
                LastModEndDate = DateTime.Now
            };

            var data = await nvdApiClient.CveAsync(request, cancellationToken);
            
            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { Vulnerabilities.Length: > 0 })
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
                logger.LogDebug("Add {Count} more CVEs", data.Vulnerabilities.Length);
                hashSet.AddRange(data.Vulnerabilities);
            }
            else
            {
                logger.LogWarning("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }

    public async Task<IEnumerable<Vulnerability>> FreshStartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Call -> FreshStartAsync");
        var hashSet = new HashSet<Vulnerability>();
        var startIndex = 0;
        bool moreData;
        var firstRequests = true;

        do
        {
            var request = new CveRequest
            {
                StartIndex = startIndex
            };

            var data = await nvdApiClient.CveAsync(request, cancellationToken);

            if (firstRequests && data != null)
            {
                logger.LogInformation("TotalResults from api: {TotalResults}", data.TotalResults);
                firstRequests = false;
            }

            if (data is { Vulnerabilities.Length: > 0 })
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
                logger.LogInformation("Add {Count} more CVEs", data.Vulnerabilities.Length);
                hashSet.AddRange(data.Vulnerabilities);
            }
            else
            {
                logger.LogInformation("Nvd Response is null!");
            }

        } while (moreData);

        return hashSet;
    }
}
