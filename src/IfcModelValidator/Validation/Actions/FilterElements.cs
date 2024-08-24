// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout


using IfcModelValidator.Handlers.Ifc;
using Xbim.Ifc4.Interfaces;

namespace IfcModelValidator.Validation.Actions;
public class FilterElements
{
    /// <summary>
    /// Checks if element contains specified values in specified parameter.
    /// </summary>
    /// <param name="properties">Ifc element retrieved from IFc model.</param>
    /// <param name="parameterName">Parameter name to look for.</param>
    /// <param name="propertyValues">Parameter values to check.</param>
    /// <returns>true if element contains parameter with given name with at least on of given values.</returns>
    public static bool FilterByParameterValues(List<(IIfcProperty, bool)> properties, string parameterName, string[] propertyValues)
    {
        if (properties.Count == 0)
            return false;
        var parameter = properties
            .FirstOrDefault(p => p.Item1.Name.ToString().Contains(parameterName));
        if (parameter.Item1 is null)
            return false;
        string parameterValue = Convert.ToString(IfcPropertyValueHandler.GetIfcPropertyValueByType(parameter.Item1));
        if (propertyValues.Any(parameterValue.Contains))
            return true;

        return false;
    }
    /// <summary>
    /// Checks if element contains specified values in specified parameter.
    /// </summary>
    /// <param name="properties">Ifc element retrieved from IFc model.</param>
    /// <param name="parameter">Parameter name to look for.</param>
    /// <param name="propertyValue">Parameter value to check.</param>
    /// <returns>true if element contains parameter with given name with given value.</returns>
    public static bool FilterByParameterValue(List<(IIfcProperty, bool)> properties, string propertyName, string propertyValue)
    {
        if (properties.Count == 0)
            return false;
        var parameter = properties
            .FirstOrDefault(p => p.Item1.Name.ToString().Contains(propertyName));
        if (parameter.Item1 is null)
            return false;
        string parameterValue = Convert.ToString(IfcPropertyValueHandler.GetIfcPropertyValueByType(parameter.Item1));
        if (parameterValue.Contains(propertyValue))
            return true;

        return false;
    }
}
