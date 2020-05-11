using System.Collections;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;

public class ILRuntimeManager : Singleton<ILRuntimeManager>
{
    private const string DLLPATH = "Assets/GameData/Data/HotFix/HotFix_Project.dll.txt";
    private const string PDBPATH = "Assets/GameData/Data/HotFix/HotFix_Project.pdb.txt";
    AppDomain m_AppDomain;
    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;

    public void Init()
    {
        GameStart.Instance.StartCoroutine( LoadHotFixAssembly());
    }
 

    IEnumerator LoadHotFixAssembly()
    {
        m_AppDomain = new AppDomain();
        WWW www = new WWW("file:///" +"Assets/GameData/Data/HotFix/HotFix_Project.dll");
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();
        
        www = new WWW("file:///" + "Assets/GameData/Data/HotFix/HotFix_Project.pdb");
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] pdb = www.bytes;
        fs = new MemoryStream(dll);
        p = new MemoryStream(pdb);
        
        m_AppDomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        
        InitializeILRuntime();
        OnHotFixLoaded();
    }
    
    private void InitializeILRuntime()
    {
    }

    private void OnHotFixLoaded()
    {
        m_AppDomain.Invoke("HotFix_Project.TestClass", "Hi", null, null);
    }
}