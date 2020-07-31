using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotFixUI : Window
{
    private HotFixPanel m_Panel;
    private float m_SumTime = 0;

    public override void Awake(object param1 = null, object param2 = null, object param3 = null)
    {
        m_SumTime = 0;
        //持有面板
        m_Panel = GameObject.GetComponent<HotFixPanel>();
        //进度设置为0
        m_Panel.m_Image.fillAmount = 0;
        // 文本提示框
        m_Panel.m_Text.text = string.Format("{0:F}M/S", 0);

        //回调
        HotPatchManager.Instance.ServerInfoError += ServerInfoError;
        HotPatchManager.Instance.ItemError += ItemError;
#if UNITY_EDITOR
        StartOnFinish();
#else
        if (HotPatchManager.Instance.ComputeUnPackFile())
        {
            m_Panel.m_SliderTopText.text = "解压中...";

            HotPatchManager.Instance.StartUnackFile (() =>
            {
                m_SumTime = 0;
                HotFix();
            });
        }
        else
        {
            HotFix();
        }
#endif
    }

    private void HotFix()
    {
        //检查网络
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //网络异常
            GameStart.OpenCommonConfirm("网络连接失败", "网络连接失败,请检查网络是否正常.", Application.Quit, Application.Quit);
        }
        else
        {
            CheckVersion();
        }

        //检查热更版本
    }

    /// <summary>
    /// 点击确认
    /// </summary>
    void OnClickStartDownload()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                GameStart.OpenCommonConfirm("确认下载", "现在使用的是数据流量，是否继续下载?", StartDownload, OnClickCancelDownload);
            }
        }
        else
        {
            StartDownload();
        }
    }

    /// <summary>
    /// 点击取消
    /// </summary>
    void OnClickCancelDownload()
    {
        Application.Quit();
    }

    /// <summary>
    /// 开始下载
    /// </summary>
    void StartDownload()
    {
        m_Panel.m_SliderTopText.text = "下载中...";
        m_Panel.m_InfoPanel.SetActive(true);
        m_Panel.m_hotContentText.text = HotPatchManager.Instance.CurrentPatches.Des;

        GameStart.Instance.StartCoroutine(HotPatchManager.Instance.StartDownLoadAB(StartOnFinish));
    }

    /// <summary>
    /// 下载完成回调，或者没有下载的东西直接进入游戏
    /// </summary>
    void StartOnFinish()
    {
        //开启携程
        GameStart.Instance.StartCoroutine(OnFinish());
    }

    /// <summary>
    /// 下载完调用，回调
    /// </summary>
    /// <returns></returns>
    IEnumerator OnFinish()
    {
        
        yield return GameStart.Instance.StartCoroutine(GameStart.Instance.StartGame(m_Panel.m_Image,
            m_Panel.m_SliderTopText));
        UIManager.Instance.CloseWnd(this);
    }

    public override void OnUpdate()
    {
        if (HotPatchManager.Instance.StartUnPack)
        {
            m_SumTime += Time.deltaTime;
            m_Panel.m_Image.fillAmount = HotPatchManager.Instance.GetUnpackProgress();
            float speed = (HotPatchManager.Instance.AlreadyUnPackSize / 1024.0f) / m_SumTime;
            m_Panel.m_Text.text = string.Format("{0:F} M/S", speed);
        }

        //如果正在下载，计算速度
        if (HotPatchManager.Instance.StartDownload)
        {
            m_SumTime += Time.deltaTime;
            m_Panel.m_Image.fillAmount = HotPatchManager.Instance.GetProgress();
            float speed = (HotPatchManager.Instance.GetLoadSize() / 1024.0f) / m_SumTime;
            m_Panel.m_Text.text = string.Format("{0:F} M/S", speed);
        }
    }

    private void CheckVersion()
    {
        HotPatchManager.Instance.CheckVersion(hot =>
        {
            if (hot)
            {
                //提示玩家是否确定热更下载
                GameStart.OpenCommonConfirm("热更确定",
                    string.Format("当前版本为{0},有{1:F}M大小的热更包,是否确定下载?", HotPatchManager.Instance.CurVersion,
                        HotPatchManager.Instance.LoadSumSize / 1024.0f), OnClickStartDownload, OnClickCancelDownload);
            }
            else
            {
                StartOnFinish();
            }
        });
    }


    /// <summary>
    /// 重新下载逻辑
    /// </summary>
    void ANewDownload()
    {
        HotPatchManager.Instance.CheckVersion(hot =>
        {
            if (hot)
            {
                StartDownload();
            }
        });
    }

    /// <summary>
    /// 服务器错误
    /// </summary>
    private void ServerInfoError()
    {
        GameStart.OpenCommonConfirm("服务器列表获取失败", "服务器内容获取失败，请检查网络连接是否正常，尝试重新下载？", CheckVersion, Application.Quit);
    }

    /// <summary>
    /// 下载错误列表
    /// </summary>
    /// <param name="all"></param>
    private void ItemError(string all)
    {
        GameStart.OpenCommonConfirm("资源下载失败", string.Format("{0}等资源下载失败，请重新尝试下载", all), ANewDownload, Application.Quit);
    }

    public override void OnClose()
    {
        HotPatchManager.Instance.ServerInfoError -= ServerInfoError;
        HotPatchManager.Instance.ItemError -= ItemError;
        //加载场景
        GameMapManager.Instance.LoadScene(ConStr.MENUSCENE);
    }
}