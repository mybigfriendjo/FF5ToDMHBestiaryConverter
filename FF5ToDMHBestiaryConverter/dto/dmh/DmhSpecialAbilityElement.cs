using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhSpecialAbilityElement
    {
        [XmlElement("desc")] public string Desc { get; set; }
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("attack")] public DmhAttack[] Attacks { get; set; }
    }
}