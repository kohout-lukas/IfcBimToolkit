﻿// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

namespace BimCollabRulesCreator.Models.ClashRules;

[XmlType("bimcollabclashrulefile")]
public class ClashRulesFile
{
    [XmlElement("version")]
    public string Version { get; init; } = "4";
    [XmlElement("applicationversion")]
    public string ApplicationVersion { get; init; } = "Win - Version: 7.2 (build 7.2.17.0)";
}
