using System.Collections.Generic;
using System.Xml.Serialization;

public class Balloons
{
    [XmlElement("balloon")]
    public List<Balloon> BalloonList { get; set; }
}
