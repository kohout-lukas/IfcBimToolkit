// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

#region
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
