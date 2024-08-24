// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace DataStandardRepository.Enums;

/// <summary>
///     Enumeration containing all header names occuring in da standard stored in Excel sheet.
/// </summary>
public enum InputIndex
{
    Unknown,
    DocumentationPart,
    IfcObjectType,
    ParentElementName,
    ElementName,
    IfcElementType,
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
    ParameterGuid,
    ASR,
    VZT,
    RTC,
    KAN,
    VOD,
    PLN,
    ESI,
    ESL,
    EPS,
    MAR,
    SHZ,
    SOZ,
    THV,
    CEV,
    FVE,
    ZTI,
    GHZ,
    AVT,
    TNS,
    _000,
    _100,
    _200,
    _300,
    _400,
    _500,
    _600,
    _700,
    _800,
    _PS
}