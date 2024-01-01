using NvdReader.Infrastructure.Exceptions;

namespace NvdReader.Application.Validators;

public static class NvdValidator
{
    private const int MaxDays = 120;

    public static bool IsDateInRange(DateTime lastModified, bool throwException = false)
    {
        var dateDiff = DateTime.UtcNow - lastModified;

        var isInRage = dateDiff.Days < MaxDays;

        if (isInRage == false && throwException)
        {
            throw new DateNotInRangeException($"DateTime is not in Range. The maximum allowable range is {MaxDays} days.");
        }

        return isInRage;
    }
}