using System.Diagnostics.CodeAnalysis;

namespace NvdReader.Domain.DataSources.Http;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class SourceRequest
{
    public DateTime? LastModStartDate { get; set; }

    public DateTime? LastModEndDate { get; set; }

    public int? ResultsPerPage { get; set; }

    public int StartIndex { get; set; }
}