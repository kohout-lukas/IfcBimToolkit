// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace IfcModelHandler.Properties;

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
