namespace NvdReader.Shared.Extensions;

public static class HashSetExtensions
{
    public static void AddRange<T>(this HashSet<T> source, T[] values)
    {
        foreach (var value in values)
        {
            source.Add(value);
        }
    }
}