// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

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
