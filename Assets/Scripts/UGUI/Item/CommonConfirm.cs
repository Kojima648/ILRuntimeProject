using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommonConfirm : BaseItem
{
    public Text m_Title;
    public Text m_Des;
    public Button m_ConfirmBtn;
    public Button m_CancleBtn;

    public void Show(string title, string des, UnityAction confirmEvent, UnityAction cancleEvent)
    {
        m_Title.text = title;
        m_Des.text = des;
        AddButtonClickListener(m_ConfirmBtn, () =>
        {
            confirmEvent();
            Destroy(gameObject);
        });
        AddButtonClickListener(m_CancleBtn, () =>
        {
            cancleEvent();
            Destroy(gameObject);
        });
    }

    void Start()
    {
    }

    void Update()
    {
    }
}