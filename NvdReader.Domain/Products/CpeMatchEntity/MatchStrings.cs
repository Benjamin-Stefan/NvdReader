using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Products.CpeMatchEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class MatchStrings
{
    [JsonPropertyName("matchString")]
    public required MatchString MatchString { get; set; }
}