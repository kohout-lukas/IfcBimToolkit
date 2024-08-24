// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region
using DataStandardRepository.Models;
using DataStandardRepository.Parsers;

#endregion

namespace DataStandardRepository.Factories;

public static class DataTypeFactory
{
    /// <summary>
    ///     Creates one data type model from given input row.
    /// </summary>
    /// <param name="rowNumber">Number of row to get data from.</param>
    /// <returns>created DataTypeModel based on given input row.</returns>
    public static DataType Create(IInputParser parser, int rowNumber)
    {
        var dataTypeString = parser.GetValueAsString(rowNumber, InputIndex.ParameterDataType);
        var dataType = dataTypeString is not null
            ? (DataType)Enum.Parse(typeof(DataType), dataTypeString, true)
            : DataType.String;

        return dataType;
    }
}
