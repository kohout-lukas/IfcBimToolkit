// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region
using Xbim.Common;
using Xbim.Ifc4.Interfaces;

#endregion

namespace IfcModelValidator.Handlers.Ifc;

/// <summary>
///     Logic for retrieving IFC element's properties values.
/// </summary>
public static class IfcPropertyValueHandler
{
    /// <summary>
    ///     Retrieves IFC property value based on its type.
    /// </summary>
    /// <param name="ifcProperty">IFC property to get value from.</param>
    /// <returns>value of the property.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static dynamic GetIfcPropertyValueByType(IIfcProperty ifcProperty)
    {
        dynamic? propertyValue = ifcProperty switch
        {
            null => string.Empty,
            IIfcPropertySingleValue singleValue => singleValue.NominalValue is not null
                ? singleValue.NominalValue.Value
                : string.Empty,
            IIfcPropertyEnumeratedValue enumeratedValue => GetEnumerationValues(enumeratedValue),
            IIfcPropertyBoundedValue boundedValue => (boundedValue.LowerBoundValue, boundedValue.UpperBoundValue),
            IIfcPropertyListValue listValue => GetListValues(listValue.ListValues),
            IIfcPropertyTableValue tableValue => (tableValue.DefiningUnit.ToString(), tableValue.DefiningValues.ToString(),
                GetListValues(tableValue.DefinedValues)),
            IIfcPropertyReferenceValue referenceValue => referenceValue.PropertyReference.ToString(),
            _ => string.Empty
        };
        return propertyValue ?? string.Empty;
    }

    /// <summary>
    ///     Retrieves values of IFC property value type ListValue.
    /// </summary>
    /// <param name="listValues">Collection of number of IIfcValues.</param>
    /// <returns>collection of values as standard types.</returns>
    private static List<string> GetListValues(IItemSet<IIfcValue> listValues)
    {
        List<string> values = [];
        for (var i = 0; i < listValues.Count; i++)
        {
            var value = listValues.GetAt(i).Value.ToString();
            if (value is null)
                continue;
            values.Add(value);
        }

        return values;
    }

    /// <summary>
    ///     Retrieves values of IFC property value type EnumeratedValue.
    /// </summary>
    /// <param name="enumeratedValue">IFC property value as enumeration.</param>
    /// <returns>collection of values as standard types.</returns>
    private static List<string> GetEnumerationValues(IIfcPropertyEnumeratedValue enumeratedValue)
    {
        List<string> values = [];
        for (var i = 0; i < enumeratedValue.EnumerationValues.Count; i++)
        {
            var value = enumeratedValue.EnumerationValues.GetAt(i).Value.ToString();
            if (value is null)
                continue;
            values.Add(value);
        }
        return values;
    }
}
