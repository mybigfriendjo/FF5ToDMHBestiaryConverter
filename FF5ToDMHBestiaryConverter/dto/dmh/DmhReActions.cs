using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhReActions
    {
        [XmlElement("element")] public DmhReActionElement[] Element { get; set; }
    }
}