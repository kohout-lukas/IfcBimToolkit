// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace BimCollabRulesCreator.Models.ClashRules;

[XmlInclude(typeof(ClashRulesFile)), XmlInclude(typeof(ClashRuleSet))]

[XmlType("ROOT_TO_DELETE")]
public class ClashRulesRoot
{
    [XmlElement("bimcollabclashrulefile")]
    public ClashRulesFile ClashRulesFile { get; init; }

    [XmlArray("CLASHRULESETS")]
    [XmlArrayItem("CLASHRULESET")]
    public List<ClashRuleSet> ClashRuleSets { get; set; } = [];
}
