using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhActions
    {
        [XmlElement("element")] public DmhActionElement[] Element { get; set; }
    }
}