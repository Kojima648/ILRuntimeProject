using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundle : DownloadItem
{
    //定义web请求
    private UnityWebRequest m_WebRequest;

    //构造函数
    public DownloadAssetBundle(string url, string path) : base(url, path)
    {
    }

    //重写download
    public override IEnumerator Download(Action callback = null)
    {
        //获取URL
        m_WebRequest = UnityWebRequest.Get(m_Url);
        //开始下载
        m_StartDownload = true;
        m_WebRequest.timeout = 30;
        //开始下载
        yield return m_WebRequest.SendWebRequest();
        m_StartDownload = false;
        //报错处理
        if (m_WebRequest.isNetworkError)
        {
            Debug.LogError("Download Error" + m_WebRequest.error);
        }
        else
        {
            byte[] bytes = m_WebRequest.downloadHandler.data;
            FileTool.CreateFile(m_SaveFilePath, bytes);
            callback?.Invoke();
        }
    }

    public override float GetProcess()
    {
        if (m_WebRequest != null)
        {
            return (long) m_WebRequest.downloadProgress;
        }

        return 0;
    }

    public override long GetCurrentLength()
    {
        if (m_WebRequest != null)
        {
            return (long) m_WebRequest.downloadedBytes;
        }

        return 0;
    }

    public override long GetLength()
    {
        return 0;
    }

    public override void Destory()
    {
        if (m_WebRequest != null)
        {
            m_WebRequest.Dispose();
            m_WebRequest = null;
        }
    }
}