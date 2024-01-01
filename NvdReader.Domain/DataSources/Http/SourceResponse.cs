using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using NvdReader.Domain.DataSources.SourceEntity;

namespace NvdReader.Domain.DataSources.Http;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class SourceResponse : BaseResponse
{
    [JsonPropertyName("sources")]
    public required Source[] Sources { get; set; }
}