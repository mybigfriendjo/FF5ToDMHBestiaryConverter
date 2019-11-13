using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using FF5ToDMHBestiaryConverter.dto.dmh;
using FF5ToDMHBestiaryConverter.dto.fc5;

namespace FF5ToDMHBestiaryConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No input file given");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File does not exist");
            }

            XmlSerializer fc5Serializer = new XmlSerializer(typeof(FC5Compendium));
            FC5Compendium compendium;
            using (StringReader reader = new StringReader(File.ReadAllText(args[0])))
            {
                compendium = (FC5Compendium) fc5Serializer.Deserialize(reader);
            }

            DmhRoot root = new DmhRoot {Bestiary = new DmhBestiary {MinorVersion = "1", MajorVersion = "2"}};

            MapValues(compendium, root);

            XmlSerializer dmhSerializer = new XmlSerializer(typeof(DmhRoot));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (StringWriter writer = new StringWriter())
            {
                dmhSerializer.Serialize(writer, root, ns);
                string filename = Path.GetDirectoryName(args[0]) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(args[0]) +
                                  "_dmhBestiary.xml";
                File.WriteAllText(filename, writer.ToString());
            }
        }

        private static void MapValues(FC5Compendium compendium, DmhRoot root)
        {
            List<DmhBestiaryElement> bestiaryElements = new List<DmhBestiaryElement>();

            foreach (FC5Monster monster in compendium.Monsters)
            {
                DmhBestiaryElement bestiaryElement = new DmhBestiaryElement {Icon = "", Private = "0"};

                if (monster.Skill != null)
                {
                    string[] skills = monster.Skill.Split(new[] {","}, StringSplitOptions.None);
                    foreach (string skill in skills)
                    {
                        if (string.IsNullOrWhiteSpace(skill))
                        {
                            continue;
                        }

                        string skillName = skill.Trim().Split(new[] {" "}, StringSplitOptions.None)[0].ToLower();
                        string skillValue = skill.Trim().Split(new[] {" "}, StringSplitOptions.None)[1].Replace("+", "");
                        switch (skillName)
                        {
                            case "history":
                                bestiaryElement.History = skillValue;
                                break;
                            case "perception":
                                bestiaryElement.Perception = skillValue;
                                break;
                            case "medicine":
                                bestiaryElement.Medicine = skillValue;
                                break;
                            case "religion":
                                bestiaryElement.Religion = skillValue;
                                break;
                            case "stealth":
                                bestiaryElement.Stealth = skillValue;
                                break;
                            case "persuasion":
                                bestiaryElement.Persuasion = skillValue;
                                break;
                            case "insight":
                                bestiaryElement.Insight = skillValue;
                                break;
                            case "deception":
                                bestiaryElement.Deception = skillValue;
                                break;
                            case "arcana":
                                bestiaryElement.Arcana = skillValue;
                                break;
                            case "athletics":
                                bestiaryElement.Athletics = skillValue;
                                break;
                            case "acrobatics":
                                bestiaryElement.Acrobatics = skillValue;
                                break;
                            case "survival":
                                bestiaryElement.Survival = skillValue;
                                break;
                            case "investigation":
                                bestiaryElement.Investigation = skillValue;
                                break;
                            case "nature":
                                bestiaryElement.Nature = skillValue;
                                break;
                            case "intimidation":
                                bestiaryElement.Intimidation = skillValue;
                                break;
                            case "performance":
                                bestiaryElement.Performance = skillValue;
                                break;
                        }
                    }
                }

                bestiaryElement.Actions = new DmhActions();
                List<DmhActionElement> dmhActions = new List<DmhActionElement>();

                if (monster.Actions != null)
                {
                    foreach (FC5Action action in monster.Actions)
                    {
                        DmhActionElement element = new DmhActionElement {Name = action.Name, Desc = string.Join("\r\n", action.Texts)};

                        if (action.Attacks == null)
                        {
                            dmhActions.Add(element);
                            continue;
                        }

                        string attackBonus = "";
                        string damageBonus = "";
                        string damageDice = "";

                        for (int i = 0; i < action.Attacks.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(action.Attacks[i]))
                            {
                                continue;
                            }

                            string[] parts = action.Attacks[i].Split(new[] {"|"}, StringSplitOptions.None);
                            string partZero = string.IsNullOrWhiteSpace(parts[0]) ? "" : parts[0] + ": ";
                            attackBonus += partZero + parts[1];
                            string[] boniDice = parts[2].Split(new[] {"+"}, StringSplitOptions.None);
                            damageBonus += partZero + (boniDice.Length > 1 ? string.Join("+", boniDice.Skip(1)) : "0");
                            damageDice += partZero + boniDice[0];
                            if (i > action.Attacks.Length - 1)
                            {
                                attackBonus += ", ";
                                damageBonus += ", ";
                                damageDice += ", ";
                            }
                        }

                        element.AttackBonus = attackBonus;
                        element.DamageBonus = damageBonus;
                        element.DamageDice = damageDice;

                        dmhActions.Add(element);
                    }
                }

                bestiaryElement.Actions.Element = dmhActions.ToArray();

                bestiaryElement.LegendaryActions = new DmhLegendaryActions();
                List<DmhLegendaryActionElement> dmhLegendaryActions = new List<DmhLegendaryActionElement>();

                if (monster.Legendaries != null)
                {
                    foreach (FC5Legendary legendary in monster.Legendaries)
                    {
                        DmhLegendaryActionElement element =
                            new DmhLegendaryActionElement {Name = legendary.Name, Desc = string.Join("\r\n", legendary.Texts)};

                        if (legendary.Attacks == null)
                        {
                            dmhLegendaryActions.Add(element);
                            continue;
                        }

                        string attackBonus = "";
                        string damageBonus = "";
                        string damageDice = "";

                        for (int i = 0; i < legendary.Attacks.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(legendary.Attacks[i]))
                            {
                                continue;
                            }

                            string[] parts = legendary.Attacks[i].Split(new[] {"|"}, StringSplitOptions.None);
                            if (parts.Length == 2)
                            {
                                parts = new[] {parts[0], "", parts[1]};
                            }

                            string partZero = string.IsNullOrWhiteSpace(parts[0]) ? "" : parts[0] + ": ";
                            attackBonus += partZero + parts[1];
                            string[] boniDice = parts[2].Split(new[] {"+"}, StringSplitOptions.None);
                            damageBonus += partZero + (boniDice.Length > 1 ? string.Join("+", boniDice.Skip(1)) : "0");
                            damageDice += partZero + boniDice[0];
                            if (i > legendary.Attacks.Length - 1)
                            {
                                attackBonus += ", ";
                                damageBonus += ", ";
                                damageDice += ", ";
                            }
                        }

                        element.AttackBonus = attackBonus;
                        element.DamageBonus = damageBonus;
                        element.DamageDice = damageDice;

                        dmhLegendaryActions.Add(element);
                    }
                }

                bestiaryElement.LegendaryActions.Element = dmhLegendaryActions.ToArray();

                bestiaryElement.SpecialAbilities = new DmhSpecialAbilities();
                List<DmhSpecialAbilityElement> dmhSpecialAbilities = new List<DmhSpecialAbilityElement>();

                if (monster.Traits != null)
                {
                    foreach (FC5Trait trait in monster.Traits)
                    {
                        DmhSpecialAbilityElement element =
                            new DmhSpecialAbilityElement {Name = trait.Name, Desc = string.Join("\r\n", trait.Texts)};

                        if (trait.Attacks == null)
                        {
                            dmhSpecialAbilities.Add(element);
                            continue;
                        }

                        string attackBonus = "";
                        string damageBonus = "";
                        string damageDice = "";

                        for (int i = 0; i < trait.Attacks.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(trait.Attacks[i]))
                            {
                                continue;
                            }

                            string[] parts = trait.Attacks[i].Split(new[] {"|"}, StringSplitOptions.None);
                            string partZero = string.IsNullOrWhiteSpace(parts[0]) ? "" : parts[0] + ": ";
                            attackBonus += partZero + parts[1];
                            string[] boniDice = parts[2].Split(new[] {"+"}, StringSplitOptions.None);
                            damageBonus += partZero + (boniDice.Length > 1 ? string.Join("+", boniDice.Skip(1)) : "0");
                            damageDice += partZero + boniDice[0];
                            if (i > trait.Attacks.Length - 1)
                            {
                                attackBonus += ", ";
                                damageBonus += ", ";
                                damageDice += ", ";
                            }
                        }

                        element.AttackBonus = attackBonus;
                        element.DamageBonus = damageBonus;
                        element.DamageDice = damageDice;

                        dmhSpecialAbilities.Add(element);
                    }
                }

                bestiaryElement.SpecialAbilities.Element = dmhSpecialAbilities.ToArray();

                bestiaryElement.ReActions = new DmhReActions();
                List<DmhReActionElement> dmhReActions = new List<DmhReActionElement>();

                if (monster.Reaction != null)
                {
                    DmhReActionElement reactionElement = new DmhReActionElement
                    {
                        Name = monster.Reaction.Name, Desc = string.Join("\r\n", monster.Reaction.Text)
                    };

                    if (monster.Reaction.Attack == null)
                    {
                        dmhReActions.Add(reactionElement);
                    }
                    else
                    {
                        string reactionAttackBonus = "";
                        string reactionDamageBonus = "";
                        string reactionDamageDice = "";

                        for (int i = 0; i < monster.Reaction.Attack.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(monster.Reaction.Attack[i]))
                            {
                                continue;
                            }

                            string[] parts = monster.Reaction.Attack[i].Split(new[] {"|"}, StringSplitOptions.None);
                            string partZero = string.IsNullOrWhiteSpace(parts[0]) ? "" : parts[0] + ": ";
                            reactionAttackBonus += partZero + parts[1];
                            string[] boniDice = parts[2].Split(new[] {"+"}, StringSplitOptions.None);
                            reactionDamageBonus += partZero + (boniDice.Length > 1 ? string.Join("+", boniDice.Skip(1)) : "0");
                            reactionDamageDice += partZero + boniDice[0];
                            if (i > monster.Reaction.Attack.Length - 1)
                            {
                                reactionAttackBonus += ", ";
                                reactionDamageBonus += ", ";
                                reactionDamageDice += ", ";
                            }
                        }

                        reactionElement.AttackBonus = reactionAttackBonus;
                        reactionElement.DamageBonus = reactionDamageBonus;
                        reactionElement.DamageDice = reactionDamageDice;

                        dmhReActions.Add(reactionElement);
                    }
                }

                bestiaryElement.ReActions.Element = dmhReActions.ToArray();

                if (monster.Save != null)
                {
                    string[] saves = monster.Save.Split(new[] {","}, StringSplitOptions.None);
                    foreach (string save in saves)
                    {
                        if (string.IsNullOrWhiteSpace(save))
                        {
                            continue;
                        }

                        string saveName = save.Trim().Split(new[] {" "}, StringSplitOptions.None)[0].ToLower();
                        string saveValue = save.Trim().Split(new[] {" "}, StringSplitOptions.None)[1].Replace("+", "");
                        switch (saveName)
                        {
                            case "str":
                                bestiaryElement.StrengthSave = saveValue;
                                break;
                            case "dex":
                                bestiaryElement.DexteritySave = saveValue;
                                break;
                            case "con":
                                bestiaryElement.ConstitutionSave = saveValue;
                                break;
                            case "int":
                                bestiaryElement.IntelligenceSave = saveValue;
                                break;
                            case "wis":
                                bestiaryElement.WisdomSave = saveValue;
                                break;
                            case "cha":
                                bestiaryElement.CharismaSave = saveValue;
                                break;
                        }
                    }
                }

                bestiaryElement.Strength = monster.Strengh.ToString();
                bestiaryElement.Dexterity = monster.Dexterity.ToString();
                bestiaryElement.Constitution = monster.Constitution.ToString();
                bestiaryElement.Intelligence = monster.Intelligence.ToString();
                bestiaryElement.Wisdom = monster.Wisdom.ToString();
                bestiaryElement.Charisma = monster.Charisma.ToString();

                bestiaryElement.Alignment = monster.Alignment;
                bestiaryElement.ArmorClass = monster.AC;
                bestiaryElement.ChallengeRating = monster.CR + " (" + FC5Monster.xpValues[monster.CR] + " XP)";
                bestiaryElement.ConditionImmunities = monster.ConditionImmune;
                bestiaryElement.DamageImmunities = monster.Immune;
                bestiaryElement.DamageResistances = monster.Resist;
                bestiaryElement.DamageVulnerabilities = monster.Vulnerable;
                bestiaryElement.HitDice = monster.HP.Contains("(")
                    ? monster.HP.Split(new[] {"("}, StringSplitOptions.None)[1].Replace(")", "")
                    : monster.HP;
                bestiaryElement.HitPoints = monster.HP.Split(new[] {" "}, StringSplitOptions.None)[0];
                bestiaryElement.Languages = monster.Languages;
                bestiaryElement.Name = monster.Name;
                bestiaryElement.Senses = monster.Senses;
                bestiaryElement.Size = FC5Monster.monsterSizes[monster.Size];
                bestiaryElement.Speed = monster.Speed;
                bestiaryElement.Type = monster.Type;
                bestiaryElement.Subtype = monster.Type.Contains("(")
                    ? monster.Type.Substring(monster.Type.IndexOf("(") + 1, monster.Type.IndexOf(")") - (monster.Type.IndexOf("(") + 1))
                    : "";

                bestiaryElement.License = new DmhLicense();
                List<string> licenseStrings = new List<string>
                {
                    "OPEN GAME LICENSE Version 1.0a",
                    "The following text is the property of Wizards of the Coast, Inc. and is Copyright 2000 Wizards of the Coast, Inc (\"Wizards\"). All Rights Reserved.",
                    "1. Definitions: (a)\"Contributors\" means the copyright and/or trademark owners who have contributed Open Game Content; (b)\"Derivative Material\" means copyrighted material including derivative works and translations (including into other computer languages), potation, modification, correction, addition, extension, upgrade, improvement, compilation, abridgment or other form in which an existing work may be recast, transformed or adapted; (c) \"Distribute\" means to reproduce, license, rent, lease, sell, broadcast, publicly display, transmit or otherwise distribute; (d)\"Open Game Content\" means the game mechanic and includes the methods, procedures, processes and routines to the extent such content does not embody the Product Identity and is an enhancement over the prior art and any additional content clearly identified as Open Game Content by the Contributor, and means any work covered by this License, including translations and derivative works under copyright law, but specifically excludes Product Identity. (e) \"Product Identity\" means product and product line names, logos and identifying marks including trade dress; artifacts; creatures characters; stories, storylines, plots, thematic elements, dialogue, incidents, language, artwork, symbols, designs, depictions, likenesses, formats, poses, concepts, themes and graphic, photographic and other visual or audio representations; names and descriptions of characters, spells, enchantments, personalities, teams, personas, likenesses and special abilities; places, locations, environments, creatures, equipment, magical or supernatural abilities or effects, logos, symbols, or graphic designs; and any other trademark or registered trademark clearly identified as Product identity by the owner of the Product Identity, and which specifically excludes the Open Game Content; (f) \"Trademark\" means the logos, names, mark, sign, motto, designs that are used by a Contributor to identify itself or its products or the associated products contributed to the Open Game License by the Contributor (g) \"Use\", \"Used\" or \"Using\" means to use, Distribute, copy, edit, format, modify, translate and otherwise create Derivative Material of Open Game Content. (h) \"You\" or \"Your\" means the licensee in terms of this agreement.",
                    "2. The License: This License applies to any Open Game Content that contains a notice indicating that the Open Game Content may only be Used under and in terms of this License. You must affix such a notice to any Open Game Content that you Use. No terms may be added to or subtracted from this License except as described by the License itself. No other terms or conditions may be applied to any Open Game Content distributed using this License.",
                    "3.Offer and Acceptance: By Using the Open Game Content You indicate Your acceptance of the terms of this License.",
                    "4. Grant and Consideration: In consideration for agreeing to use this License, the Contributors grant You a perpetual, worldwide, royalty-free, non-exclusive license with the exact terms of this License to Use, the Open Game Content.",
                    "5.Representation of Authority to Contribute: If You are contributing original material as Open Game Content, You represent that Your Contributions are Your original creation and/or You have sufficient rights to grant the rights conveyed by this License.",
                    "6.Notice of License Copyright: You must update the COPYRIGHT NOTICE portion of this License to include the exact text of the COPYRIGHT NOTICE of any Open Game Content You are copying, modifying or distributing, and You must add the title, the copyright date, and the copyright holder's name to the COPYRIGHT NOTICE of any original Open Game Content you Distribute.",
                    "7. Use of Product Identity: You agree not to Use any Product Identity, including as an indication as to compatibility, except as expressly licensed in another, independent Agreement with the owner of each element of that Product Identity. You agree not to indicate compatibility or co-adaptability with any Trademark or Registered Trademark in conjunction with a work containing Open Game Content except as expressly licensed in another, independent Agreement with the owner of such Trademark or Registered Trademark. The use of any Product Identity in Open Game Content does not constitute a challenge to the ownership of that Product Identity. The owner of any Product Identity used in Open Game Content shall retain all rights, title and interest in and to that Product Identity.",
                    "8. Identification: If you distribute Open Game Content You must clearly indicate which portions of the work that you are distributing are Open Game Content.",
                    "9. Updating the License: Wizards or its designated Agents may publish updated versions of this License. You may use any authorized version of this License to copy, modify and distribute any Open Game Content originally distributed under any version of this License.",
                    "10 Copy of this License: You MUST include a copy of this License with every copy of the Open Game Content You Distribute.",
                    "11. Use of Contributor Credits: You may not market or advertise the Open Game Content using the name of any Contributor unless You have written permission from the Contributor to do so.",
                    "12 Inability to Comply: If it is impossible for You to comply with any of the terms of this License with respect to some or all of the Open Game Content due to statute, judicial order, or governmental regulation then You may not Use any Open Game Material so affected.",
                    "13 Termination: This License will terminate automatically if You fail to comply with all terms herein and fail to cure such breach within 30 days of becoming aware of the breach. All sublicenses shall survive the termination of this License.",
                    "14 Reformation: If any provision of this License is held to be unenforceable, such provision shall be reformed only to the extent necessary to make it enforceable.",
                    "15 COPYRIGHT NOTICE Open Game License v 1.0 Copyright 2000, Wizards of the Coast, Inc."
                };
                bestiaryElement.License.Element = licenseStrings.ToArray();

                bestiaryElements.Add(bestiaryElement);
            }

            root.Bestiary.Elements = bestiaryElements.ToArray();
        }
    }
}