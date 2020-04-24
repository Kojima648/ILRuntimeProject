using System;
using System.Collections.Generic;
using System.Xml.Serialization;
/// <summary>
/// 热更配置文件的数据结构
/// </summary>
[Serializable]
public class ServerInfo
{
    [XmlElement("GameVersion")] public VersionInfo[] GameVersion;
}

/// <summary>
/// 当前游戏版本对应的所有补丁
/// </summary>
[Serializable]
public class VersionInfo
{
    //当前游戏的版本号
    [XmlAttribute] public string Version;
    //当前版本下的热更包（可能含多个）
    [XmlElement] public Patches[] Patches;
}

/// <summary>
/// 所有热更补丁
/// </summary>
public class Patches
{
    //当前热更版本，第几次热更
    [XmlAttribute] public int Version;

    //热更的描述
    [XmlAttribute] public string Des;
    
    //所有的热更文件
    [XmlElement] public List<Patch> Files;
}

/// <summary>
/// 单个补丁包，每个热更包里面包含的文件
/// </summary>
[Serializable]
public class Patch
{
    //热更包名
    [XmlAttribute]public string Name;
    //需要下载的地址
    [XmlAttribute]public string Url;
    //当前的平台
    [XmlAttribute]public string Platform;
    //资源的MD5码
    [XmlAttribute]public string MD5;
    //资源的大小
    [XmlAttribute]public float Size;
}