// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace DataStandardRepository.Models;

/// <summary>
///     Database model for storing all non-graphical information about building elements.
/// </summary>
public class ElementModel
{
    /// <summary>
    ///     Base identification.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Name of the element.
    /// </summary>
    /// 
    public string? Name { get; set; }

    /// <summary>
    ///     Unique part of code used for easy differentiating between building elements.
    /// </summary>
    public string Classification { get; set; }

    public Dictionary<string, string> UniqueClassification { get; set; }

    public string? Description { get; set; }
    /// <summary>
    ///     Parent or higher level element model.
    /// </summary>
    public ElementModel? ParentElementModel { get; set; }

    /// <summary>
    ///     Collection of building element parameters with given project stages.
    /// </summary>
    public List<ElementParameterModel>? ElementParameters { get; set; }

    public string? DocumentationPart { get; set; }

    public ElementModel(string name, string ifcElementType, string ifcObjectType, string documentationPart)
    {
        Name = name;
        Classification = ifcElementType == string.Empty ? ifcObjectType : ifcElementType;
        UniqueClassification = new Dictionary<string, string> {
            { ifcObjectType ?? string.Empty, ifcElementType ?? string.Empty } };
        DocumentationPart = documentationPart;
    }
    public ElementModel()
    {
        Name = string.Empty;
        Classification = string.Empty;
        UniqueClassification = [];
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public bool HasParent()
    {
        return ParentElementModel != null;
    }
}
