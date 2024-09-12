// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

namespace BimCollabRulesCreator.Models.SmartViews;

[XmlType("SMARTVIEW")]
[XmlInclude(typeof(Rule))]
public class SmartView
{
    [XmlElement("TITLE")]
    public string Title { get; set; }
    [XmlElement("DESCRIPTION")]
    public string Description { get; set; } = string.Empty;
    [XmlElement("CREATOR")]
    public string Creator { get; init; }
    [XmlElement("CREATIONDATE")]
    public string CreationDate { get; init; } = DateTime.UtcNow.ToString("s");
    [XmlElement("MODIFIER")]
    public string Modifier { get; init; }
    [XmlElement("MODIFICATIONDATE")]
    public string ModificationDate { get; init; } = DateTime.UtcNow.ToString("s");
    [XmlElement("GUID")]
    public Guid Guid { get; init; } = Guid.NewGuid();

    [XmlArray("RULES")]
    [XmlArrayItem("RULE")]
    public List<Rule> Rules { get; set; } = [];

}
