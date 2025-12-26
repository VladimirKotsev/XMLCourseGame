using System.Collections.Generic;
using System.Xml.Serialization;

public class Levels
{
    [XmlElement("level")]
    public List<Level> LevelList { get; set; }
}