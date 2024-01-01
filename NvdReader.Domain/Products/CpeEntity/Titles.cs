using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.Products.CpeEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Titles
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("lang")]
    public string? Lang { get; set; }
}