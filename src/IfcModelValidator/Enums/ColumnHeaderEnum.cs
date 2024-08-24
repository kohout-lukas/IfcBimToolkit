// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace IfcModelValidator.Enums;

/// <summary>
///     Enumeration containing all header names occuring in da standard stored in Excel sheet.
/// </summary>
public enum ColumnHeader
{
    Unknown,
    ParentElementClassification,
    ParentElementName,
    ElementName,
    ElementClassification,
    ParameterGroup,
    ParameterName,
    ParameterDescription,
    ParameterDataType,
    ParameterUnit,
    DUR,
    DSP,
    PDPS,
    DSPS,
    ParameterIfc,
    ParameterRevitInternal,
    ParameterRevitShared,
    ParameterGuid
}