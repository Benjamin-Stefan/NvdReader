using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using NvdReader.Domain.Products.CpeMatchEntity;

namespace NvdReader.Domain.Products.Http;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CpeMatchResponse : BaseResponse
{
    [JsonPropertyName("matchStrings")]
    public required MatchStrings[] MatchStrings { get; set; }
}