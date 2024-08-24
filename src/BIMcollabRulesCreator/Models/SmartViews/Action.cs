// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace BimCollabRulesCreator.Models.SmartViews;


[XmlType("ACTION")]
public class Action
{
    [XmlElement("TYPE")]
    public string Type { get; set; }

    [XmlElement("R")]
    public string R { get; set; }

    [XmlElement("G")]
    public string G { get; set; }

    [XmlElement("B")]
    public string B { get; set; }
}
