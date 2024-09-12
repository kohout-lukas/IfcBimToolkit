// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

#region
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

#endregion

namespace IfcModelHandler.Elements;

/// <summary>
///     Logic for retrieving and creating IFC elements.
/// </summary>
public static class IfcElementHandler<T>
{
    /// <summary>
    ///     Retrieves all IIfcElements from parsed IFC file.
    /// </summary>
    /// <param name="model">Parsed IFC model/</param>
    /// <returns>collection of all IFC elements in IFC model.</returns>
    public static IEnumerable<T> GetAllElementsInIfcModel(IfcStore model)
    {
        var allElements = model
            .Instances
            .AsParallel()
            .Where(x => x is T && x is not null)
            .Cast<T>();
        return allElements;
    }
}
