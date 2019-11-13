using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhLegendaryActions
    {
        [XmlElement("element")] public DmhLegendaryActionElement[] Element { get; set; }
    }
}