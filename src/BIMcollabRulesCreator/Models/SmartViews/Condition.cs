// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace BimCollabRulesCreator.Models.SmartViews;

[XmlType("CONDITION")]
public class Condition
{
    [XmlElement("TYPE")]
    public string Type { get; set; } = "None";

    [XmlElement("VALUE")]
    public string Value { get; set; }

    [XmlElement("STATE")]
    public string State { get; set; }
}
