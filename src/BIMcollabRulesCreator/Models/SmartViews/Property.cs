// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace BimCollabRulesCreator.Models.SmartViews;
[XmlType("PROPERTY")]
public class Property
{
    [XmlElement("NAME")]
    public string Name { get; set; } = "None";

    [XmlElement("PROPERTYSETNAME")]
    public string PropertySet { get; set; } = "None";

    [XmlElement("TYPE")]
    public string Type { get; set; } = "None";

    [XmlElement("VALUETYPE")]
    public string ValueType { get; set; } = "None";

    [XmlElement("UNIT")]
    public string Unit { get; set; } = "None";
}
