// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using CsvHelper.Configuration.Attributes;

namespace IfcModelValidator.Models;
public class ValidationResult
{
    [Name("Název prvku v IFC")]
    public string Name { get; set; } = string.Empty;
    public string IfcType { get; set; } = string.Empty;

    public string IfcGlobalId { get; set; } = string.Empty;
    [Name("Skupina elementů")]
    public string IfcObjectType { get; set; } = string.Empty;
    [Ignore]
    public string IfcElementType { get; set; } = string.Empty;
    [Name("Označení elementu")]
    public string IfcElementTypeCsv
    {
        get
        {
            if (IfcElementType.Length > 1 && IfcElementType[0] == '0')
                return @"=""" + IfcElementType + @"""";
            return IfcElementType;
        }
    }
    [Name("Klasifikace dle DS")]
    public string ValidSortingCode { get; set; } = "NE";
    public List<string>? Properties { get; set; } = [];
    [Name("Chybějící vlastnosti")]
    public string ConcatenatedProperties
    {
        get
        {
            if (Properties != null && Properties.Count != 0)
            {
                return string.Join(":", Properties);
            }
            return string.Empty;
        }
    }
}