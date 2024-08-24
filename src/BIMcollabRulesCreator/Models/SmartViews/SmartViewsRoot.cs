// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

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
