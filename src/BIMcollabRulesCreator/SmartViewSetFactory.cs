// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using BimCollabRulesCreator.Models.SmartViews;
using Action = BimCollabRulesCreator.Models.SmartViews.Action;

namespace BimCollabRulesCreator;
public class SmartViewSetFactory(string smartViewSetName,
    IEnumerable<Element> rawElements,
    bool separateElements,
    bool onlyIncorrect)
{
    private readonly string _smartViewSetName = onlyIncorrect
            ? smartViewSetName.Replace(".db", "_ruleset_remove")
            : smartViewSetName.Replace(".db", "_ruleset");
    private readonly IEnumerable<Element> _rawElements = rawElements;
    private readonly bool _separateElements = separateElements;
    private readonly bool _onlyIncorrect = onlyIncorrect;

    public SmartViewsRoot CreateSmartViewSet()
    {
        var root = new SmartViewsRoot
        {
            SmartViewsFile = new SmartViewsFile()
        };

        var set = new SmartViewSet
        {
            Title = _smartViewSetName,
            Description = _smartViewSetName
        };

        var elements = _rawElements.Where(x => x.ParentClassification is not null)
            .Where(x => x.Properties.Any(p => p.Name == "Kód prvku"))
            .ToList();

        var smartViewAll = CreateSmartView("VŠECHNY TŘÍDY", "VŠECHNY TŘÍDY", "lukas.kohout@bimcon.cz");
        smartViewAll.Rules.Add(CreateRule_AddAll());
        smartViewAll.Rules.AddRange(CreateMultipleRules(elements));
        set.SmartViews.Add(smartViewAll);

        if (_separateElements)
        {
            var listOfCodes = elements.Select(x => x.ParentClassification).Distinct();
            foreach (var code in listOfCodes)
            {
                var elementsWithCode = elements.Where(x => x.ParentClassification == code);
                if (!elementsWithCode.Any())
                    continue;
                var first = elementsWithCode.First();
                var smartView = CreateSmartView(first.ParentClassification + " - " + first.ParentName,
                    first.ParentClassification + " - " + first.ParentName, "lukas.kohout@bimcon.cz");
                smartView.Rules.AddRange(CreateMultipleRules(elementsWithCode));
                set.SmartViews.Add(smartView);
            }
        }

        root.SmartViewSets.Add(set);
        return root;
    }

    private static SmartView CreateSmartView(string name,
        string description,
        string creator)
    {
        return new SmartView
        {
            Title = name,
            Description = description,
            Creator = creator,
            Modifier = creator
        };
    }

    private IEnumerable<Rule> CreateMultipleRules(IEnumerable<Element> elements)
    {
        IEnumerable<Rule> allRules = [];
        foreach (var element in elements)
            _ = allRules.Concat(CreateElementRules(element));
        return allRules;
    }

    private static Rule CreateRule_AddAll()
    {
        return new Rule
        {
            IfcType = "Any",
            Property = new Property
            {
                Name = "None",
                PropertySet = "None",
                Type = "PropertySet",
                ValueType = "None"
            },
            Condition = new Condition
            {
                Type = "is",
                Value = null
            },
            Action = new Action
            {
                Type = "AddSetColored",
                R = "255",
                G = "0",
                B = "0"
            }
        };
    }

    private List<Rule> CreateElementRules(Element element)
    {
        if (element.Properties.Count == 0)
            throw new ArgumentNullException(nameof(element));

        var property = element.Properties
            .FirstOrDefault(x => x.Name.Contains("Kód prvku"));
        List<Rule> rules = [];

        if (_separateElements)
        {
            rules.Add(new Rule
            {
                IfcType = "Any",
                Property = new Property
                {
                    Name = "Kód prvku",
                    PropertySet = "Základní informace",
                    Type = "PropertySet",
                    ValueType = "StringValue"
                },
                Condition = new Condition
                {
                    Type = "StartsWith",
                    Value = element.Classification
                },
                Action = new Action
                {
                    Type = "AddSetColored",
                    R = "255",
                    G = "0",
                    B = "0"
                }
            });
            rules.Add(new Rule(property.Name, element.Classification, "And...", "StartsWith"));
        }
        else
            rules.Add(new Rule(property.Name, element.Classification, "And...", "StartsWith"));

        for (int i = 0, n = element.Properties.Count; i < n; i++)
        {
            var p = element.Properties.ElementAt(i);
            var rule = new Rule(p.Name, p.PropertySet, p.Type, "And...");
            if (i == element.Properties.Count - 1)
            {
                if (_onlyIncorrect)
                {
                    rule.Action.Type = "Remove";
                    rules.Add(rule);
                    continue;
                }
                rule.Action.Type = "SetColored";
                rule.Action.R = "121";
                rule.Action.G = "197";
                rule.Action.B = "0";
            }

            rules.Add(rule);
        }
        return rules;
    }
}
