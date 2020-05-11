using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public class ABMD5 
{
    [XmlElement("ABMD5List")]
    public List<ABMD5Base> ABMD5List { get; set; }
}
[Serializable]
public class ABMD5Base
{
    [XmlAttribute("Name")]
    public string Name { get; set; }
    [XmlAttribute("MD5")]
    public string MD5 { get; set; }
    [XmlAttribute("Size")]
    public float Size { get; set; }
}
