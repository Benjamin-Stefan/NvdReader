using NvdReader.Domain.Vulnerabilities.CveEntity;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Vulnerabilities.Http;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CveResponse : BaseResponse
{
    [JsonPropertyName("vulnerabilities")]
    public required Vulnerability[] Vulnerabilities { get; set; }
}