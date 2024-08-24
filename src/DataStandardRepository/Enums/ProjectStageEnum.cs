// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace DataStandardRepository.Enums;

/// <summary>
///     Enumeration containing all project stage's shortcuts.
/// </summary>
/// <remarks>Project stages are defined by 499/2006 Sb.</remarks>
public enum ProjectStage
{
    None,
    DUR,
    DSP,
    PDPS,
    DVZ,
    DSPS
}