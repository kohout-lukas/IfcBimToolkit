// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace DataStandardRepository.Enums;

/// <summary>
///     Enumeration containing all common engineering SW.
/// </summary>
public enum SoftwareId
{
    Revit = 1,
    ArchiCAD = 2,
    Allplan = 3,
    Civil = 4,
    Microstation = 5,
    DDSCad = 6,
    Other = 7
}