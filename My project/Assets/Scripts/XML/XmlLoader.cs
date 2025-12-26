using System.IO;
using System.Xml.Serialization;

public static class XmlLoader
{
    public static GameData LoadGame(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));

        using (FileStream stream = new FileStream(filePath, FileMode.Open))
        {
            return serializer.Deserialize(stream) as GameData;
        }
    }
}
