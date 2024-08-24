// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using BimCollabRulesCreator.Models.ClashRules;
using BimCollabRulesCreator.Models.SmartViews;

namespace BimCollabRulesCreator;
public class ClashRuleFactory
{
    private readonly string _clashRuleSetName;
    private readonly List<string> _modelNames;
    private readonly bool _onlyInternalClashes;
    private readonly bool _onlyClashesVersusBuilding;

    public ClashRuleFactory(string clashRuleSetName,
        List<string> modelNames,
        bool onlyInternalClashes,
        bool onlyClashesVersusBuilding)
    {
        _modelNames = modelNames;
        _onlyInternalClashes = onlyInternalClashes;
        _onlyClashesVersusBuilding = onlyClashesVersusBuilding;
        if (!onlyInternalClashes && !onlyClashesVersusBuilding)
            _clashRuleSetName = clashRuleSetName;
        else if (onlyInternalClashes)
            _clashRuleSetName = clashRuleSetName + " - INTERNAL";
        else
            _clashRuleSetName = clashRuleSetName + " - ASR vs PROFESE";
    }

    public static ClashRulesRoot CreateClashRuleSet()
    {
        var root = new ClashRulesRoot
        {
            ClashRulesFile = new ClashRulesFile()
        };
        return root;
    }

    public static ClashRule CreateBasicClashRule(string sourceModel,
        string targetModel)
    {
        var clashRule = new ClashRule
        {
            Title = sourceModel + " - " + targetModel,
            Description = sourceModel + " - " + targetModel,
        };
        var sourceSmartView = new SmartView
        {
            Title = string.Empty,
            Description = string.Empty,
            Creator = string.Empty,
            Modifier = string.Empty
        };
        var targetSmartView = new SmartView
        {
            Title = string.Empty,
            Description = string.Empty,
            Creator = string.Empty,
            Modifier = string.Empty
        };

        var sourceRule = new Rule("Model", "Summary", sourceModel, "Add", "Contains");
        var targetRule = new Rule("Model", "Summary", targetModel, "Add", "Contains");
        var removeOpeningRule = new Rule("Opening", "Remove");
        var removeSpaceRule = new Rule("Space", "Remove");

        sourceSmartView.Rules.Add(sourceRule);
        sourceSmartView.Rules.Add(removeOpeningRule);
        sourceSmartView.Rules.Add(removeSpaceRule);

        targetSmartView.Rules.Add(targetRule);
        targetSmartView.Rules.Add(removeOpeningRule);
        targetSmartView.Rules.Add(removeSpaceRule);

        clashRule.SourceSmartViews.Add(sourceSmartView);
        clashRule.TargetSmartViews.Add(targetSmartView);

        return clashRule;
    }
    public static ClashRule CreateRulesForEndPoints(IEnumerable<string> names)
    {
        var clashRule = new ClashRule
        {
            Title = "PC",
            Description = "PC",
        };
        var sourceSmartView = new SmartView
        {
            Title = string.Empty,
            Description = string.Empty,
            Creator = string.Empty,
            Modifier = string.Empty
        };
        var targetSmartView = new SmartView
        {
            Title = string.Empty,
            Description = string.Empty,
            Creator = string.Empty,
            Modifier = string.Empty
        };

        foreach(var name in names)
        {
            var sourceRule = new Rule("IFC Element", "Summary", name, "Add", "Is");
            var targetRule = new Rule("IFC Element", "Summary", name, "Add", "Is");
            sourceSmartView.Rules.Add(sourceRule);
            targetSmartView.Rules.Add(targetRule);
        }
        clashRule.SourceSmartViews.Add(sourceSmartView);
        clashRule.TargetSmartViews.Add(targetSmartView);

        return clashRule;
    }
}
