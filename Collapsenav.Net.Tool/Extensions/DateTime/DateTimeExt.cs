using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool;

public enum DateTimeType
{
    Year,
    Month,
    Day,
    Hour,
    Minute,
    Second
}
public static partial class DateTimeExt
{
    /// <summary>
    /// DateTime转为时间戳
    /// </summary>
    public static long ToTimestamp(this DateTime time)
    {
        return new DateTimeOffset(time).ToUnixTimeMilliseconds();
    }
    /// <summary>
    /// DateTime转为短时间戳(10位)
    /// </summary>
    public static long ToShortTimestamp(this DateTime time)
    {
        return new DateTimeOffset(time).ToUnixTimeSeconds();
    }
    /// <summary>
    /// 时间戳转为DateTime
    /// </summary>
    public static DateTime ToDateTime(this long timestamp)
    {
        return timestamp <= 0 ? DateTime.Now : DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
    }
    /// <summary>
    /// 时间戳转为DateTime
    /// </summary>
    public static DateTime ToShortDateTime(this long timestamp)
    {
        return timestamp <= 0 ? DateTime.Now : DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
    }


    /// <summary>
    /// 转为默认日期格式字符串(yyyy-MM-dd)
    /// </summary>
    /// <param name="date"></param>
    /// <param name="s">中间的分隔符,'-' </param>
    public static string ToDefaultDateString(this DateTime date, string s)
    {
        return date.ToString($"yyyy-MM-dd").Replace("-", s);
    }
    /// <summary>
    /// 转为默认日期格式字符串(yyyy-MM-dd)
    /// </summary>
    /// <param name="date"></param>
    public static string ToDefaultDateString(this DateTime date)
    {
        return date.ToString($"yyyy-MM-dd");
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss)
    /// </summary>
    /// <param name="time"></param>
    public static string ToDefaultTimeString(this DateTime time)
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss");
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">分隔符,'-' </param>
    public static string ToDefaultTimeString(this DateTime time, string f)
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss").Replace("-", f);
    }

    public static string DefaultDateFormat = "yyyy-MM-dd";
    public static string DefaultTimeFormat = "yyyy-MM-dd HH:mm:ss";
    public static string DefaultMilliFormat = "yyyy-MM-dd HH:mm:ss.fff";

    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">前半段分隔符,'-' </param>
    /// <param name="e">后半段分隔符,'-' </param>
    public static string ToDefaultTimeString(this DateTime time, string f, string e)
    {
        var map = new Dictionary<string, string> { { "-", f }, { ":", e } };
        return new Regex(@"(-)|(:)").Replace(time.ToString($"yyyy-MM-dd HH:mm:ss"), m => map[m.Value]);
    }


    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss.fff)
    /// </summary>
    /// <param name="time"></param>
    public static string ToDefaultMilliString(this DateTime time)
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss.fff");
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss.fff)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">前段的分隔符,'-' </param>
    public static string ToDefaultMilliString(this DateTime time, string f)
    {
        return time.ToString($"yyyy-MM-dd HH:mm:ss.fff").Replace("-", f);
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss.fff)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">前段的分隔符,'-' </param>
    /// <param name="m">中段的分隔符,':' </param>
    public static string ToDefaultMilliString(this DateTime time, string f, string m)
    {
        var map = new Dictionary<string, string> { { "-", f }, { ":", m } };
        return new Regex(@"(-)|(:)").Replace(time.ToString($"yyyy-MM-dd HH:mm:ss.fff"), m => map[m.Value]);
    }
    /// <summary>
    /// 转为默认时间格式字符串(yyyy-MM-dd HH:mm:ss.fff)
    /// </summary>
    /// <param name="time"></param>
    /// <param name="f">前段的分隔符,'-' </param>
    /// <param name="m">中段的分隔符,':' </param>
    /// <param name="e">末尾的分隔符,'.'</param>
    public static string ToDefaultMilliString(this DateTime time, string f, string m, string e)
    {
        var map = new Dictionary<string, string> { { "-", f }, { ":", m }, { ".", e } };
        return new Regex(@"(-)|(:)|(\.)").Replace(time.ToString($"yyyy-MM-dd HH:mm:ss.fff"), m => map[m.Value]);
    }

    public static IEnumerable<DateTime> CreateDates(this (DateTime start, DateTime end) range, DateTimeType? type = DateTimeType.Day)
    {
        if (range.start > range.end)
            range = (range.end, range.start);
        var (start, end) = range;
        var timediff = end - start;
        var dates = type switch
        {
            DateTimeType.Year => GetDates(start.AddYears, end.Year - start.Year),
            DateTimeType.Month => GetDates(start.AddMonths, (end.Year - start.Year) * 12 + (end.Month - start.Month) - (end.Day < start.Day ? 1 : 0)),
            DateTimeType.Day => GetDates(num => start.AddDays(num), timediff.TotalDays),
            DateTimeType.Hour => GetDates(num => start.AddHours(num), timediff.TotalHours),
            DateTimeType.Minute => GetDates(num => start.AddMinutes(num), timediff.TotalMinutes),
            DateTimeType.Second => GetDates(num => start.AddSeconds(num), timediff.TotalSeconds),
            _ => Enumerable.Empty<DateTime>()
        };

        return dates;

        static IEnumerable<DateTime> GetDates(Func<int, DateTime> function, double count)
        {
            return Enumerable.Range(0, (int)(count + 1)).Select(num => function(num));
        }
    }
}
