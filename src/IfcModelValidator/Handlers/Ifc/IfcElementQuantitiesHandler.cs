// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using Xbim.Ifc4.Interfaces;

namespace IfcModelValidator.Handlers.Ifc;

/// <summary>
///     Logic for retrieving and creating IFC element quantity.
/// </summary>
public class IfcElementQuantitiesHandler
{
    /// <summary>
    ///     Retrieves all IFC physical quantities from IFC element.
    /// </summary>
    /// <param name="ifcElement">Source IIfcElement from parsed IFC file.</param>
    /// <returns>collection of IIfcPhysicalQuantities of given IFC element in IFC model.</returns>
    public static IEnumerable<IIfcPhysicalQuantity> GetElementQuantitiesOfInstance(IIfcElement ifcElement)
    {
        var instanceQuantities = ifcElement.IsDefinedBy
            .Where(x => x.RelatingPropertyDefinition is IIfcElementQuantity)
            .SelectMany(x => ((IIfcElementQuantity)x.RelatingPropertyDefinition).Quantities);
        return instanceQuantities;
    }
}