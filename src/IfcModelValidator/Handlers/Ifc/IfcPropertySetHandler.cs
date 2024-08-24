// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace IfcModelValidator.Handlers.Ifc;

/// <summary>
///     Logic for retrieving IFC element's property sets.
/// </summary>
public static class IfcPropertySetHandler
{
    /// <summary>
    ///     Retrieves all property sets in given IFC model.
    /// </summary>
    /// <param name="ifcModel">Source IFC model for parsing.</param>
    /// <returns>collection of all IFC property sets in IFC model.</returns>
    public static IEnumerable<IIfcPropertySet> GetAllPropertySetsInIfcModel(IfcStore ifcModel)
    {
        var allPropertySets = ifcModel
            .Instances
            .OfType<IIfcPropertySet>();
        return allPropertySets;
    }
}
