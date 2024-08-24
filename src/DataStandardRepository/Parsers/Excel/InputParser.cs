// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using DataStandardRepository.Factories;
using DataStandardRepository.Models;
using OfficeOpenXml;

namespace DataStandardRepository.Parsers.Excel;
public class InputParser : IInputParser
{
    /// <summary>
    ///     Parsed header types with corresponding column indexes.
    /// </summary>
    private readonly Dictionary<int, InputIndex[]> _headerIndexesAndTypes;

    private readonly ExcelWorksheet _worksheet;

    public InputParser(ExcelWorksheet worksheet)
    {
        _worksheet = worksheet;
        _headerIndexesAndTypes = GetInputIndexes();
    }

    /// <summary>
    ///     Retrieving all common parameter for all elements in data standard. Common parameters are marked with "XX" in first
    ///     column.
    /// </summary>
    /// <returns>collection of element parameters.</returns>
    public IEnumerable<ElementParameterModel> GetCommonParameters(int identifier)
    {
        List<ElementParameterModel> commonParameters = [];

        var rowNumber = identifier;
        while (_worksheet is not null && _worksheet.Cells[rowNumber, 1].Value.ToString() == "XX")
        {
            commonParameters.Add(new ElementParameterModel
            (
                ParameterFactory.Create(this, rowNumber, DataTypeFactory.Create(this, rowNumber)),
                ProjectStageFactory.Create(this, rowNumber)
            ));
            rowNumber++;
        }

        return commonParameters;
    }
    /// <summary>
    ///     Gets column header enumeration for given Excel worksheet. Header types and its position is stored.
    /// </summary>
    private Dictionary<int, InputIndex[]> GetInputIndexes()
    {
        var columnNumber = 1;
        Dictionary<int, InputIndex[]> headerIndexesAndTypes = [];

        while (_worksheet is not null && _worksheet.Cells[1, columnNumber].Value is not null)
        {
            var columnName = GetValueAsString(1, columnNumber);
            if (columnName.Contains(':'))
            {
                var names = columnName.Split(':');
                var input1 = Enum.TryParse(names[0], true, out InputIndex inputIndex1) ? inputIndex1 : InputIndex.Unknown;
                var input2 = Enum.TryParse(names[1], true, out InputIndex inputIndex2) ? inputIndex2 : InputIndex.Unknown;
                headerIndexesAndTypes.Add(columnNumber, [input1, input2]);
            }
            else
            {
                var input = Enum.TryParse(columnName, true, out InputIndex inputIndex) ? inputIndex : InputIndex.Unknown;
                headerIndexesAndTypes.Add(columnNumber, [input]);
            }

            columnNumber++;
        }
        return headerIndexesAndTypes;
    }

    /// <summary>
    ///     Gets single cell value as string.
    /// </summary>
    /// <param name="rowNumber">Cell row number.</param>
    /// <param name="columnName">Column name from ColumnHeader enumeration.</param>
    /// <returns>cell's string value.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public string GetValueAsString(int rowNumber, InputIndex columnName)
    {
        var columnNumber = _headerIndexesAndTypes
            .FirstOrDefault(x => x.Value.Contains(columnName))
            .Key;

        if (columnNumber == 0 || _worksheet == null)
            return string.Empty;

        return _worksheet.Cells[rowNumber, columnNumber].Value is not null
            ? _worksheet.Cells[rowNumber, columnNumber].Value.ToString()!
            : string.Empty;
    }
    /// <summary>
    ///     Gets single cell value as string.
    /// </summary>
    /// <param name="rowNumber">Cell row number.</param>
    /// <param name="first">Column name from ColumnHeader enumeration.</param>
    /// <param name="second">Column name from ColumnHeader enumeration.</param>
    /// <returns>cell's string value.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public string GetValueAsString(int rowNumber, InputIndex first, InputIndex second)
    {
        var columnNumber = _headerIndexesAndTypes
            .FirstOrDefault(x => x.Value[0] == first && x.Value[1] == second)
            .Key;

        if (columnNumber == 0 || _worksheet == null)
            return string.Empty;

        return _worksheet.Cells[rowNumber, columnNumber].Value is not null
            ? _worksheet.Cells[rowNumber, columnNumber].Value.ToString()!
            : string.Empty;
    }

    /// <summary>
    ///     Gets single cell value as string.
    /// </summary>
    /// <param name="rowNumber">Cell row number.</param>
    /// <param name="columnNumber">Cell column number.</param>
    /// <returns>cell's string value.</returns>
    /// <exception cref="Exception"></exception>
    private string GetValueAsString(int rowNumber, int columnNumber)
    {
        if (_worksheet is null) throw new Exception("Worksheet contains no data.");

        return _worksheet.Cells[rowNumber, columnNumber].Value.ToString()!;
    }
}
