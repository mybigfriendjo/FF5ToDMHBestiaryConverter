using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.fc5
{
    // ReSharper disable InconsistentNaming
    public class FC5Monster
    {
        public static readonly Dictionary<string, string> monsterSizes = new Dictionary<string, string>
        {
            {"T", "Tiny"},
            {"S", "Small"},
            {"M", "Medium"},
            {"L", "Large"},
            {"H", "Huge"},
            {"G", "Gargantuan"}
        };

        public static readonly Dictionary<string, string> xpValues = new Dictionary<string, string>
        {
            {"00", "0 or 10"},
            {"0", "0 or 10"},
            {"1/8", "25"},
            {"1/4", "50"},
            {"1/2", "100"},
            {"1", "200"},
            {"2", "450"},
            {"3", "700"},
            {"4", "1,100"},
            {"5", "1,800"},
            {"6", "2,300"},
            {"7", "2,900"},
            {"8", "3,900"},
            {"9", "5,000"},
            {"10", "5,900"},
            {"11", "7,200"},
            {"12", "8,400"},
            {"13", "10,000"},
            {"14", "11,500"},
            {"15", "13,000"},
            {"16", "15,000"},
            {"17", "18,000"},
            {"18", "20,000"},
            {"19", "22,000"},
            {"20", "25,000"},
            {"21", "33,000"},
            {"22", "41,000"},
            {"23", "50,000"},
            {"24", "62,000"},
            {"25", "75,000"},
            {"26", "90,000"},
            {"27", "105,000"},
            {"28", "120,000"},
            {"29", "135,000"},
            {"30", "155,000"}
        };

        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("size")] public string Size { get; set; }
        [XmlElement("type")] public string Type { get; set; }
        [XmlElement("alignment")] public string Alignment { get; set; }
        [XmlElement("ac")] public string AC { get; set; }
        [XmlElement("hp")] public string HP { get; set; }
        [XmlElement("speed")] public string Speed { get; set; }
        [XmlElement("str")] public int Strengh { get; set; }
        [XmlElement("dex")] public int Dexterity { get; set; }
        [XmlElement("con")] public int Constitution { get; set; }
        [XmlElement("int")] public int Intelligence { get; set; }
        [XmlElement("wis")] public int Wisdom { get; set; }
        [XmlElement("cha")] public int Charisma { get; set; }
        [XmlElement("save")] public string Save { get; set; }
        [XmlElement("skill")] public string Skill { get; set; }
        [XmlElement("resist")] public string Resist { get; set; }
        [XmlElement("vulnerable")] public string Vulnerable { get; set; }
        [XmlElement("immune")] public string Immune { get; set; }
        [XmlElement("conditionImmune")] public string ConditionImmune { get; set; }
        [XmlElement("senses")] public string Senses { get; set; }
        [XmlElement("passive")] public int Passive { get; set; }
        [XmlElement("languages")] public string Languages { get; set; }
        [XmlElement("cr")] public string CR { get; set; }
        [XmlElement("trait")] public FC5Trait[] Traits { get; set; }
        [XmlElement("action")] public FC5Action[] Actions { get; set; }
        [XmlElement("legendary")] public FC5Legendary[] Legendaries { get; set; }
        [XmlElement("spells")] public string Spells { get; set; }
        [XmlElement("description")] public string Description { get; set; }
        [XmlElement("slots")] public string Slots { get; set; }
        [XmlElement("reaction")] public FC5Reaction Reaction { get; set; }

        private string CalcMod(int value)
        {
            int tmpValue;
            if (value >= 10)
            {
                tmpValue = (int) Math.Floor((double) ((value - 10) / 2));
            }
            else
            {
                tmpValue = Math.Abs(value - 10) / 2;
                tmpValue = tmpValue + value % 2;
                tmpValue *= -1;
            }

            return (tmpValue > 0 ? "+" : "") + tmpValue;
        }
    }
}