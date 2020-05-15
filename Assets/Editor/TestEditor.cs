using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TestEditor
{
    /*[MenuItem("Tools/文件写入测试")]
    public static void JenkinsTest()
    {
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/测试.txt");
        StreamWriter sw = fileInfo.CreateText();
        sw.WriteLine(System.DateTime.Now);
        sw.Close();
        sw.Dispose();
    }*/

    private const string DLLPATH = "Assets/GameData/Data/HotFix/HotFix_Project.dll";
    private const string PDBPATH = "Assets/GameData/Data/HotFix/HotFix_Project.pdb";
    private static Sprite ttt;

    [MenuItem("Tools/测试加载")]
    public static void TestLoad()
    {
        ttt = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameData/UGUI/Test1.png");
    }

    [MenuItem("Tools/测试卸载")]
    public static void TestUnLoad()
    {
        Resources.UnloadAsset(ttt);
        //对引用进行了释放，但是还存在在编辑器内存
    }

    [MenuItem("Tools/修改热更后缀为bytes")]
    public static void ChangeDllName()
    {
        if (File.Exists(DLLPATH))
        {
            string targetPath = DLLPATH + ".bytes";
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            File.Move(DLLPATH, targetPath);
        }

        if (File.Exists(PDBPATH))
        {
            string targetPath = PDBPATH + ".bytes";
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            File.Move(PDBPATH, targetPath);
        }

        AssetDatabase.Refresh();
    }
}