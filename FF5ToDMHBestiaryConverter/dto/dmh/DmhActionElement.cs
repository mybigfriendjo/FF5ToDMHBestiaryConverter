using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhActionElement
    {
        [XmlElement("attack_bonus")] public string AttackBonus { get; set; }
        [XmlElement("desc")] public string Desc { get; set; }
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("damage_bonus")] public string DamageBonus { get; set; }
        [XmlElement("damage_dice")] public string DamageDice { get; set; }
    }
}