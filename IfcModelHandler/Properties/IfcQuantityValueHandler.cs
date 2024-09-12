// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using Xbim.Ifc4.Interfaces;

namespace IfcModelHandler.Properties;

/// <summary>
///     Logic for retrieving IFC element's quantity values.
/// </summary>
public static class IfcQuantityValueHandler
{
    /// <summary>
    ///     Retrieves IFC quantity value based on its type.
    /// </summary>
    /// <param name="ifcQuantity">IFC quantity to get value from.</param>
    /// <returns>value of the property.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static dynamic GetIfcQuantityValueByType(IIfcPhysicalQuantity ifcQuantity)
    {
        dynamic quantityValue = ifcQuantity switch
        {
            IIfcPhysicalSimpleQuantity simpleQuantity => Convert.ToInt32(simpleQuantity.ToString()),
            _ => throw new ArgumentOutOfRangeException(ifcQuantity.Name)
        };
        return quantityValue;
    }
}
