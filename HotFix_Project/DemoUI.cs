using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix_Project
{
    class DemoUi : Window
    {
        private DemoPanel m_demoPanel;
        public override void Awake(object param1 = null, object param2 = null, object param3 = null)
        {
            m_demoPanel = GameObject.GetComponent<DemoPanel>();
            ResourceManager.Instance.AsyncLoadResource("Assets/GameData/UGUI/Test1.png", OnLoadSpriteTest1, LoadResPriority.RES_MIDDLE, true);

        }

        void OnLoadSpriteTest1(string path, Object obj, object param1 = null, object param2 = null, object param3 = null)
        {
            if (obj != null)
            {
                Sprite sp = obj as Sprite;
                m_demoPanel.image.sprite = sp;
                m_demoPanel.text.text = "ABCDEFG";
                m_demoPanel.text.color = Color.blue;
                Debug.Log("热更Debug123~");

            }
        }


    }
}
