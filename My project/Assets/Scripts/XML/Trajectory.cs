using System.Xml.Serialization;

public class Trajectory
{
    [XmlAttribute("startX")]
    public float StartX { get; set; }

    [XmlAttribute("startY")]
    public float StartY { get; set; }

    [XmlAttribute("endX")]
    public float EndX { get; set; }

    [XmlAttribute("endY")]
    public float EndY { get; set; }

    [XmlAttribute("speed")]
    public int Speed { get; set; }

    [XmlAttribute("type")]
    public TrajectoryType Type { get; set; }
}
