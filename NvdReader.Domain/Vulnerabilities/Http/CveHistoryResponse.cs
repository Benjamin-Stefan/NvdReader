using NvdReader.Domain.Vulnerabilities.CveHistoryEntity;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.Http;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CveHistoryResponse : BaseResponse
{
    [JsonPropertyName("cveChanges")]
    public required CveChange[] CveChanges { get; set; }
}