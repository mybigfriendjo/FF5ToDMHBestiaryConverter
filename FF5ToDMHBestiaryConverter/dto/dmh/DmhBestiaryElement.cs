using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.dmh
{
    public class DmhBestiaryElement
    {
        [XmlAttribute("icon")] public string Icon { get; set; }
        [XmlAttribute("private")] public string Private { get; set; }
        [XmlElement("acrobatics")] public string Acrobatics { get; set; }
        [XmlElement("alignment")] public string Alignment { get; set; }
        [XmlElement("arcana")] public string Arcana { get; set; }
        [XmlElement("armor_class")] public string ArmorClass { get; set; }
        [XmlElement("athletics")] public string Athletics { get; set; }
        [XmlElement("challenge_rating")] public string ChallengeRating { get; set; }
        [XmlElement("charisma")] public string Charisma { get; set; }
        [XmlElement("charisma_save")] public string CharismaSave { get; set; }
        [XmlElement("condition_immunities")] public string ConditionImmunities { get; set; }
        [XmlElement("constitution")] public string Constitution { get; set; }
        [XmlElement("constitution_save")] public string ConstitutionSave { get; set; }
        [XmlElement("damage_immunities")] public string DamageImmunities { get; set; }
        [XmlElement("damage_resistances")] public string DamageResistances { get; set; }
        [XmlElement("damage_vulnerabilities")] public string DamageVulnerabilities { get; set; }
        [XmlElement("deception")] public string Deception { get; set; }
        [XmlElement("dexterity")] public string Dexterity { get; set; }
        [XmlElement("dexterity_save")] public string DexteritySave { get; set; }
        [XmlElement("history")] public string History { get; set; }
        [XmlElement("hit_dice")] public string HitDice { get; set; }
        [XmlElement("hit_points")] public string HitPoints { get; set; }
        [XmlElement("insight")] public string Insight { get; set; }
        [XmlElement("intelligence")] public string Intelligence { get; set; }
        [XmlElement("intelligence_save")] public string IntelligenceSave { get; set; }
        [XmlElement("intimidation")] public string Intimidation { get; set; }
        [XmlElement("investigation")] public string Investigation { get; set; }
        [XmlElement("languages")] public string Languages { get; set; }
        [XmlElement("medicine")] public string Medicine { get; set; }
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("nature")] public string Nature { get; set; }
        [XmlElement("perception")] public string Perception { get; set; }
        [XmlElement("performance")] public string Performance { get; set; }
        [XmlElement("persuasion")] public string Persuasion { get; set; }
        [XmlElement("religion")] public string Religion { get; set; }
        [XmlElement("senses")] public string Senses { get; set; }
        [XmlElement("size")] public string Size { get; set; }
        [XmlElement("speed")] public string Speed { get; set; }
        [XmlElement("stealth")] public string Stealth { get; set; }
        [XmlElement("strength")] public string Strength { get; set; }
        [XmlElement("strength_save")] public string StrengthSave { get; set; }
        [XmlElement("subtype")] public string Subtype { get; set; }
        [XmlElement("survival")] public string Survival { get; set; }
        [XmlElement("type")] public string Type { get; set; }
        [XmlElement("wisdom_save")] public string WisdomSave { get; set; }
        [XmlElement("wisdom")] public string Wisdom { get; set; }

        [XmlElement("actions")] public DmhActions Actions { get; set; }
        [XmlElement("legendary_actions")] public DmhLegendaryActions LegendaryActions { get; set; }
        [XmlElement("special_abilities")] public DmhSpecialAbilities SpecialAbilities { get; set; }
        [XmlElement("reactions")] public DmhReActions ReActions { get; set; }
        [XmlElement("license")] public DmhLicense License { get; set; }
    }
}