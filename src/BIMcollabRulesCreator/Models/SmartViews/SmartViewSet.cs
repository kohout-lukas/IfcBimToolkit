// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace BimCollabRulesCreator.Models.SmartViews;
[XmlType("SMARTVIEWSET")]
[XmlInclude(typeof(SmartView))]
public class SmartViewSet
{
    [XmlElement("TITLE")]
    public string Title { get; set; }
    [XmlElement("DESCRIPTION")]
    public string Description { get; set; } = string.Empty;
    [XmlElement("MODIFICATIONDATE")]
    public string ModificationDate { get; init; } = DateTime.UtcNow.ToString("s");
    [XmlElement("GUID")]
    public Guid Guid { get; init; } = Guid.NewGuid();
    [XmlArray("SMARTVIEWS")]
    [XmlArrayItem("SMARTVIEW")]
    public List<SmartView> SmartViews { get; set; } = [];
}
