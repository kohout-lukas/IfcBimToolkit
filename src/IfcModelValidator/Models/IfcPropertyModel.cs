// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace IfcModelValidator.Models;

public record IfcPropertyModel
{
    /// <summary>
    ///     Name of the IFC element property.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    ///     Saved value of the IFC element property.
    /// </summary>
    public dynamic Value { get; init; } = string.Empty;

    /// <summary>
    ///     IFC property type from IFC4 schema.
    /// </summary>
    public string Type { get; init; } = string.Empty;

    /// <summary>
    ///     IFC property set of the IFC property.
    /// </summary>
    public string PropertySet { get; init; } = string.Empty;

    public IfcPropertyModel(string name, dynamic value, string type, string propertySet)
    {
        Name = name;
        Value = value;
        Type = type;
        PropertySet = propertySet;
    }
    public IfcPropertyModel(string name, dynamic value, string propertySet)
    {
        Name = name;
        Value = value;
        PropertySet = propertySet;
    }
    public IfcPropertyModel() { }
}