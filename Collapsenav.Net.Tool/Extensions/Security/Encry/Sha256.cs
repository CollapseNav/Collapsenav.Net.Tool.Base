using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;
/// <summary>
/// Sha256工具
/// </summary>
public partial class Sha256Tool : DefaultAlgorithm<SHA256>
{
    static Sha256Tool() => Algorithm ??= SHA256.Create();
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
