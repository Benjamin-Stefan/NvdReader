namespace NvdReader.Infrastructure.ExternalClients.Nvd;

internal class NvdEndpoints
{
    public const string NvdUrl = "https://services.nvd.nist.gov/rest/json/";
    public const string NvdCveApiUrl = NvdUrl + "cves/2.0";
    public const string NvdCveHistoryApiUrl = NvdUrl + "cvehistory/2.0";
    public const string NvdCpeApiUrl = NvdUrl + "cpes/2.0";
    public const string NvdCpeMatchApiUrl = NvdUrl + "cpematch/2.0";
    public const string NvdSourceApiUrl = NvdUrl + "source/2.0";
}