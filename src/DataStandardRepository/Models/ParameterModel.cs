// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace DataStandardRepository.Models;

/// <summary>
///     Database model for storing parameters.
/// </summary>
public class ParameterModel
{
    /// <summary>
    ///     Base identification.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     name of the parameter.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Parameter group that is relevant for this parameter/
    /// </summary>
    public string Group { get; set; }

    /// <summary>
    ///     Short description of the parameter.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Data type of the parameter.
    /// </summary>
    public DataType DataType { get; set; }

    /// <summary>
    ///     Unit of the parameter.
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    ///     IFC standard name if it exists.
    /// </summary>
    public string? IfcName { get; set; }

    /// <summary>
    ///     Revit internal parameter name if it exists.
    /// </summary>
    public string? RevitInternalParameterName { get; set; }

    /// <summary>
    ///     Revit shared parameter name if it exists.
    /// </summary>
    public string? RevitSharedParameterName { get; set; }

    /// <summary>
    ///     Revit shared parameter Guid the shared parameter exists.
    /// </summary>
    public string? Guid { get; set; }

    public ParameterModel(string name, string group, DataType dataType, string unit)
    {
        Name = name;
        Group = group;
        DataType = dataType;
        Unit = unit;
    }
    public ParameterModel()
    {
        Name = string.Empty;
        Group = string.Empty;
        DataType = DataType.String;
        Unit = string.Empty;
    }
}
