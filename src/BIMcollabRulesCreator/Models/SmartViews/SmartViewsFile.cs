// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

namespace BimCollabRulesCreator.Models.SmartViews;

[XmlType("bimcollabsmartviewfile")]
public class SmartViewsFile
{
    [XmlElement("version")]
    public string Version { get; init; } = "6";
    [XmlElement("applicationversion")]
    public string ApplicationVersion { get; init; } = "Win - Version: 6.4 (build 6.4.0.9)";
}
