// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace DataStandardRepository.Models;

/// <summary>
///     Database model for storing building element parameter with relevant project stages.
/// </summary>
public class ElementParameterModel
{
    /// <summary>
    ///     Complete database model of parameter.
    /// </summary>
    public ParameterModel Parameter { get; set; }

    /// <summary>
    ///     Collection of project stages for whom is given parameter valid.
    /// </summary>
    public List<ProjectStageModel> ProjectStages { get; set; }

    /// <summary>
    ///     Note specifies some details that are necessary for proper use of element parameter.
    /// </summary>
    public string? Note { get; set; }

    public ElementParameterModel(ParameterModel parameter, IEnumerable<ProjectStageModel> projectStages)
    {
        Parameter = parameter;
        ProjectStages = [.. projectStages];
    }
    public ElementParameterModel()
    {
        Parameter = new ParameterModel();
        ProjectStages = [];
    }
}