using System.Xml.Serialization;

public class Balloon
{
    [XmlAttribute("hitpoints")]
    public int Hitpoints { get; set; }

    [XmlElement("item")]
    public string Item { get; set; }

    [XmlElement("trajectory")]
    public Trajectory Trajectory { get; set; }
}
