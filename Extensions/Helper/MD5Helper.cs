using System.Security.Cryptography;
using System.Text;

namespace Extensions.Helper;

public class MD5Helper
{
    /// <summary>
    /// 16位MD5加密
    /// </summary>
    /// <param name="orignStr"></param>
    /// <returns></returns>
    public static string MD5Encrypt16(string orignStr)
    {
        var md5 = MD5.Create();
        var _encryptStr = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(orignStr)), 4, 8);
        _encryptStr = _encryptStr.Replace("-", string.Empty);
        return _encryptStr;
    }

    /// <summary>
    /// 32位MD5加密
    /// </summary>
    /// <param name="orignStr"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string MD5Encrypt32(string orignStr)
    {
        var _encryptStr = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(orignStr) && !string.IsNullOrWhiteSpace(orignStr))
            {
                var md5 = MD5.Create();
                byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(orignStr));
                foreach (var item in s)
                {
                    _encryptStr = string.Concat(_encryptStr, item.ToString("x2"));
                }
            }
        }
        catch
        {
            throw new Exception($"错误的字符串：{orignStr}");
        }
        return _encryptStr;
    }

    /// <summary>
    /// 64位MD5加密
    /// </summary>
    /// <param name="orignStr"></param>
    /// <returns></returns>
    public static string MD5Encrypt64(string orignStr)
    {
        var md5 = MD5.Create();
        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(orignStr));
        return Convert.ToBase64String(s);
    }

    /// <summary>
    /// SHA1加密
    /// </summary>
    /// <param name="orignStr"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string Sha1Encrypt(string orignStr, string format = "x2")
    {
        var buffer = Encoding.UTF8.GetBytes(orignStr);
        var data = SHA1.Create().ComputeHash(buffer);
        var sb = new StringBuilder();
        foreach (var t in data)
        {
            sb.Append(t.ToString(format));
        }
        return sb.ToString();
    }

    /// <summary>
    /// SHA256加密
    /// </summary>
    /// <param name="orignStr"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string Sha256Encrypt(string orignStr, string format = "x2")
    {
        var buffer = Encoding.UTF8.GetBytes(orignStr);
        var data = SHA256.Create().ComputeHash(buffer);
        var sb = new StringBuilder();
        foreach (var t in data)
        {
            sb.Append(t.ToString(format));
        }
        return sb.ToString();
    }
}
