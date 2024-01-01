using System.Diagnostics.CodeAnalysis;

namespace NvdReader.Domain.Vulnerabilities.Http;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CveHistoryRequest
{
    public DateTime? ChangeStartDate { get; set; }

    public DateTime? ChangeEndDate { get; set; }

    public string? CveId { get; set; }

    public int ResultsPerPage { get; set; }

    public int StartIndex { get; set; }
}