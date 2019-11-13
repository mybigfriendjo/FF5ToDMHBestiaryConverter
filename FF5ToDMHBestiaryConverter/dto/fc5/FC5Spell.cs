using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.fc5
{
    // ReSharper disable once InconsistentNaming
    public class FC5Spell
    {
        private static readonly Dictionary<string, string> spellSchools = new Dictionary<string, string>
        {
            {"A", "Abjuration"},
            {"C", "Conjuration"},
            {"D", "Divination"},
            {"EN", "Enchantment"},
            {"EV", "Evocation"},
            {"I", "Illusion"},
            {"N", "Necromancy"},
            {"T", "Transmutation"}
        };

        [XmlElement("classes")] public string Classes { get; set; }
        [XmlElement("components")] public string Components { get; set; }
        [XmlElement("duration")] public string Duration { get; set; }
        [XmlElement("level")] public int Level { get; set; }
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("range")] public string Range { get; set; }
        [XmlElement("ritual")] public string Ritual { get; set; }
        [XmlElement("roll")] public string[] Roll { get; set; }
        [XmlElement("school")] public string School { get; set; }
        [XmlElement("text")] public string[] Text { get; set; }
        [XmlElement("time")] public string Time { get; set; }
        [XmlAttribute("PS")] public string Ps { get; set; }
        [XmlAttribute("UA")] public string Ua { get; set; }
    }
}