// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using IfcModelValidator.Models;
using Xbim.Ifc4.Interfaces;

namespace IfcModelValidator.Handlers.Ifc;
public class IfcPropertyHandler
{
    /// <summary>
    ///     Retrieves all property sets of instance of given IFC element.
    /// </summary>
    /// <param name="ifcElement">IFC element with properties in property sets.</param>
    /// <returns>collection of IFC property sets of instance.</returns>
    public static List<(IIfcProperty Property, bool PartOfComplexProperty)> GetElementPropertiesOfInstance(IIfcElement ifcElement)
    {
        var propertySets = ifcElement.IsDefinedBy
            .AsParallel()
            .Where(x => x.RelatingPropertyDefinition is IIfcPropertySet)
            .Select(x => x.RelatingPropertyDefinition)
            .ToList();
        List<(IIfcProperty Property, bool PartOfComplexProperty)> properties = [];
        foreach (var propertySetDefinition in propertySets)
        {
            properties.AddRange(GetAllPropertiesOfPropertySet((IIfcPropertySet)propertySetDefinition));
        }
        return properties;
    }

    /// <summary>
    ///     Retrieves all property sets of type of given IFC element.
    /// </summary>
    /// <param name="ifcElement">IFC element with properties in property sets.</param>
    /// <returns>collection of IFC property sets of type.</returns>
    public static List<(IIfcProperty Property, bool PartOfComplexProperty)> GetElementPropertiesOfType(IIfcElement ifcElement)
    {
        var propertySets = ifcElement.IsTypedBy
            .AsParallel()
            .Where(x => x.RelatingType is not null)
            .SelectMany(x => x.RelatingType.HasPropertySets)
            .ToList();
        List<(IIfcProperty Property, bool PartOfComplexProperty)> properties = [];
        foreach (var propertySetDefinition in propertySets)
        {
            if (propertySetDefinition is IIfcWindowLiningProperties or IIfcDoorLiningProperties or IIfcDoorPanelProperties)
                continue;
            properties.AddRange(GetAllPropertiesOfPropertySet((IIfcPropertySet)propertySetDefinition));
        }
        return properties;
    }
    /// <summary>
    ///     Retrieves all properties from given IFC Complex Property.
    /// </summary>
    /// <param name="complexProperty">IFCComplexProperty</param>
    /// <returns>collection of properties.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    private static List<(IIfcProperty Property, bool PartOfComplexProperty)> GetComplexProperties(IIfcComplexProperty complexProperty)
    {
        ArgumentNullException.ThrowIfNull(complexProperty);

        return complexProperty.HasProperties
            .Where(property => property is not null)
            .Select(property => (property, true))
            .ToList();
    }
    /// <summary>
    ///     Retrieves all properties stored in given property set.
    /// </summary>
    /// <param name="propertySet">IFCPropertySet</param>
    /// <returns>collection of properties.</returns>
    private static List<(IIfcProperty Property, bool PartOfComplexProperty)> GetAllPropertiesOfPropertySet(IIfcPropertySet propertySet)
    {
        List<(IIfcProperty Property, bool PartOfComplexProperty)> properties = [];
        foreach (var property in propertySet.HasProperties)
        {
            if (property is null)
                continue;
            if (property is IIfcComplexProperty)
            {
                properties.AddRange(GetComplexProperties((property as IIfcComplexProperty)!));
            }
            properties.Add((property, false));
        }
        return properties;
    }
    /// <summary>
    ///     Retrieves sorting code from given property names.
    /// </summary>
    /// <param name="properties">Collection of IFC property models to look in for classification.</param>
    /// <param name="ifcObjectTypePropertyName">Property name containing classification information.</param>
    /// <param name="ifcElementTypePropertyNames">Collection of possible property names containing classification information.</param>
    /// <returns>sorting code for given IFC element.</returns>
    public static ClassificationModel GetClassification(IEnumerable<(IIfcProperty, bool)> properties,
        string ifcObjectTypePropertyName,
        List<string> ifcElementTypePropertyNames)
    {
        var ifcObjectType = properties
            .AsParallel()
            .FirstOrDefault(x => x.Item1.Name == ifcObjectTypePropertyName);
        var ifcElementType = properties
            .AsParallel()
            .FirstOrDefault(x => ifcElementTypePropertyNames.Contains(x.Item1.Name));

        IfcPropertyModel objectProperty = ifcObjectType.Item1 is not null
            ? new(ifcObjectType.Item1.Name,
                IfcPropertyValueHandler.GetIfcPropertyValueByType(ifcObjectType.Item1),
                ifcObjectType.Item2
                    ? ifcObjectType.Item1.PartOfComplex.First().Name.ToString()
                    : ifcObjectType.Item1.PartOfPset.First().Name.ToString() ?? string.Empty)
            : new IfcPropertyModel();
        IfcPropertyModel elementProperty = ifcElementType.Item1 is not null
            ? new(ifcElementType.Item1.Name,
                IfcPropertyValueHandler.GetIfcPropertyValueByType(ifcElementType.Item1),
                ifcElementType.Item2
                    ? ifcElementType.Item1.PartOfComplex.First().Name.ToString()
                    : ifcElementType.Item1.PartOfPset.First().Name.ToString() ?? string.Empty)
            : new IfcPropertyModel();

        return new ClassificationModel(objectProperty, elementProperty);
    }
}
