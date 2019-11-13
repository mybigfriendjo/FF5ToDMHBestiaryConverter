using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhBestiary
    {
        [XmlAttribute("minorversion")] public string MinorVersion { get; set; }
        [XmlAttribute("majorversion")] public string MajorVersion { get; set; }
        [XmlElement("element")] public DmhBestiaryElement[] Elements { get; set; }
    }
}