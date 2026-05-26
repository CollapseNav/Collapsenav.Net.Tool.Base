using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace Collapsenav.Net.Tool;

public partial class StringExt
{
    /// <summary>
    /// 是否为 null
    /// </summary>
    public static bool IsNull(
#if (NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER)
        [NotNullWhen(false)]
#endif
        this string? str) => str is null;
    /// <summary>
    /// 若为 null 则返回 value
    /// </summary>
    public static string IsNull(this string? str, string value) => str.IsNull() ? value : str;
    /// <summary>
    /// 是否为空字符串
    /// </summary>
    public static bool IsEmpty(
#if (NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER)
        [NotNullWhen(false)]
#endif
        this string? str) => str == string.Empty;
    /// <summary>
    /// 若为空字符串则返回 value
    /// </summary>
    public static string IsEmpty(this string? str, string? value) => str.IsEmpty() ? (value ?? string.Empty) : str;
    /// <summary>
    /// 不为 null（可能为空字符串）
    /// </summary>
    public static bool NotNull(
#if (NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER)
        [NotNullWhen(true)]
#endif
        this string? str) => str is not null;
    /// <summary>
    /// 不为空（null 或空字符串都算空）
    /// </summary>
    public static bool NotEmpty(
#if (NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER)
        [NotNullWhen(true)]
#endif
        this string? str) => !string.IsNullOrEmpty(str);
    /// <summary>
    /// 是空白字符串
    /// </summary>
    public static bool IsBlank(
#if (NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER)
        [NotNullWhen(false)]
#endif
        this string? str) => string.IsNullOrWhiteSpace(str);
    /// <summary>
    /// 不是空白字符串
    /// </summary>
    public static bool NotBlank(
#if (NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER)
        [NotNullWhen(true)]
#endif
        this string? str) => !str.IsBlank();
    /// <summary>
    /// 若是空白字符串则返回value
    /// </summary>
    public static string? IsBlank(this string? str, string? value) => str.IsBlank() ? (value ?? string.Empty) : str;
    /// <summary>
    /// 全包含
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    /// <param name="ignoreCase">是否忽略大小写(默认不忽略)</param>
    public static bool ContainsAll(this string? origin, IEnumerable<string> keys, bool ignoreCase = false)
    {
        if (origin.IsNull())
            return false;
#if NETSTANDARD2_0
        return keys.All(key => origin.ToLower().Contains(key.ToLower()));
#else
        return keys.All(key => origin.Contains(key, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
#endif
    }
    /// <summary>
    /// 部分包含
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    /// <param name="ignoreCase">是否忽略大小写(默认不忽略)</param>
    public static bool ContainsAny(this string? origin, IEnumerable<string> keys, bool ignoreCase = false)
    {
        if (origin.IsNull())
            return false;
#if NETSTANDARD2_0
        return keys.Any(key => origin.ToLower().Contains(key.ToLower()));
#else
        return keys.Any(key => origin.Contains(key, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
#endif
    }
    /// <summary>
    /// 全包含(不忽略大小写)
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    public static bool ContainsAll(this string? origin, params string[] keys)
    {
        if (origin.IsNull())
            return false;
        return keys.All(key => origin.Contains(key));
    }
    /// <summary>
    /// 部分包含(不忽略大小写)
    /// </summary>
    /// <param name="origin">源字符串</param>
    /// <param name="keys"></param>
    public static bool ContainsAny(this string? origin, params string[] keys)
    {
        if (origin.IsNull())
            return false;
        return keys.Where(key => origin.Contains(key)).Any();
    }

    /// <summary>
    /// 检查是否邮箱格式
    /// </summary>
    public static bool IsEmail(this string? email)
    {
        if (email.IsNull())
            return false;
        return Regex.IsMatch(email, "^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+.[a-zA-Z0-9]+$");
    }
    /// <summary>
    /// 检查是否 Url 格式，check=true 时会发起网络 Ping 请求验证域名可达性
    /// </summary>
    public static bool IsUrl(this string? url, bool check = false)
    {
        if (url.IsNull())
            return false;
        // TODO 日后写正则判断...
        var isHttp = url.HasStartsWith("https://", "http://");
        if (!isHttp) return false;
        // ping测试
        if (check && !url.GetDomain().CanPing()) return false;
        return true;
    }
    /// <summary>
    /// 检查域名是否能 Ping 通（发起网络 ICMP 请求）
    /// </summary>
    public static bool CanPing(this string? domain, int timeout = 1000)
    {
        if (domain.IsNull())
            return false;
        Ping pingObj = new();
        if (domain.IsUrl())
            domain = domain.GetDomain();
        var reply = pingObj.Send(domain, timeout);
        return reply.Status == IPStatus.Success;
    }
}
