using FluentFTP;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Extensions.Helper;

public class FtpHelper
{
    /// <summary>
    /// 服务器地址
    /// </summary>
    public static readonly string strServer = AppSettings.app(new string[] { "FtpConfig", "SerUrl" });
    /// <summary>
    /// 用户名
    /// </summary>
    public static readonly string strUser = AppSettings.app(new string[] { "FtpConfig", "UserNo" });
    /// <summary>
    /// 密码
    /// </summary>
    public static readonly string strPassword = AppSettings.app(new string[] { "FtpConfig", "Pwd" });
    /// <summary>
    /// 端口号
    /// </summary>
    public static readonly int port = Convert.ToInt32(AppSettings.app(new string[] { "FtpConfig", "Port" }));

    public static readonly string pathPrefix = AppSettings.app(new string[] { "FtpConfig", "SavePathPrefix" });

    public static Tuple<string, byte[], bool> FtpUpload(IFormFile file, string fileName)
    {
        try
        {
            if (file is null) return new("未选择文件", null, false);
            if (string.IsNullOrEmpty(fileName)) return new("未明确文件存储路径", null, false);
            string folderName = string.IsNullOrEmpty(pathPrefix) ? fileName : $"{pathPrefix}/{fileName}";
            var stream = file.OpenReadStream();
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer);
            using FtpClient ftp = new()
            {
                Host = strServer,
                Port = port,
                Credentials = new NetworkCredential(strUser, strPassword),
            };
            ftp.Connect();
            // 查询所有文件夹名称
            string[] str = ftp.GetNameListing();
            if (!str.Contains($"/{folderName}"))
                ftp.CreateDirectory($"./{folderName}");

            // 向服务器写入指定文件
            using var ftpStream = ftp.OpenWrite($"/{folderName}/{file.FileName}");
            stream.CopyTo(ftpStream);
            return new("", buffer, true);
        }
        catch (Exception ex)
        {
            return new(ex.Message, null, false);
        }
    }
}
