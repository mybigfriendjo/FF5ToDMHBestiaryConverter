using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhLicense
    {
        [XmlElement("element")] public string[] Element { get; set; }
    }
}