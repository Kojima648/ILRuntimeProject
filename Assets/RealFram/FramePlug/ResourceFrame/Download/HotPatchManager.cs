using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class HotPatchManager : Singleton<HotPatchManager>
{
    private MonoBehaviour _monoBehaviour;

    //下载路径
    private string m_DownloadPath = Application.persistentDataPath + "/Download";

    //当前版本和当前的报名
    private string m_CurrentVerion;
    public string m_CurrentPackageName;
    private string m_ServerXmlPath = Application.persistentDataPath + "/ServerInfo.xml";
    private string m_LocalXmlPath = Application.persistentDataPath + "/LocalInfo.xml";
    private ServerInfo m_serverInfo;
    private ServerInfo m_LocalInfo;

    private VersionInfo m_GameVersion;

    //当前热更Patches
    private Patches m_CurrentPatches;

    //所有热更的东西
    Dictionary<string, Patch> m_HotFixDic = new Dictionary<string, Patch>();

    //所以需要下载的东西
    List<Patch> m_DownloadList = new List<Patch>();

    //所有需要下载的东西的Dic
    Dictionary<string, Patch> m_DownloadDic = new Dictionary<string, Patch>();

    /// <summary>
    /// 需要下载的资源总个数
    /// </summary>
    public int LoadFileCount { get; set; } = 0;

    /// <summary>
    /// 需要下载资源的大小
    /// </summary>
    public float LoadSumSize { get; set; } = 0;

    //需要mono来开启携程
    public void Init(MonoBehaviour mono)
    {
        _monoBehaviour = mono;
    }

    /// <summary>
    /// 版本检测
    /// </summary>
    /// <param name="hotCallBack"></param>
    public void CheckVersion(Action<bool> hotCallBack = null)
    {
        //每次检测版本前，清理
        m_HotFixDic.Clear();
        ReadVersion();
        _monoBehaviour.StartCoroutine(ReadXml(() =>
        {
            //判断反序列化是否成功
            if (m_serverInfo == null)
            {
                //临时处理
                if (hotCallBack != null)
                {
                    hotCallBack(false);
                }

                return;
            }

            //读取所有的游戏版本
            foreach (VersionInfo version in m_serverInfo.GameVersion)
            {
                //找到等于当前版本的Version
                if (version.Version == m_CurrentVerion)
                {
                    m_GameVersion = version;
                    break;
                }
            }

            //获取到热更的AB包（获取到当前游戏版本之后调用）
            GetHotAB();

            //计算下载的资源
            ComputeDownload();
            //获取下载的资源列表
            LoadFileCount = m_DownloadList.Count;
            //获取下载的资源大小
            LoadSumSize = m_DownloadList.Sum(x => x.Size);

            if (hotCallBack != null)
            {
                hotCallBack(m_DownloadList.Count > 0);
            }
        }));
    }

    bool CheckLocalAndServerPatch()
    {
        if (!File.Exists(m_LocalXmlPath))
            return true;

        //反序列化，服务器xml存到本地(Copy)
        m_LocalInfo = BinarySerializeOpt.XmlDeserialize(m_LocalXmlPath, typeof(ServerInfo)) as ServerInfo;

        VersionInfo localGameVersion = null;
        if (m_LocalInfo != null)
        {
            foreach (VersionInfo version in m_LocalInfo.GameVersion)
            {
                //如果版本等于游戏当前版本
                if (version.Version == m_CurrentVerion)
                {
                    localGameVersion = version;
                    break;
                }
            }
        }

        if (localGameVersion != null && localGameVersion.Patches != null && m_GameVersion.Patches != null &&
            m_GameVersion.Patches.Length > 0 &&
            m_GameVersion.Patches[m_GameVersion.Patches.Length - 1].Version !=
            localGameVersion.Patches[localGameVersion.Patches.Length - 1].Version)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// //读当前版本的Version
    /// </summary>
    public void ReadVersion()
    {
        TextAsset versionText = Resources.Load<TextAsset>("Version");
        if (versionText == null)
        {
            Debug.LogError("未读到resources下的version文件");
            return;
        }

        string[] all = versionText.text.Split('\n');
        if (all.Length > 0)
        {
            string[] infoList = all[0].Split(';');
            if (infoList.Length >= 2)
            {
                m_CurrentVerion = infoList[0].Split('|')[1];
                m_CurrentPackageName = infoList[1].Split('|')[1];
            }
        }
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="callBack">回调</param>
    /// <returns></returns>
    IEnumerator ReadXml(Action callBack)
    {
        string xmlUrl = "http://127.0.0.1/hotfix/ServerInfo.xml";
        UnityWebRequest webRequest = UnityWebRequest.Get(xmlUrl);
        webRequest.timeout = 30;
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError)
        {
            Debug.Log("Download Error" + webRequest.error);
        }
        else
        {
            FileTool.CreateFile(m_ServerXmlPath, webRequest.downloadHandler.data);
            if (File.Exists(m_ServerXmlPath))
            {
                m_serverInfo = BinarySerializeOpt.XmlDeserialize(m_ServerXmlPath, typeof(ServerInfo)) as ServerInfo;
            }
            else
            {
                Debug.LogError("热更配置读取错误");
            }
        }
    }

    /// <summary>
    /// 获取所有热更包信息
    /// </summary>
    void GetHotAB()
    {
        if (m_GameVersion != null && m_GameVersion.Patches != null && m_GameVersion.Patches.Length > 0)
        {
            //最后一次的热更新 
            Patches lastPatches = m_GameVersion.Patches[m_GameVersion.Patches.Length - 1];
            //热更包不能为空
            if (lastPatches != null && lastPatches.Files != null)
            {
                //把热更文件添加在字典中
                foreach (Patch patch in lastPatches.Files)
                {
                    //所有需要热更的文件
                    m_HotFixDic.Add(patch.Name, patch);
                }
            }
        }
    }

    /// <summary>
    /// 计算下载的资源
    /// </summary>
    void ComputeDownload()
    {
        m_DownloadList.Clear();
        m_DownloadDic.Clear();
        if (m_GameVersion != null && m_GameVersion.Patches != null && m_GameVersion.Patches.Length > 0)
        {
            m_CurrentPatches = m_GameVersion.Patches[m_GameVersion.Patches.Length - 1];
            if (m_CurrentPatches.Files != null && m_CurrentPatches.Files.Count > 0)
            {
                foreach (Patch currentPatch in m_CurrentPatches.Files)
                {
                    if (Application.platform == RuntimePlatform.WindowsPlayer ||
                        Application.platform == RuntimePlatform.WindowsEditor &&
                        currentPatch.Platform.Contains("StandaloneWindows64"))
                    {
                        AddDownloadList(currentPatch);
                    }
                    else if (Application.platform == RuntimePlatform.Android ||
                             Application.platform == RuntimePlatform.WindowsEditor ||
                             currentPatch.Platform.Contains("Android"))
                    {
                        AddDownloadList(currentPatch);
                    }
                    else if (Application.platform == RuntimePlatform.IPhonePlayer ||
                             Application.platform == RuntimePlatform.WindowsEditor ||
                             currentPatch.Platform.Contains("IOS"))
                    {
                        AddDownloadList(currentPatch);
                    }
                }
            }
        }
    }

    void AddDownloadList(Patch patch)
    {
        string filePath = m_DownloadPath + "/" + patch.Name;
        //如果存在 就对比（下载下来的与服务器的MD5做对比,不一致就放到下载队列，如果不存在就直接下载）
        if (File.Exists(filePath))
        {
            string md5 = MD5Manager.Instance.BuildFileMd5(filePath);
            //文件被外部更改
            if (patch.MD5 != md5)
            {
                m_DownloadList.Add(patch);
                m_DownloadDic.Add(patch.Name, patch);
            }
        }
        else
        {
            m_DownloadList.Add(patch);
            m_DownloadDic.Add(patch.Name, patch);
        }
    }
}

public class FileTool
{
    /// <summary>
    /// 创建文件
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="bytes">字节数组</param>
    public static void CreateFile(string filePath, byte[] bytes)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        FileInfo file = new FileInfo(filePath);
        Stream stream = file.Create();
        stream.Write(bytes, 0, bytes.Length);
    }
}