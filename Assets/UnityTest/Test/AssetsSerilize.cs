using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "TestAssets",menuName ="CreatAssets",order = 0)]
public class AssetsSerilize : ScriptableObject
{
    public int Id;
    public string Name;
    public List<string> TestList;
}
