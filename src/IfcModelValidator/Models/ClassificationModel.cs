// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace IfcModelValidator.Models;
public record ClassificationModel
{
    /// <summary>
    /// First level of classification.
    /// </summary>
    public IfcPropertyModel IfcObjectTypeProperty { get; init; }
    /// <summary>
    /// Second level of classification.
    /// </summary>
    public IfcPropertyModel IfcElementTypeProperty { get; init; }

    public ClassificationModel(IfcPropertyModel ifcObjectTypeProperty, IfcPropertyModel ifcElementTypeProperty)
    {
        IfcObjectTypeProperty = ifcObjectTypeProperty;
        IfcElementTypeProperty = ifcElementTypeProperty;
    }
}
