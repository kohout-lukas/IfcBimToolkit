// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

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
