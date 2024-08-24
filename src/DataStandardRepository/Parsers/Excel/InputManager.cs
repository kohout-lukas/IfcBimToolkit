// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using DataStandardRepository.Factories;
using DataStandardRepository.Models;
using OfficeOpenXml;
using System.Linq;

#endregion

namespace DataStandardRepository.Parsers.Excel;

/// <summary>
///     Logic for reading data standard information from pre-processed Excel file.
/// </summary>
public class InputManager(string inputFilePath, int startingRow)
{
    private readonly string _inputFilePath = inputFilePath;
    private readonly int _startingRow = startingRow;
    private static readonly List<ElementModel> _elements = [];

    /// <summary>
    ///     Retrieves all lines of data standard from given Excel file.
    /// </summary>
    /// <param name="inputFilePath">File path of the input Excel file.</param>
    /// <returns>collection of elements to further work with.</returns>
    public IEnumerable<ElementModel> GetAllElements()
    {
        FileInfo file = new(_inputFilePath);
        using ExcelPackage package = new(file);

        package.Compatibility.IsWorksheets1Based = true;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        foreach(var worksheet in package.Workbook.Worksheets)
        {
            // ParseExcelWorksheet(worksheet, _startingRow);
            ParseExcelWorksheet(worksheet, 3);
        }
        return _elements;
    }

    private static void ParseExcelWorksheet(ExcelWorksheet worksheet, 
        int row)
    {
        InputParser parser = new(worksheet);
        var commonParameters = parser.GetCommonParameters(row);
        while (worksheet.Cells[row, 1].Value is not null)
        {
            if (worksheet.Cells[row, 1].Value.ToString() == "XX")
            {
                row++;
                continue;
            }
            var parameterExists = parser.GetValueAsString(row, InputIndex.ParameterName) != string.Empty;
            if (!parameterExists)
            {
                row++;
                continue;
            }
            
            var ifcObjectType = parser.GetValueAsString(row, InputIndex.IfcObjectType);
            var ifcElementType = parser.GetValueAsString(row, InputIndex.IfcElementType);

            var elementParsed = _elements.Any(
                x => x.UniqueClassification.Keys.First() == ifcObjectType &&
                x.UniqueClassification.Values.First() == ifcElementType);          

            if (!elementParsed)
            {
                ElementParameterModel elementParameterModel = new
                (
                    ParameterFactory.Create(parser, row, DataTypeFactory.Create(parser, row)),
                    ProjectStageFactory.Create(parser, row)
                );

                var element = parameterExists
                    ? ElementFactory.Create(parser, row, elementParameterModel)
                    : ElementFactory.Create(parser, row);
                element.DocumentationPart = parser.GetValueAsString(row, InputIndex.DocumentationPart);
                element.ElementParameters = new(commonParameters);
                _elements.Add(element);
            }
            
            var parameter = ParameterFactory.Create(parser, row, DataTypeFactory.Create(parser, row));
            var projectStages = ProjectStageFactory.Create(parser, row);
            UpdateExistingElement(ifcObjectType, ifcElementType, parameter, projectStages);

            row++;
        }
    }
    private static void UpdateExistingElement(string ifcObjectType, 
        string ifcElementType, 
        ParameterModel parameter,
        IEnumerable<ProjectStageModel> projectStages)
    {
        var elementToUpdate = _elements.First(
                x => x.UniqueClassification.Keys.First() == ifcObjectType &&
                x.UniqueClassification.Values.First() == ifcElementType);

        var parameterExists = elementToUpdate.ElementParameters is not null
            && elementToUpdate.ElementParameters.Any(x => x.Parameter.Name == parameter.Name);
        if (!parameterExists)
        {
            var param = new ElementParameterModel(parameter,projectStages);
            elementToUpdate.ElementParameters!.Add(param);
            return;
        }

        var existingParameter = elementToUpdate.ElementParameters!.First(x => x.Parameter.Name == parameter.Name);
        foreach (var stage in projectStages)
        {
            var p = stage.ProjectStage;
            if(existingParameter.ProjectStages.Any(x => x.ProjectStage == p))
            {
                var existingProjectStage = existingParameter.ProjectStages.First(x => x.ProjectStage == p);
                existingProjectStage.Professions.UnionWith(stage.Professions);
                continue;
            }
            existingParameter.ProjectStages.Add(stage);
        }
    }
}
