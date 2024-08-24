// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

#endregion

namespace IfcModelValidator.Handlers.Ifc;

/// <summary>
///     Logic for retrieving and creating IFC elements.
/// </summary>
public static class IfcElementHandler
{
    /// <summary>
    ///     Retrieves all IIfcElements from parsed IFC file.
    /// </summary>
    /// <param name="model">Parsed IFC model/</param>
    /// <returns>collection of all IFC elements in IFC model.</returns>
    public static IEnumerable<IIfcElement> GetAllElementsInIfcModel(IfcStore model)
    {
        var allElements = model
            .Instances
            .AsParallel()
            .Where(x => x is IIfcElement && x is not null)
            .Cast<IIfcElement>();
        return allElements;
    }
}
