// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

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
