using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window
{
    //引用GameObject
    public GameObject GameObject { get; set; }

    //引用Transform
    public Transform Transform { get; set; }

    //名字
    public string Name { get; set; }

    //是否从resources加载
    public bool Resource { get; set; } = false;
    //是否热更
    public bool IsHotFix { get; set; } = false;
    //热更类名 
    public string HotFixClassName { get; set; }

    //所有的Button
    protected List<Button> m_AllButton = new List<Button>();

    //所有Toggle
    protected List<Toggle> m_AllToggle = new List<Toggle>();

    public virtual bool OnMessage(UIMsgID msgID, params object[] paralist)
    {
        return true;
    }

    public virtual void Awake(object param1 = null,object param2 = null,object param3 = null)
    {
    }

    public virtual void OnShow(object param1 = null,object param2 = null,object param3 = null)
    {
    }

    public virtual void OnDisable()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnClose()
    {
        RemoveAllButtonListener();
        RemoveAllToggleListener();
        m_AllButton.Clear();
        m_AllToggle.Clear();
    }

    /// <summary>
    /// 同步替换图片
    /// </summary>
    /// <param name="path"></param>
    /// <param name="image"></param>
    /// <param name="setNativeSize"></param>
    /// <returns></returns>
    public bool ChangeImageSprite(string path, Image image, bool setNativeSize = false)
    {
        if (image == null)
            return false;

        Sprite sp = ResourceManager.Instance.LoadResource<Sprite>(path);
        if (sp != null)
        {
            if (image.sprite != null)
                image.sprite = null;

            image.sprite = sp;
            if (setNativeSize)
            {
                image.SetNativeSize();
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// 异步替换图片
    /// </summary>
    /// <param name="path"></param>
    /// <param name="image"></param>
    /// <param name="setNativeSize"></param>
    public void ChangImageSpriteAsync(string path, Image image, bool setNativeSize = false)
    {
        if (image == null)
            return;

        ResourceManager.Instance.AsyncLoadResource(path, OnLoadSpriteFinish, LoadResPriority.RES_MIDDLE, true, image,
            setNativeSize);
    }

    /// <summary>
    /// 图片加载完成
    /// </summary>
    /// <param name="path"></param>
    /// <param name="obj"></param>
    /// <param name="param1"></param>
    /// <param name="param2"></param>
    /// <param name="param3"></param>
    void OnLoadSpriteFinish(string path, Object obj, object param1 = null, object param2 = null, object param3 = null)
    {
        if (obj != null)
        {
            Sprite sp = obj as Sprite;
            Image image = param1 as Image;
            bool setNativeSize = (bool) param2;
            if (image.sprite != null)
                image.sprite = null;

            image.sprite = sp;
            if (setNativeSize)
            {
                image.SetNativeSize();
            }
        }
    }

    /// <summary>
    /// 移除所有的button事件
    /// </summary>
    public void RemoveAllButtonListener()
    {
        foreach (Button btn in m_AllButton)
        {
            btn.onClick.RemoveAllListeners();
        }
    }

    /// <summary>
    /// 移除所有的toggle事件
    /// </summary>
    public void RemoveAllToggleListener()
    {
        foreach (Toggle toggle in m_AllToggle)
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }

    /// <summary>
    /// 添加button事件监听
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="action"></param>
    public void AddButtonClickListener(Button btn, UnityEngine.Events.UnityAction action)
    {
        if (btn != null)
        {
            if (!m_AllButton.Contains(btn))
            {
                m_AllButton.Add(btn);
            }

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(action);
            btn.onClick.AddListener(BtnPlaySound);
        }
    }

    /// <summary>
    /// Toggle事件监听
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="action"></param>
    public void AddToggleClickListener(Toggle toggle, UnityEngine.Events.UnityAction<bool> action)
    {
        if (toggle != null)
        {
            if (!m_AllToggle.Contains(toggle))
            {
                m_AllToggle.Add(toggle);
            }

            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener(action);
            toggle.onValueChanged.AddListener(TogglePlaySound);
        }
    }

    /// <summary>
    /// 播放button声音
    /// </summary>
    void BtnPlaySound()
    {
    }

    /// <summary>
    /// 播放toggle声音
    /// </summary>
    /// <param name="isOn"></param>
    void TogglePlaySound(bool isOn)
    {
    }
}