// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace DataStandardRepository.Models;

/// <summary>
///     Database model for storing project stages.
/// </summary>
public class ProjectStageModel
{
    /// <summary>
    ///     Project stage  given by ProjectStage enumeration. Default value is "ProjectStage.DSPS".
    /// </summary>
    public ProjectStage? ProjectStage { get; init; }

    /// <summary>
    ///     Project professions for which parameter in this stage applies.
    /// </summary>
    public HashSet<Profession> Professions { get; set; } = [];
}
