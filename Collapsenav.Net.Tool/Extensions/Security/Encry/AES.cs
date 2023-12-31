using System.Security.Cryptography;

namespace Collapsenav.Net.Tool;
/// <summary>
/// Aes工具
/// </summary>
public partial class AESTool : WithKeyAndIVAlgorithm<Aes>
{
    static AESTool() => Algorithm ??= Aes.Create();
    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="secmsg">密文</param>
    /// <param name="key">key</param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <param name="iv">向量</param>
    /// <param name="level">加密级别</param>
    public static string Decrypt(string secmsg, string key = DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = DefaultIV, int level = 32)
    {
        var decryptMsg = secmsg.FromBase64();
        Algorithm!.Mode = mode;
        Algorithm.Padding = padding;
        using var decrypt = Algorithm.CreateDecryptor(GetAesBytes(key, level), GetAesBytes(iv, 16));
        var result = decrypt.TransformFinalBlock(decryptMsg, 0, decryptMsg.Length);
        return result.BytesToString();
    }
    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="msg">原文</param>
    /// <param name="key">key</param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <param name="iv">向量</param>
    /// <param name="level">加密级别</param>
    public static string Encrypt(string msg, string key = DefaultKey, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7, string iv = DefaultIV, int level = 32)
    {
        Algorithm!.Mode = mode;
        Algorithm.Padding = padding;
        using var encrypt = Algorithm.CreateEncryptor(GetAesBytes(key, level), GetAesBytes(iv, 16));
        var result = encrypt.TransformFinalBlock(msg.ToBytes(), 0, msg.Length);
        return result.ToBase64();
    }

    public static byte[] GetAesBytes(string key, int level = 32)
    {
        if (key.Length < level)
            key = key.PadLeft(level, '#');
        return key.First(level).ToBytes();
    }
    public static byte[] GetAesBytes(byte[] key, int level = 32)
    {
        if (key.Length < level)
            key = new string('#', key.Length).ToBytes().Concat(key).ToArray();
        return key.Take(level).ToArray();
    }
}
