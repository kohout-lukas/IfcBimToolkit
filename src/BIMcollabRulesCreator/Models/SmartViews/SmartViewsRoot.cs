// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

namespace BimCollabRulesCreator.Models.SmartViews;

[XmlInclude(typeof(SmartViewsFile)), XmlInclude(typeof(SmartViewSet))]

[XmlType("ROOT_TO_DELETE")]
public class SmartViewsRoot
{
    [XmlElement("bimcollabsmartviewfile")]
    public SmartViewsFile SmartViewsFile { get; init; }

    [XmlArray("SMARTVIEWSETS")]
    [XmlArrayItem("SMARTVIEWSET")]
    public List<SmartViewSet> SmartViewSets { get; set; } = [];
}
