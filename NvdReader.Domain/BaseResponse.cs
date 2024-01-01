using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class BaseResponse
{
    [JsonPropertyName("resultsPerPage")]
    public required int ResultsPerPage { get; set; }

    [JsonPropertyName("startIndex")]
    public required int StartIndex { get; set; }

    [JsonPropertyName("totalResults")]
    public required int TotalResults { get; set; }

    [JsonPropertyName("format")]
    public required string Format { get; set; }

    [JsonPropertyName("version")]
    public required string Version { get; set; }

    [JsonPropertyName("timestamp")]
    public required DateTime Timestamp { get; set; }
}