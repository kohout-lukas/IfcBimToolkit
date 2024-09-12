// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

#region

using DataStandardRepository.Factories;
using DataStandardRepository.Models;
using OfficeOpenXml;

#endregion

namespace DataStandardRepository.Parsers.Excel;

/// <summary>
///     Logic for reading data standard information from pre-processed Excel file.
/// </summary>
public class InputManager_OLD(string inputFilePath, int startingRow)
{
    private readonly string _inputFilePath = inputFilePath;
    private readonly int _startingRow = startingRow;

    /// <summary>
    ///     Retrieves all lines of data standard from given Excel file.
    /// </summary>
    /// <param name="inputFilePath">File path of the input Excel file.</param>
    /// <returns>collection of elements to further work with.</returns>
    public IEnumerable<ElementModel> GetAllElements()
    {
        FileInfo file = new(_inputFilePath);
        List<ElementModel> elements = [];

        using ExcelPackage package = new(file);

        package.Compatibility.IsWorksheets1Based = true;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var row = _startingRow;
        var worksheet = package.Workbook.Worksheets.First();
        InputParser parser = new(worksheet);

        while (worksheet.Cells[row, 1].Value is not null)
        {
            if (worksheet.Cells[row, 1].Value.ToString() == "XX")
            {
                row++;
                continue;
            }
            var parameterExists = parser.GetValueAsString(row, InputIndex.ParameterName) != string.Empty;
            if (!parameterExists)
                continue;
            ElementParameterModel elementParameterModel = new
            (
                ParameterFactory.Create(parser, row, DataTypeFactory.Create(parser, row)),
                ProjectStageFactory.Create(parser, row)
            );
            var ifcObjectType = parser.GetValueAsString(row, InputIndex.IfcObjectType);
            var ifcElementType = parser.GetValueAsString(row, InputIndex.IfcElementType);

            var elementParsed = elements.Exists(
                x => x.UniqueClassification.Keys.First() == ifcObjectType &&
                x.UniqueClassification.Values.First() == ifcElementType);

            if (!elementParsed)
            {
                var element = parameterExists
                    ? ElementFactory.Create(parser, row, elementParameterModel)
                    : ElementFactory.Create(parser, row);
                elements.Add(element);
                row++;
                continue;
            }

            var elementToUpdate = elements.First(
                x => x.UniqueClassification.Keys.First() == ifcObjectType &&
                x.UniqueClassification.Values.First() == ifcElementType);

            if (elementToUpdate.ElementParameters != null)
            {
                if (parameterExists)
                    elementToUpdate.ElementParameters.Add(elementParameterModel);
                row++;
                continue;
            }

            if (elementToUpdate is null || elementToUpdate.ElementParameters is not null) continue;
            if (parameterExists)
                elementToUpdate.ElementParameters = [elementParameterModel];
            row++;
        }

        // var commonParameters = parser.GetCommonParameters(_startingRow);
        // elements.RemoveRange(0, commonParameters.Count());

        foreach (var element in elements)
        {
            element.ElementParameters ??= [];
            // element.ElementParameters.AddRange(commonParameters);
        }

        return elements;
    }
}
