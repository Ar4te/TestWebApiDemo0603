using FluentFTP;
using Microsoft.AspNetCore.Http;
using System.Net;
using static System.Net.WebRequestMethods;

namespace Extensions.Helper;

public class FtpHelper
{
    /// <summary>
    /// 服务器地址
    /// </summary>
    public static readonly string strServer = "";
    /// <summary>
    /// 用户名
    /// </summary>
    public static readonly string strUser = "";
    /// <summary>
    /// 密码
    /// </summary>
    public static readonly string strPassword = "";
    /// <summary>
    /// 端口号
    /// </summary>
    public static readonly int port = 0;
    /// <summary>
    /// 文件夹
    /// </summary>
    public static readonly string fileName = "";


    public async static void FtpUpload(IFormFile file)
    {
        try
        {
            if (file is null) return;
            var stream = file.OpenReadStream();
            using FtpClient ftp = new()
            {
                Host = strServer,
                Port = port,
                Credentials = new NetworkCredential(strUser, strPassword),
            };
            ftp.Connect();
            // 查询所有文件夹名称
            string[] str = ftp.GetNameListing();
            if (!str.Contains($"/{fileName}"))
                ftp.CreateDirectory($"./{fileName}");

            // 向服务器写入指定文件
            using var ftpStream = ftp.OpenWrite($"/{fileName}/{file.FileName}");
            stream.CopyTo(ftpStream);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
