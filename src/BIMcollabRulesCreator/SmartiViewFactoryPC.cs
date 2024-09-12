// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using BimCollabRulesCreator.Models.SmartViews;
using Action = BimCollabRulesCreator.Models.SmartViews.Action;

namespace BimCollabRulesCreator;
public class SmartViewSetFactoryPC(Dictionary<string, List<string>> rawElements)
{
    private const string SMARTVIEWSETNAME = "SPSR5";
    private const string PROPERTYSETNAME = "SPSR5";
    private readonly Dictionary<string, List<string>> _rawElements = rawElements;

    public SmartViewsRoot CreateSmartViewSet()
    {
        var root = new SmartViewsRoot
        {
            SmartViewsFile = new SmartViewsFile()
        };

        var set = new SmartViewSet
        {
            Title = SMARTVIEWSETNAME,
            Description = SMARTVIEWSETNAME
        };

        foreach (var key in _rawElements.Keys)
        {
            var smartView = CreateSmartView(key, key, "pavel.capek@bimcon.cz");
            var rules = CreateParameterRules(_rawElements[key]);
            smartView.Rules.Add(rules.First());
            rules.RemoveAt(0);
            smartView.Rules.AddRange(rules);
            smartView.Rules.Add(CreateRule_RemoveDefined(key));
            set.SmartViews.Add(smartView);
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

    private static Rule CreateRule_RemoveDefined(string parameter)
    {
        return new Rule
        {
            IfcType = "Any",
            Property = new Property
            {
                Name = parameter,
                PropertySet = PROPERTYSETNAME,
                Type = "PropertySet",
                ValueType = "StringValue"
            },
            Condition = new Condition
            {
                Type = "StringIsDefined",
                Value = ""
            },
            Action = new Action
            {
                Type = "Remove"
            }
        };
    }

    private static List<Rule> CreateParameterRules(List<string> codes)
    {
        if (codes.Count == 0)
            throw new ArgumentNullException(nameof(codes));
        List<Rule> rules = [];
        foreach (var code in codes)
        {
            var rule = new Rule()
            {
                IfcType = "Any",
                Property = new Property
                {
                    Name = "ElementProjectSpecificIdentifier",
                    PropertySet = PROPERTYSETNAME,
                    Type = "PropertySet",
                    ValueType = "StringValue",
                    Unit = "None"
                },
                Condition = new Condition
                {
                    Type = "StartsWith",
                    Value = code
                },
                Action = new Action
                {
                    Type = "Add"
                }
            };
            rules.Add(rule);
        }

        return rules;
    }
}
