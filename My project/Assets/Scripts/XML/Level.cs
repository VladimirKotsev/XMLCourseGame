using System.Xml.Serialization;

public class Level
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlElement("balloons")]
    public Balloons Balloons { get; set; }
}
