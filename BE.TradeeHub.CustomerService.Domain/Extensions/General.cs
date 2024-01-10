using System.ComponentModel;
using System.Reflection;

namespace BE.TradeeHub.CustomerService.Domain.Extensions;

public static class General
{
    public static string GetDescription(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString());

        var attributes = (DescriptionAttribute[])fi?.GetCustomAttributes(typeof(DescriptionAttribute), false)!;

        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}