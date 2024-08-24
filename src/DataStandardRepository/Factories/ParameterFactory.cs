// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using DataStandardRepository.Models;
using DataStandardRepository.Parsers;

#endregion

namespace DataStandardRepository.Factories;

public static class ParameterFactory
{
    /// <summary>
    ///     Creates one parameter model from given input row.
    /// </summary>
    /// <param name="rowNumber">Number of row to get data from.</param>
    /// <param name="dataType">DataTypeMod for use when defining ParameterModel.</param>
    /// <returns>created ParameterModel based on given input row.</returns>
    public static ParameterModel Create(IInputParser parser, int rowNumber, DataType dataType)
    {
        var unitName = parser.GetValueAsString(rowNumber, InputIndex.ParameterUnit) is not null
                        ? parser.GetValueAsString(rowNumber, InputIndex.ParameterUnit)
                        : string.Empty;
        ParameterModel parameterModel = new
        (
            parser.GetValueAsString(rowNumber, InputIndex.ParameterName)
                ?? throw new NullReferenceException($"Null value on row{rowNumber} in column ParameterName"),
            parser.GetValueAsString(rowNumber, InputIndex.ParameterGroup)
                    ?? throw new NullReferenceException($"Null value on row{rowNumber} in column ParameterGroup"),
            dataType,
            unitName != string.Empty
                    ? parser.GetValueAsString(rowNumber, InputIndex.ParameterUnit)
                    : "TEXT")
        {
            Description = parser.GetValueAsString(rowNumber, InputIndex.ParameterDescription),
            IfcName = parser.GetValueAsString(rowNumber, InputIndex.ParameterIfc),
            RevitInternalParameterName = parser.GetValueAsString(rowNumber, InputIndex.ParameterRevitInternal),
            RevitSharedParameterName = parser.GetValueAsString(rowNumber, InputIndex.ParameterRevitShared),
            Guid = parser.GetValueAsString(rowNumber, InputIndex.ParameterGuid)
        };

        return parameterModel;
    }
}