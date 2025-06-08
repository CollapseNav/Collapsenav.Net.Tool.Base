using System.ComponentModel;

namespace Collapsenav.Net.Tool;

public static class EnumExt
{
    public static object? ToEnum(this string value, Type type)
    {
        var values = Enum.GetValues(type);
        foreach (var v in values)
        {
            if (v == null)
                continue;
            var attributes = v.GetType().GetField(v.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.IsEmpty() && v.ToString() != value)
                continue;
            else if (attributes.NotEmpty())
            {
                var attribute = (DescriptionAttribute)attributes[0];
                if (attribute.Description == value)
                    return v;
            }
        }
        return null;
    }

    public static T? ToEnum<T>(this string value) where T : struct, Enum
    {
        var type = typeof(T);
        var enumValue = ToEnum(value, type);
        if (enumValue is T t)
            return t;
        return null;
    }

    public static string Description<T>(this T value) where T : struct, Enum
    {
        var fieldInfo = typeof(T).GetField(value.ToString());
        var attributes = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attributes.IsEmpty())
            return value.ToString();
        return ((DescriptionAttribute)attributes[0]).Description;
    }
}