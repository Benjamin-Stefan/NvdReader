using NvdReader.Domain.Vulnerabilities.CveEntity;

namespace NvdReader.Infrastructure.Interfaces;

public interface ICveRepository : IRepository<Cve>
{
    DateTime GetLastModified();
}