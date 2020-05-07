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
    public string CurVersion => m_CurrentVerion;
    public string m_CurrentPackageName;
    private string m_ServerXmlPath = Application.persistentDataPath + "/ServerInfo.xml";
    private string m_LocalXmlPath = Application.persistentDataPath + "/LocalInfo.xml";
    private ServerInfo m_serverInfo;
    private ServerInfo m_LocalInfo;

    private VersionInfo m_GameVersion;

    //当前热更Patches
    private Patches m_CurrentPatches;

    public Patches CurrentPatches => m_CurrentPatches;
    //所有热更的东西
    Dictionary<string, Patch> m_HotFixDic = new Dictionary<string, Patch>();

    //所以需要下载的东西
    List<Patch> m_DownloadList = new List<Patch>();

    //所有需要下载的东西的Dic
    Dictionary<string, Patch> m_DownloadDic = new Dictionary<string, Patch>();

    //服务器上的资源名对应的MD5,用于下载后校验
    Dictionary<string, string> m_DownloadMD5Dic = new Dictionary<string, string>();

    //服务器列表获取错误回调
    public Action ServerInfoError;

    //文件下载出错回调
    public Action<string> ItemError;

    //文件下载完成回调
    public Action LoadOver;

    //储存已经下载的资源
    List<Patch> m_AlreadyDownList = new List<Patch>();

    //是否开始下载
    public bool StartDownload = false;

    //尝试重新下载次数
    private int m_TryDownCount = 0;

    private const int DOWNLOADCOUNT = 4;

    //当前正在下载的资源
    private DownloadAssetBundle m_curDownload = null;


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
    /// 计算AB包路径
    /// </summary>
    /// <returns></returns>
    public string ComputeABPath(string name)
    {
        Patch patch = null;
        m_HotFixDic.TryGetValue(name, out patch);
        //如果服务器包含这个热更
        if (patch != null)
        {
            //需要从下载的地方加载
            return m_DownloadPath + "/" + name;
        }

        return "";
    }

    /// <summary>
    /// 版本检测
    /// </summary>
    /// <param name="hotCallBack"></param>
    public void CheckVersion(Action<bool> hotCallBack = null)
    {
        m_TryDownCount = 0;
        //每次检测版本前，清理
        m_HotFixDic.Clear();
        ReadVersion();
        _monoBehaviour.StartCoroutine(ReadXml(() =>
        {
            //判断反序列化是否成功
            if (m_serverInfo == null)
            {
                //临时处理
                ServerInfoError?.Invoke();
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
            if (CheckLocalAndServerPatch())
            {
                //计算下载的资源
                ComputeDownload();
                if (File.Exists(m_ServerXmlPath))
                {
                    if (File.Exists(m_LocalXmlPath))
                    {
                        File.Delete(m_LocalXmlPath);
                    }

                    File.Move(m_ServerXmlPath, m_LocalXmlPath);
                }
            }
            else
            {
                ComputeDownload();
            }

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

    /// <summary>
    /// 检查本地资源，是否与服务器下载列表信息一致，主要用于在下载一半退出，再进入游戏，下载剩下部分
    /// </summary>
    void CheckLocalResources()
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

    /// <summary>
    /// 检查本地热更信息与服务器热更信息比较
    /// </summary>
    /// <returns></returns>
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

        callBack?.Invoke();
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
        m_DownloadMD5Dic.Clear();

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
                m_DownloadMD5Dic.Add(patch.Name, patch.MD5);
            }
        }
        else
        {
            m_DownloadList.Add(patch);
            m_DownloadDic.Add(patch.Name, patch);
            m_DownloadMD5Dic.Add(patch.Name, patch.MD5);
        }
    }

    /// <summary>
    /// 获取下载进度
    /// </summary>
    /// <returns></returns>
    public float GetProgress()
    {
        return GetLoadSize() / LoadSumSize;
    }

    /// <summary>
    /// 获取已经下载总大小
    /// </summary>
    /// <returns></returns>
    public float GetLoadSize()
    {
        //已经下载的资源进度
        float alreadySize = m_AlreadyDownList.Sum(x => x.Size);
        //当前已经下载的大小
        float curAlreadySize = 0;
        //当前不等于空，再计算
        if (m_curDownload != null)
        {
            Patch patch = FindPatchByGamePath(m_curDownload.FileName);
            if (patch != null && m_AlreadyDownList.Contains(patch))
            {
                //总大小
                curAlreadySize = m_curDownload.GetProcess() * patch.Size;
            }
        }

        return alreadySize + curAlreadySize;
    }

    public IEnumerator StartDownloadAB(Action callBack, List<Patch> allPatch = null)
    {
        m_AlreadyDownList.Clear();
        StartDownload = true;
        if (allPatch == null)
        {
            allPatch = m_DownloadList;
        }

        //如果下载文件夹不存在，则下载
        if (!Directory.Exists(m_DownloadPath))
        {
            Directory.CreateDirectory(m_DownloadPath);
        }

        //对象存储
        List<DownloadAssetBundle> downloadAssetBundles = new List<DownloadAssetBundle>();

        //遍历下载列表，添加进下载队列
        foreach (Patch patch in allPatch)
        {
            downloadAssetBundles.Add(new DownloadAssetBundle(patch.Url, m_DownloadPath));
        }

        //开始下载
        foreach (DownloadAssetBundle download in downloadAssetBundles)
        {
            //存储当前的下载文件以获取进度和大小
            m_curDownload = download;
            yield return _monoBehaviour.StartCoroutine(download.Download());
            Patch patch = FindPatchByGamePath(download.FileName);
            //不为空再添加进Dic
            if (patch != null)
            {
                m_AlreadyDownList.Add(patch);
            }

            download.Destory();
        }

        //如果校验没通过，自动重新下载没通过的文件，重复下载计数，达到一定次数后，客户端反馈
        VerifyMD5(downloadAssetBundles, callBack);
    }

    //MD5码的校验,根据下载的Patch里存储的md5

    void VerifyMD5(List<DownloadAssetBundle> downloadAssets, Action callBack)
    {
        List<Patch> downloadList = new List<Patch>();
        foreach (DownloadAssetBundle download in downloadAssets)
        {
            string md5 = "";
            //用之前下载的MD5DIC进行查找
            if (m_DownloadMD5Dic.TryGetValue(download.FileName, out md5))
            {
                if (MD5Manager.Instance.BuildFileMd5(download.SaveFilePath) != md5)
                {
                    Debug.Log(string.Format("此文件{0}md5校验失败，即将重新下载", download.FileName));
                    //获取到patch
                    Patch patch = FindPatchByGamePath(download.FileName);
                    //md5校验完，有不一样的放到新的Dic里 重新下载
                    if (patch != null)
                    {
                        downloadList.Add(patch);
                    }
                }
            }
        }

        //如果全部都正确
        if (downloadList.Count <= 0)
        {
            m_DownloadMD5Dic.Clear();
            if (callBack != null)
            {
                StartDownload = false;
                callBack();
            }

            //如果Loadover不为空，执行下载完成后的回调
            LoadOver?.Invoke();
        }
        else
        {
            //下载次数判断
            if (m_TryDownCount >= DOWNLOADCOUNT)
            {
                //获取到所有下载失败的文件
                string allName = "";
                StartDownload = false; //下载失败也相当于下载完成
                foreach (Patch patch in downloadList)
                {
                    allName += patch.Name + ";";
                }

                Debug.LogError("资源重复下载4次MD5校验都失败，请检查资源:" + allName);
                if (ItemError != null)
                {
                    //告诉外面回调，下载某些资源出错
                    ItemError(allName);
                }
            }
            else
            {
                //重复下载次数++
                m_TryDownCount++;
                //重复下载的DIC，clear掉，不再包含下载过的
                m_DownloadMD5Dic.Clear();
                foreach (Patch patch in downloadList)
                {
                    m_DownloadMD5Dic.Add(patch.Name, patch.MD5);
                }

                //自动重新下载校验失败的文件    
                _monoBehaviour.StartCoroutine(StartDownloadAB(callBack, downloadList));
            }
        }
    }

    /// <summary>
    /// 根据Download的文件名查找对象的热更Patch，再进行添加
    /// </summary>
    Patch FindPatchByGamePath(string name)
    {
        Patch patch = null;
        m_DownloadDic.TryGetValue(name, out patch);
        return patch;
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
        stream.Close();
        stream.Dispose();
    }
}