// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

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
