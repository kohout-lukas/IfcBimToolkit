// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using IfcModelHandler.Models;

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
