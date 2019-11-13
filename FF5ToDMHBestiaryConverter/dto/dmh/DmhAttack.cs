using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhAttack
    {
        [XmlAttribute("desc")] public string Description { get; set; }
        [XmlElement("attack_bonus")] public string AttackBonus { get; set; }
        [XmlElement("damage_bonus")] public string DamageBonus { get; set; }
        [XmlElement("damage_dice")] public string DamageDice { get; set; }
    }
}