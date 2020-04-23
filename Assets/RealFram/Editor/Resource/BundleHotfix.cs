using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class BundleHotfix : EditorWindow
{
    [MenuItem("Tools/打包热更包")]
    static void Init()
    {
        BundleHotfix window = (BundleHotfix) GetWindow(typeof(BundleHotfix), false, "热更新界面", true);
        window.Show();
    }

    //md5路径
    private string md5Path = "";
    //热更次数
    private string hotCount = "1";

    //调用windows文件系统对象
    private OpenFileName m_OpenFileName = null;

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        md5Path = EditorGUILayout.TextField("ABMD5路径: ", md5Path, GUILayout.Width(350), GUILayout.Height(20));
        if (GUILayout.Button("选择版本ABMD5文件", GUILayout.Width(150), GUILayout.Height(30)))
        {
            m_OpenFileName = new OpenFileName();
            m_OpenFileName.structSize = Marshal.SizeOf(m_OpenFileName);
            m_OpenFileName.filter = "ABMD5文件(*.bytes)\0*.bytes";
            m_OpenFileName.file = new string(new char[256]);
            m_OpenFileName.maxFile = m_OpenFileName.file.Length;
            m_OpenFileName.fileTitle = new string(new char[64]);
            m_OpenFileName.maxFileTitle = m_OpenFileName.fileTitle.Length;
            //默认路径
            m_OpenFileName.initialDir = (Application.dataPath + "/../Version").Replace("/", "\\");
            m_OpenFileName.title = "打开ABMD5窗口";
            m_OpenFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

            //执行完的回调
            if (LocalDialog.GetSaveFileName(m_OpenFileName))
            {
                Debug.Log(m_OpenFileName.file);
                md5Path = m_OpenFileName.file;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        hotCount = EditorGUILayout.TextField("热更补丁版本:", hotCount, GUILayout.Width(350), GUILayout.Height(20));
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("开始打热更包",GUILayout.Width(100),GUILayout.Height(50)))
        {
            if (!string.IsNullOrEmpty(md5Path)&& md5Path.EndsWith(".bytes"))
            {
                BundleEditor.Build(true,md5Path,hotCount);
            }
        }
    }
}