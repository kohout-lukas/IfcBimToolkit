﻿// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using Xbim.Ifc4.Interfaces;

namespace IfcModelHandler.Elements;

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