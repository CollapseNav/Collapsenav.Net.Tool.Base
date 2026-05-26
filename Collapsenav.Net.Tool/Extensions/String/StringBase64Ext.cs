namespace Collapsenav.Net.Tool;
public static partial class StringExt
{
    /// <summary>
    /// bytes 转为 base64字符串
    /// </summary>
    public static string ToBase64(this byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }
    /// <summary>
    /// string 转为 base64字符串
    /// </summary>
    public static string ToBase64(this string origin)
    {
        return origin.ToBytes().ToBase64();
    }
    /// <summary>
    /// 流转为base64字符串
    /// </summary>
    public static string ToBase64(this Stream stream)
    {
        return stream.ToBytes().ToBase64();
    }

    /// <summary>
    /// base64字符串 转为 bytes
    /// </summary>
    public static byte[] FromBase64(this string base64String)
    {
        return Convert.FromBase64String(base64String);
    }
    /// <summary>
    /// base64字符串 转为 bytes
    /// </summary>
    public static string FromBase64ToString(this string base64String)
    {
        return base64String.FromBase64().BytesToString();
    }

    /// <summary>
    /// 将流内容作为图片转为 data:image/base64 格式（假设流内容为图片数据）
    /// </summary>
    public static string ImageToBase64(this Stream stream)
    {
        return "data:image;base64," + stream.ToBase64();
    }

    /// <summary>
    /// 读取图片文件并转为 data:image/base64 格式
    /// </summary>
    public static string ImageToBase64(this string filepath)
    {
        using var fs = filepath.OpenReadShareStream();
        return "data:image;base64," + fs.ToBase64();
    }

    /// <summary>
    /// 从 data:image/base64 字符串中提取纯 base64 部分
    /// </summary>
    public static string Base64ImageToString(this string base64String)
    {
        return base64String[(base64String.IndexOf("base64,") + "base64,".Length)..].Trim();
    }

    /// <summary>
    /// 将 data:image/base64 字符串转为 Stream
    /// </summary>
    public static Stream Base64ImageToStream(this string base64String)
    {
        return base64String.Base64ImageToString().FromBase64().ToStream();
    }
    /// <summary>
    /// 将 data:image/base64 保存到文件
    /// </summary>
    public static void Base64ImageSaveTo(this string base64String, string filepath)
    {
        base64String.Base64ImageToString().FromBase64().SaveTo(filepath);
    }

    /// <summary>
    /// 将 data:image/base64 保存到文件（异步）
    /// </summary>
    public static async Task Base64ImageSaveToAsync(this string base64String, string filepath)
    {
        await base64String.Base64ImageToString().FromBase64().SaveToAsync(filepath);
    }
}