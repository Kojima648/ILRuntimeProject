using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using ProtoBuf;

[ProtoBuf.ProtoContract]
[System.Serializable]
public class MonsterData : ExcelBase
{
#if UNITY_EDITOR
    /// <summary>
    /// 编辑器下初始类转xml
    /// </summary>
    public override void Construction()
    {
        AllMonster = new List<MonsterBase>();
        for (int i = 0; i < 5; i++)
        {
            MonsterBase monster = new MonsterBase();
            monster.Id = i + 1;
            monster.Name = i + "sq";
            monster.OutLook = "Assets/GameData/Prefabs/Attack.prefab";
            monster.Rare = 2;
            monster.Height = 2 + i;
            AllMonster.Add(monster);
        }
    }
#endif

    /// <summary>
    /// 数据初始化
    /// </summary>
    public override void Init()
    {
        m_AllMonsterDic.Clear();
        foreach (MonsterBase monster in AllMonster)
        {
            if (m_AllMonsterDic.ContainsKey(monster.Id))
            {
                Debug.LogError(monster.Name + " 有重复ID");
            }
            else
            {
                m_AllMonsterDic.Add(monster.Id, monster);
            }
        }
    }

    /// <summary>
    /// 根据ID查找Monster数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public MonsterBase FinMonsterById(int id)
    {
        return m_AllMonsterDic[id];
    }

    [XmlIgnore]
    public Dictionary<int, MonsterBase> m_AllMonsterDic = new Dictionary<int, MonsterBase>();
    
    [ProtoBuf.ProtoMember(1)]
    [XmlElement("AllMonster")]
    public List<MonsterBase> AllMonster { get; set; }
    
    [ProtoBuf.ProtoMember(2)]
    [XmlElement("AllKing")]
    public List<KingBase> AllKing { get; set; }
}
[ProtoBuf.ProtoContract]
[ProtoInclude(20,typeof(KingBase))]
[System.Serializable]
public class MonsterBase
{
    //ID
    [ProtoBuf.ProtoMember(1)]
    [XmlAttribute("Id")]
    public int Id { get; set; }
    //Name
    [ProtoBuf.ProtoMember(2)]
    [XmlAttribute("Name")]
    public string Name { get; set; }
    //预知路径
    [ProtoBuf.ProtoMember(3)]
    [XmlAttribute("OutLook")]
    public string OutLook { get; set; }
    //怪物等级
    [ProtoBuf.ProtoMember(4)]
    [XmlAttribute("Level")]
    public int Level { get; set; }
    //怪物稀有度
    [ProtoBuf.ProtoMember(5)]
    [XmlAttribute("Rare")]
    public int Rare { get; set; }
    //怪物高度
    [ProtoBuf.ProtoMember(6)]
    [XmlAttribute("Height")]
    public float Height { get; set; }
}

[ProtoContract]
[System.Serializable]
public class KingBase :MonsterBase
{
    [ProtoMember(1)] [XmlAttribute("HP")] public int HP { get; set;  }

}
