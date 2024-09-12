// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.ProductExtension;

namespace IfcBimToolkitApp.Models;
public class ElementPropertyDataModel
{
    public Type IfcClass { get; set; } = typeof(IfcElement);
    public string PropertySetName { get; set; } = string.Empty;
    public string PropertyName { get; set; } = string.Empty;
    public string PropertyNominalValue { get; set; } = string.Empty;
    public IfcValue? PropertyIfcValue { get; set; } = new IfcText();

    public ElementPropertyDataModel(Type ifcClass,
        string propertySetName,
        string propertyName,
        string propertyNominalValue,
        string propertyIfcValue)
    {
        IfcClass = ifcClass;
        PropertySetName = propertySetName;
        PropertyName = propertyName;
        PropertyNominalValue = propertyNominalValue;
        var type = Type.GetType(propertyIfcValue);
        if (type == null)
        {
            PropertyIfcValue = new IfcText(PropertyNominalValue);
        }
        else
        {
            PropertyIfcValue = type as IfcValue;
        }
    }
}
