﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace NvdReader.Domain.DataSources.SourceEntity;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class V3AcceptanceLevel
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("lastModified")]
    public DateTime LastModified { get; set; }
}