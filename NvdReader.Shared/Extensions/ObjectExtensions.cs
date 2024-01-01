using System.Web;

namespace NvdReader.Shared.Extensions;

public static class ObjectExtensions
{
    public static string GetQueryString(this object obj)
    {
        var properties = obj.GetType()
            .GetProperties()
            .Where(p => p.GetValue(obj, null) != null)
            .Select(p =>
            {
                if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                {
                    var propertyValue = p.GetValue(obj, null)!.ToString();
                    var value = propertyValue != null
                        ? DateTime.Parse(propertyValue).ToString("yyyy-MM-ddTHH:mm:ss")
                        : null;

                    return $"{p.Name}={value}";
                }

                return $"{p.Name}={HttpUtility.UrlEncode(p.GetValue(obj, null)!.ToString())}";

            });

        return string.Join("&", properties.ToArray());
    }
}
