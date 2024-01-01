using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.CveEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Metrics
{
    [JsonPropertyName("cvssMetricV2")]
    public CvssMetricV2[]? CvssMetricV2 { get; set; }

    [JsonPropertyName("cvssMetricV31")]
    public CvssMetricV31[]? CvssMetricV31 { get; set; }

    [JsonPropertyName("cvssMetricV30")]
    public CvssMetricV30[]? CvssMetricV30 { get; set; }
}