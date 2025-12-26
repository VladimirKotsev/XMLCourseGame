using System.Xml.Serialization;

[XmlRoot("game")]
public class GameData
{
    [XmlElement("levels")]
    public Levels Levels { get; set; }
}
