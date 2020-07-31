using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace HotFix_Project
{
    public class MenuUi : Window
    {
        private MenuPanel m_MainPanel;

        public override void Awake(object param1 = null,
        object param2 = null, object param3 = null)
        {
            m_MainPanel = GameObject.GetComponent<MenuPanel>();
            AddButtonClickListener(m_MainPanel.m_StartButton, OnClickStart);
            AddButtonClickListener(m_MainPanel.m_LoadButton, OnClickLoad);
            AddButtonClickListener(m_MainPanel.m_ExitButton, OnClickExit);
            ObjectManager.Instance.InstantiateObject("Assets/GameData/Prefabs/Attack.prefab");
            ResourceManager.Instance.AsyncLoadResource("Assets/GameData/UGUI/Test1.png", OnLoadSpriteTest1, LoadResPriority.RES_MIDDLE, true);
            ResourceManager.Instance.AsyncLoadResource("Assets/GameData/UGUI/test3.png", OnLoadSpriteTest3, LoadResPriority.RES_HIGHT, true);
            ResourceManager.Instance.AsyncLoadResource("Assets/GameData/UGUI/test2.png", OnLoadSpriteTest2, LoadResPriority.RES_HIGHT, true);

            //LoadMonsterData();
        }


        void LoadMonsterData()
        {
            MonsterData monsterData = ConfigerManager.Instance.FindData<MonsterData>(CFG.TABLE_MONSTER);
            Debug.LogError(monsterData);
            for (int i = 0; i < monsterData.AllMonster.Count; i++)
            {
                Debug.Log(string.Format("ID:{0} 名字：{1}  外观：{2}  高度：{3}  稀有度：{4}", monsterData.AllMonster[i].Id, monsterData.AllMonster[i].Name, monsterData.AllMonster[i].OutLook, monsterData.AllMonster[i].Height, monsterData.AllMonster[i].Rare));
            }
        }

        void OnLoadSpriteTest1(string path, Object obj, object param1 = null, object param2 = null, object param3 = null)
        {
            if (obj != null)
            {
                Sprite sp = obj as Sprite;
                m_MainPanel.m_Test1.sprite = sp;
                Debug.Log("图片1加载出来了~热更新2.0");

            }
        }

        void OnLoadSpriteTest3(string path, Object obj, object param1 = null, object param2 = null, object param3 = null)
        {
            if (obj != null)
            {
                Sprite sp = obj as Sprite;
                m_MainPanel.m_Test3.sprite = sp;
                Debug.Log("图片3加载出来了~热更新2.0");
            }
        }

        void OnLoadSpriteTest2(string path, Object obj, object param1 = null, object param2 = null, object param3 = null)
        {
            if (obj != null)
            {
                Sprite sp = obj as Sprite;
                m_MainPanel.m_Test2.sprite = sp;
                Debug.Log("图片2加载出来了~热更新2.0");
            }
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ResourceManager.Instance.ReleaseResouce(m_MainPanel.m_Test1.sprite, true);
                m_MainPanel.m_Test1.sprite = null;
            }
        }

        void OnClickStart()
        {
            Debug.Log("点击了开始游戏！");
        }

        void OnClickLoad()
        {
            Debug.Log("点击了加载游戏！");
        }

        void OnClickExit()
        {
            Debug.Log("点击了退出游戏！");
        }
    }
}