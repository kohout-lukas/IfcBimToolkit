// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

namespace BimCollabRulesCreator.Models.SmartViews;
[XmlType("RULE")]
[XmlInclude(typeof(Property)), XmlInclude(typeof(Condition)), XmlInclude(typeof(Action))]
public class Rule
{
    [XmlElement("IFCTYPE")]
    public string IfcType { get; set; }

    [XmlElement("PROPERTY")]
    public Property Property { get; set; }

    [XmlElement("CONDITION")]
    public Condition Condition { get; set; }

    [XmlElement("ACTION")]
    public Action Action { get; set; }

    public Rule() { }

    public Rule(string parameterName,
        string propertySetName,
        string parameterDataType,
        string action)
    {
        string conditionType;
        string valueType;
        string state = string.Empty;
        string value = string.Empty;

        switch (parameterDataType)
        {
            case "double":
                conditionType = "NumericIsDefined";
                valueType = "DoubleValue";
                state = "Empty";
                break;
            case "boolean":
                conditionType = "BooleanIsDefined";
                valueType = "BoolValue";
                value = "false";
                break;
            case "integer":
                conditionType = "integerIsDefined";
                valueType = "IntegerValue";
                value = "Empty";
                break;
            default:
                conditionType = "StringIsDefined";
                valueType = "StringValue";
                break;
        }

        IfcType = "Any";
        Property = new Property
        {
            Name = parameterName,
            PropertySet = propertySetName,
            Type = "PropertySet",
            ValueType = valueType
        };
        Condition = new Condition
        {
            Type = conditionType,
            Value = value,
            State = state
        };
        Action = new Action
        {
            Type = action
        };
    }

    public Rule(string parameterName,
        string propertySetName,
        string code,
        string action,
        string condition = "StartsWith")
    {
        IfcType = "Any";
        Property = new Property
        {
            Name = parameterName,
            PropertySet = propertySetName,
            Type = "PropertySet",
            ValueType = "StringValue",
            Unit = "None"
        };
        Condition = new Condition
        {
            Type = condition,
            Value = code
        };
        Action = new Action
        {
            Type = action
        };
    }
    public Rule(string ifcClass,
        string action)
    {
        IfcType = ifcClass;
        Property = new Property
        {
            Name = "None",
            PropertySet = "None",
            Type = "None",
            ValueType = "None",
            Unit = "None"
        };
        Condition = new Condition
        {
            Type = "Is",
            Value = string.Empty
        };
        Action = new Action
        {
            Type = action
        };
    }
}
