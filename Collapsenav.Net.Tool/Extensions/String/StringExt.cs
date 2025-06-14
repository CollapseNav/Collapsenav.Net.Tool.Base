﻿using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool;

public static partial class StringExt
{

    #region pad 填充
    /// <summary>
    /// 填充
    /// </summary>
    /// <param name="obj">源</param>
    /// <param name="total">总长度, 正数为右填充, 负数为左填充</param>
    /// <param name="fill">填充字符</param>
    public static string Pad<T>(this T obj, int total, char? fill = ' ') => total > 0 ? obj.PadRight(Math.Abs(total), fill) : obj.PadLeft(Math.Abs(total), fill);
    /// <summary>
    /// 填充
    /// </summary>
    /// <param name="obj">源</param>
    /// <param name="total">总长度, 正数为右填充, 负数为左填充</param>
    /// <param name="act">一个委托</param>
    /// <param name="fill">填充字符</param>
    public static string Pad<T>(this T obj, int total, Func<T, object> act, char? fill = ' ') => total > 0 ? act(obj).PadRight(Math.Abs(total), fill) : act(obj).PadLeft(Math.Abs(total), fill);
    /// <summary>
    /// 左填充
    /// </summary>
    /// <param name="obj">源</param>
    /// <param name="total">总长度</param>
    /// <param name="fill">填充字符</param>
    public static string PadLeft<T>(this T obj, int total, char? fill = ' ') => obj?.ToString()?.PadLeft(total, fill ?? ' ') ?? string.Empty;
    /// <summary>
    /// 右填充
    /// </summary>
    /// <param name="obj">源</param>
    /// <param name="total">总长度</param>
    /// <param name="fill">填充字符</param>
    public static string PadRight<T>(this T obj, int total, char? fill = ' ') => obj?.ToString()?.PadRight(total, fill ?? ' ') ?? string.Empty;
    /// <summary>
    /// 左填充
    /// </summary>
    /// <param name="obj">源</param>
    /// <param name="total">总长度</param>
    /// <param name="act">一个委托</param>
    /// <param name="fill">填充字符</param>
    public static string PadLeft<T>(this T obj, int total, Func<T, object> act, char? fill = ' ') => act(obj)?.ToString()?.PadLeft(total, fill ?? ' ') ?? string.Empty;
    /// <summary>
    /// 右填充
    /// </summary>
    /// <param name="obj">源</param>
    /// <param name="total">总长度</param>
    /// <param name="act">一个委托</param>
    /// <param name="fill">填充字符</param>
    public static string PadRight<T>(this T obj, int total, Func<T, object> act, char? fill = ' ') => act(obj)?.ToString()?.PadRight(total, fill ?? ' ') ?? string.Empty;
    #endregion
    // /// <summary>
    // /// 数字转为中文,暂时只支持整数,支持最大的整数长度为16位
    // /// </summary>
    // /// <param name="num"></param>
    // public static string ToChinese(this string num)
    // {
    //     string[] nums = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
    //     string[] units = { "", "十", "百", "千", "万", "十", "百", "千", "亿", "十", "百", "千", "万", "十", "百", "千" };
    //     StringBuilder sb = new();
    //     var numChar = num.ToArray();
    //     var curUnits = units.Take(num.Length).Reverse().ToArray();
    //     foreach (var (ch, index) in numChar.Select((nchar, i) => (nchar - 48, i)))
    //     {
    //         sb.Append(nums[ch]);
    //         if (ch == 0 && !"亿万".Contains(curUnits[index]))
    //             continue;
    //         sb.Append(curUnits[index]);
    //     }
    //     Regex matchZero = new("零+");
    //     Regex matchLastZero = new("零+$");
    //     var result = matchZero.Replace(sb.ToString(), "零");
    //     result = matchLastZero.Replace(result.Replace("零万", "万零").Replace("零亿", "亿零"), "");
    //     return result;
    // }
    #region 字符串的格式转换
    public static DateTime? ToDateTime(this string input) => DateTime.TryParse(input, out DateTime result) ? result : null;
    public static int? ToInt(this string input) => int.TryParse(input, out int result) ? result : null;
    public static double? ToDouble(this string input) => double.TryParse(input, out double result) ? result : null;
    public static Guid? ToGuid(this string input) => Guid.TryParse(input, out Guid result) ? result : null;
    public static long? ToLong(this string input) => long.TryParse(input, out long result) ? result : null;
    public static object? ToValue(this string input, Type type, string? format = null)
    {
        var flag = type.HasMethod("Parse");
        if (flag)
        {
            var method = type.GetMethods().FirstOrDefault(item => item.Name == "Parse");
            if (type == typeof(DateTime))
            {
                var date = new DateTime();
                method = type.GetMethods().FirstOrDefault(item => item.Name == "TryParseExact");
                object[] args;
                if (format.NotEmpty())
                {
                    args = new object[] { input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, date };
                    flag = (bool?)method?.Invoke(null, args) ?? false;
                }
                else if (input.Contains("/"))
                {
                    method = type.GetMethods().FirstOrDefault(item => item.Name == "TryParse");
                    args = new object[] { input, date };
                    flag = (bool?)method?.Invoke(null, args) ?? false;
                }
                else if (input.Contains("."))
                {
                    args = new object[] { input, DateTimeExt.DefaultMilliFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, date };
                    flag = (bool?)method?.Invoke(null, args) ?? false;
                }
                else if (input.Contains(":"))
                {
                    args = new object[] { input, DateTimeExt.DefaultTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, date };
                    flag = (bool?)method?.Invoke(null, args) ?? false;
                }
                else
                {
                    args = new object[] { input, DateTimeExt.DefaultDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, date };
                    flag = (bool?)method?.Invoke(null, args) ?? false;
                }
                return flag ? args[^1] : null;
            }
            return method?.Invoke(null, new[] { input });
        }
        return input;
    }
    #endregion

    /// <summary>
    /// 拼!
    /// </summary>
    /// <param name="query">源集合</param>
    /// <param name="separate">分隔符</param>
    /// <param name="getStr">针对复杂类型的委托</param>
    public static string Join<T>(this IEnumerable<T> query, string separate = "", Func<T, object>? getStr = null)
    {
        return string.Join(separate, query.Select(getStr ?? (item => item?.ToString() ?? string.Empty)));
    }
    /// <summary>
    /// 将字符串分割并转换
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="origin"></param>
    /// <param name="selector"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static T[] SplitAs<T>(this string? origin, Func<string, T> selector, params char[]? separator)
    {
        if (origin.IsEmpty())
            return Array.Empty<T>();
        if (separator.IsEmpty())
            separator = new[] { ' ' };
        return origin.Split(separator).Select(selector).ToArray();
    }

    /// <summary>
    /// 获取 Domain 域名
    /// </summary>
    public static string GetDomain(this string input)
    {
        if (input.IsUrl())
        {
            int headerIndex = input.IndexOf("//") + 2;
            return input.Substring(headerIndex, input.IndexOf('/', headerIndex) > 0 ? input.IndexOf('/', headerIndex) - headerIndex : input.Length - headerIndex);
        }
        // TODO 还需要添加其他判断
        return string.Empty;
    }

    /// <summary>
    /// 自动遮罩(偷懒)
    /// </summary>
    public static string AutoMask(this string origin, string mask = "*")
    {
        // TODO 邮箱,手机,地址,网址,账号等可能需要特定的遮罩
        if (origin.Trim().IsEmpty())
            return "***";
        if (origin.Length <= 6)
        {
            origin = origin.PadLeft(6, '#');
            return Regex.Replace(origin, "(.{1}).*(.{1})", $"$1{mask}$2");
        }
        else
        {
            return Regex.Replace(origin, "(.{3}).*(.{3})", $"$1{mask}$2");
        }
    }
    /// <summary>
    /// 字符串转为byte[] 默认 utf8
    /// </summary>
    public static byte[] ToBytes(this string origin, Encoding? encode = null)
    {
        encode ??= Encoding.UTF8;
        return encode.GetBytes(origin);
    }
    /// <summary>
    /// 从前取
    /// </summary>
    public static string First(this string origin, int len = 1)
    {
        return len > origin.Length ? origin : origin[..len];
    }
    /// <summary>
    /// 从后取
    /// </summary>
    public static string Last(this string origin, int len = 1)
    {
        return len > origin.Length ? origin : origin.Substring(origin.Length - len, len);
    }
    /// <summary>
    /// byte[] 转为 hex 字符串
    /// </summary>
    public static string ToHexString(this byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
    }

    /// <summary>
    /// hex 字符串转为 byte[]
    /// </summary>
    public static byte[] HexToBytes(this string hex)
    {
        var bytes = new byte[hex.Length / 2];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)Convert.ToInt32(hex[(i * 2)..(i * 2 + 2)], 16);
        return bytes;
    }

    /// <summary>
    /// bytes转为string
    /// </summary>
    public static string BytesToString(this byte[] bytes, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return encoding.GetString(bytes);
    }

    /// <summary>
    /// 打乱字符串顺序
    /// </summary>
    public static string Shuffle(this string input)
    {
        return input.Shuffle<char>().Join();
    }

    public static string AddIf(this string origin, bool check, string value)
    {
        if (check)
            origin += value;
        return origin;
    }

    public static string AddIf(this string origin, string check, string value)
    {
        return check.IsEmpty() ? origin : origin + value;
    }

    public static string AddIf<T>(this string origin, T? check, string value) where T : struct
    {
        if (check.HasValue)
            origin += value;
        return origin;
    }

    #region 给集合中的字符串添加前后缀
    /// <summary>
    /// 加前缀
    /// </summary>
    public static IEnumerable<string> AddBegin(this IEnumerable<string> origin, string value)
    {
        origin = origin.Select(item => value + item);
        return origin;
    }
    /// <summary>
    /// 加后缀
    /// </summary>
    public static IEnumerable<string> AddEnd(this IEnumerable<string> origin, string value)
    {
        origin = origin.Select(item => item + value);
        return origin;
    }
    /// <summary>
    /// 加前缀
    /// </summary>
    public static string[] AddBegin(this string[] origin, string value)
    {
        for (var i = 0; i < origin.Length; i++)
            origin[i] = value + origin[i];
        return origin;
    }
    /// <summary>
    /// 加后缀
    /// </summary>
    public static string[] AddEnd(this string[] origin, string value)
    {
        for (var i = 0; i < origin.Length; i++)
            origin[i] += value;
        return origin;
    }
    /// <summary>
    /// 加前缀
    /// </summary>
    public static IList<string> AddBegin(this IList<string> origin, string value)
    {
        for (var i = 0; i < origin.Count; i++)
            origin[i] = value + origin[i];
        return origin;
    }
    /// <summary>
    /// 加后缀
    /// </summary>
    public static IList<string> AddEnd(this IList<string> origin, string value)
    {
        for (var i = 0; i < origin.Count; i++)
            origin[i] += value;
        return origin;
    }
    #endregion

    /// <summary>
    /// 从前取到
    /// </summary>
    public static string FirstTo(this string origin, string target)
    {
        var index = origin.IndexOf(target);
        return index == -1 ? origin : origin.First(index);
    }
    /// <summary>
    /// 从后取到
    /// </summary>
    public static string EndTo(this string origin, string target)
    {
        var index = origin.LastIndexOf(target);
        return index == -1 ? origin : origin.Last(origin.Length - (target.Length + index));
    }
    /// <summary>
    /// 首字母大写
    /// </summary>
    public static string UpFirstLetter(this string origin)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(origin);
    }
    /// <summary>
    /// 在两个字符串中间填充
    /// </summary>
    public static string PadWith(this string origin, string value, int len, char? fill = ' ')
    {
        if ((origin.Length + value.Length) >= len)
            return origin + value;
        return origin.PadRight(len - value.Length, fill) + value;
    }
}
