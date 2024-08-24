// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace BimCollabRulesCreator.Models.ClashRules;

[XmlType("bimcollabclashrulefile")]
public class ClashRulesFile
{
    [XmlElement("version")]
    public string Version { get; init; } = "4";
    [XmlElement("applicationversion")]
    public string ApplicationVersion { get; init; } = "Win - Version: 7.2 (build 7.2.17.0)";
}
