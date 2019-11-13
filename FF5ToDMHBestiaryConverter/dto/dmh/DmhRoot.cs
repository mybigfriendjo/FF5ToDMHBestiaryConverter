using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    [XmlRoot("root")]
    public class DmhRoot
    {
        [XmlElement("bestiary")] public DmhBestiary Bestiary { get; set; }
    }
}