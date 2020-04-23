using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TestEditor
{
    [MenuItem("TTT/TTTT")]
    public static void JenkinsTest()
    {
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/测试.txt");
        StreamWriter sw = fileInfo.CreateText();
        sw.WriteLine(System.DateTime.Now);
        sw.Close();
        sw.Dispose();
    }


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
}
