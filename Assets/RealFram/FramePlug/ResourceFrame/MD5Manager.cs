using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
/// <summary>
/// https://www.cnblogs.com/CaomaoUnity3d/p/4782858.html
/// </summary>
public class MD5Manager : Singleton<MD5Manager>
{
    public String BuildFileMd5(String filename)
    {
        String filemd5 = null;
        try
        {
            using (var fileStream = File.OpenRead(filename))
            {
                var md5 = MD5.Create();
                var fileMD5Bytes = md5.ComputeHash(fileStream); //计算指定Stream 对象的哈希值                                     
                filemd5 = FormatMD5(fileMD5Bytes);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
        }

        return filemd5;
    }

    public static string FormatMD5(Byte[] data)
    {
        return System.BitConverter.ToString(data).Replace("-", "").ToLower(); //将byte[]装换成字符串
    }
}