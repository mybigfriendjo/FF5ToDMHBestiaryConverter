﻿using System.Xml.Serialization;

namespace FF5ToDMHBestiaryConverter.dto.fc5
{
    // ReSharper disable once InconsistentNaming
    public class FC5Item
    {
        [XmlElement("name")] public string[] Name { get; set; }
    }
}