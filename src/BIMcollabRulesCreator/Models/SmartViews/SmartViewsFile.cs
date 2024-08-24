// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace BimCollabRulesCreator.Models.SmartViews;

[XmlType("bimcollabsmartviewfile")]
public class SmartViewsFile
{
    [XmlElement("version")]
    public string Version { get; init; } = "6";
    [XmlElement("applicationversion")]
    public string ApplicationVersion { get; init; } = "Win - Version: 6.4 (build 6.4.0.9)";
}
