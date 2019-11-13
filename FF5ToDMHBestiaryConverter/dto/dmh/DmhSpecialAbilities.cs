using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhSpecialAbilities
    {
        [XmlElement("element")] public DmhSpecialAbilityElement[] Element { get; set; }
    }
}