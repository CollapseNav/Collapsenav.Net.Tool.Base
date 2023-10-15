using System.Security.Cryptography;
namespace Collapsenav.Net.Tool;
/// <summary>
/// Md5工具
/// </summary>
public partial class MD5Tool : DefaultAlgorithm<MD5>
{
    static MD5Tool() => Algorithm ??= MD5.Create();
    /// <summary>
    /// 解密
    /// </summary>
    public static string Decrypt(string md5)
    {
        throw new Exception("Are you kidding ?");
    }
    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(string msg)
    {
        var result = Algorithm!.ComputeHash(msg.ToBytes());
        return BitConverter.ToString(result).Replace("-", "");
    }

    /// <summary>
    /// 加密
    /// </summary>
    public static string Encrypt(Stream stream)
    {
        stream.SeekToOrigin();
        var result = Algorithm!.ComputeHash(stream);
        stream.SeekToOrigin();
        return BitConverter.ToString(result).Replace("-", "");
    }
}
