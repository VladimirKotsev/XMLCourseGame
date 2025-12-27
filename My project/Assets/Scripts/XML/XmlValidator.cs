using System.Xml;
using System.Xml.Schema;
using UnityEngine;

public static class XmlValidator
{
    public static bool Validate(string xmlPath, string xsdPath)
    {
        bool isValid = true;

        XmlSchemaSet schema = new XmlSchemaSet();
        schema.Add("", xsdPath);

        XmlReaderSettings settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.Schemas = schema;
        settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

        settings.ValidationEventHandler += (sender, e) =>
        {
            isValid = false;
            Debug.LogError($"XML Validation Error: {e.Message}");
        };

        using (XmlReader reader = XmlReader.Create(xmlPath, settings))
        {
            while (reader.Read()) { }
        }

        return isValid;
    }
}
