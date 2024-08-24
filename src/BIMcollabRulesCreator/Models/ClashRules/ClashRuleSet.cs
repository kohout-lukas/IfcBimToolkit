// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace BimCollabRulesCreator.Models.ClashRules;
[XmlType("CLASHRULESET")]
[XmlInclude(typeof(ClashRule))]
public class ClashRuleSet
{
    [XmlElement("TITLE")]
    public string Title { get; set; }
    [XmlElement("DESCRIPTION")]
    public string Description { get; set; } = string.Empty;
    [XmlElement(ElementName = "MODIFICATIONDATE", Namespace = "set")]
    public string ModificationDate { get; init; } = DateTime.UtcNow.ToString("s");
    [XmlElement("GUID")]
    public Guid Guid { get; init; } = Guid.NewGuid();
    [XmlArray("CLASHRULES")]
    [XmlArrayItem("CLASHRULE")]
    public List<ClashRule> ClashRules { get; set; } = [];
    public ClashRuleSet() { }
    public ClashRuleSet(string profession, string sourcePropertyValue)
    {
        Title = $"{profession} {sourcePropertyValue}";
        Description = $"{profession} {sourcePropertyValue}";
    }
}
