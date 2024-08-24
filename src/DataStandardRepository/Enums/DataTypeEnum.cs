// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace DataStandardRepository.Enums;

/// <summary>
///     Enumeration containing all data types for data standard properties.
/// </summary>
public enum DataType
{
    String,
    Number,
    Integer,
    Boolean,
    Date,
    Money,
    Url
}