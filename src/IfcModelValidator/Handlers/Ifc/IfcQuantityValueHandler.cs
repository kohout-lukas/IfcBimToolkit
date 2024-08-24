// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using Xbim.Ifc4.Interfaces;

namespace IfcModelValidator.Handlers.Ifc;

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
