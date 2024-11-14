// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using IfcBimToolkitApp.Models;
using IfcModelHandler.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc4.SharedBldgElements;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.PropertyResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.SharedComponentElements;

namespace IfcBimToolkitApp.Commands;
public static class AddPropertiesWithValuesCommand
{
    private static readonly List<ElementPropertyDataModel> seed =
    [
        new ElementPropertyDataModel(typeof(IfcColumn), "MSKP", "Kód prvku", "SL03", "Xbim.Ifc4.MeasureResource.IfcText"),
        new ElementPropertyDataModel(typeof(IfcElementAssembly), "MSKP", "Kód prvku", "DC01", "Xbim.Ifc4.MeasureResource.IfcText"),
        new ElementPropertyDataModel(typeof(IfcBeam), "MSKP", "Kód prvku", "DP01", "Xbim.Ifc4.MeasureResource.IfcText"),
        new ElementPropertyDataModel(typeof(IfcMember), "MSKP", "Kód prvku", "DP02", "Xbim.Ifc4.MeasureResource.IfcText"),
        new ElementPropertyDataModel(typeof(IfcPlate), "MSKP", "Kód prvku", "DP03", "Xbim.Ifc4.MeasureResource.IfcText"),
        new ElementPropertyDataModel(typeof(IfcMechanicalFastener), "MSKP", "Kód prvku", "OB01", "Xbim.Ifc4.MeasureResource.Ifctext"),
    ];

    public static void AddPropertiesWithData(string input, string output)
    {
        using var model = IfcStore.Open(input);
        using var transaction = model.BeginTransaction("Adding element data");
        var elements = IfcElementHandler<IfcObjectDefinition>.GetAllElementsInIfcModel(model);
        var data = new List<ElementPropertyDataModel>();
        var distinctData = GetDataDistincByIfcClass(seed);

        foreach (var element in elements)
        {
            var type = element.GetType();
            if (distinctData.ContainsKey(element.GetType()))
            {
                CreateElementData(model, element, distinctData[element.GetType()]);
            }
        }

        transaction.Commit();
        model.SaveAs(output, Xbim.IO.StorageType.Ifc);
    }

    private static Dictionary<Type, List<ElementPropertyDataModel>> GetDataDistincByIfcClass(List<ElementPropertyDataModel> data)
    {
        Dictionary<Type, List<ElementPropertyDataModel>> distinctData = [];
        foreach (var element in data)
        {
            if (!distinctData.TryGetValue(element.IfcClass, out List<ElementPropertyDataModel>? value))
            {
                value = [];
                distinctData.Add(element.IfcClass, value);
            }
            value.Add(element);
        }
        return distinctData;
    }
    private static void CreateElementData(IfcStore model, IfcObjectDefinition element, List<ElementPropertyDataModel> elementData)
    {        
        var orderedElementData = elementData.OrderBy(x => x.PropertySetName);
        var comparison = "";

        foreach (var item in orderedElementData)
        {
            if (comparison == item.PropertySetName)
            {
                continue;
            }
            var pSetRel = model.Instances.New<IfcRelDefinesByProperties>(r =>
            {
                r.GlobalId = Guid.NewGuid();
                r.RelatingPropertyDefinition = model.Instances.New<IfcPropertySet>(pSet =>
                {
                    pSet.Name = item.PropertySetName;
                    pSet.HasProperties.AddRange(CreateElementProperties(model, orderedElementData.Where(x => x.PropertySetName == pSet.Name)));
                });
            });          
            pSetRel.RelatedObjects.Add(element);
            comparison = item.PropertySetName;
        }
    }
    private static List<IfcProperty> CreateElementProperties(IfcStore model, IEnumerable<ElementPropertyDataModel> elementData)
    {
        List<IfcProperty> properties = [];
        foreach (var item in elementData) 
        {
            properties.Add(model.Instances.New<IfcPropertySingleValue>(p =>
            {
                p.Name = item.PropertyName;
                p.NominalValue = IfcValueFactory(item.PropertyIfcValue, item.PropertyNominalValue);
            }));
        }
        return properties;
    }
    private static IfcValue IfcValueFactory(IfcValue? type, string value)
    {
        return type switch
        {
            IfcLengthMeasure => new IfcLengthMeasure(value),
            IfcLabel => new IfcLabel(value),
            IfcVolumeMeasure => new IfcLabel(value),
            IfcAreaMeasure => new IfcLabel(value),
            _ => new IfcText(value),
        };
    }
}
