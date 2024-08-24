// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using DataStandardRepository.Models;
using OfficeOpenXml;

namespace DataStandardRepository.Writers;
public class ExcelWriter : IDataStandardWriter
{
    public bool WriteDataStandard(IEnumerable<ElementModel> collection, string outputFilePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        FileInfo file = new(outputFilePath);

        using ExcelPackage excel = new(file);
        excel.Compatibility.IsWorksheets1Based = true;
        var worksheet = excel.Workbook.Worksheets.First();
        int row = 4;

        //WriteHeader(worksheet);
        foreach (var element in collection.OrderBy(e => e.UniqueClassification.Keys.First()))
        {
            if (element.ParentElementModel is null)
                continue;
            row = WriteElementWithProperties(worksheet, element, row);
        }
        worksheet.Cells[$"A1:P{row}"].AutoFitColumns();

        if (File.Exists(outputFilePath))
            File.Delete(outputFilePath);
        File.WriteAllBytes(outputFilePath, excel.GetAsByteArray());

        return true;
    }
    /// <summary>
    ///     Writes static header to first row in given Excel worksheet.
    /// </summary>
    /// <param name="worksheet">Target Excel worksheet in Excel workbook.</param>
    private static void WriteHeader(ExcelWorksheet worksheet)
    {
        worksheet.Cells[1, 1].Value = "IfcObjectType";
        worksheet.Cells[1, 2].Value = "Object";
        worksheet.Cells[1, 3].Value = "IfcElementType";
        worksheet.Cells[1, 4].Value = "Element";
        worksheet.Cells[1, 5].Value = "Skupina parametrů";
        worksheet.Cells[1, 6].Value = "Parametr";
        worksheet.Cells[1, 7].Value = "Popis parametru";
        worksheet.Cells[1, 8].Value = "Datový typ";
        worksheet.Cells[1, 9].Value = "Jednotka";
        worksheet.Cells[1, 10].Value = "DUR";
        worksheet.Cells[1, 11].Value = "DSP";
        worksheet.Cells[1, 12].Value = "PDPS";
        worksheet.Cells[1, 13].Value = "DSPS";
        worksheet.Cells[1, 14].Value = "IFC název";
        worksheet.Cells[1, 15].Value = "Revit - systémové parametry";
        worksheet.Cells[1, 16].Value = "Revit - sdílené parametry";
        worksheet.Cells[1, 17].Value = "Guid";
        worksheet.Cells["A1:P1"].AutoFilter = true;
    }

    /// <summary>
    ///     Writes validation information about element into given worksheet row.
    /// </summary>
    /// <param name="worksheet">Target Excel worksheet in Excel workbook.</param>
    /// <param name="ifcElement">Validated element with validation result.</param>
    /// <param name="row">Row number to write the information.</param>
    private static int WriteElementWithProperties(ExcelWorksheet worksheet, ElementModel element, int row)
    {
        if (element.ElementParameters is null)
            return row;
        foreach (var elParameter in element.ElementParameters.OrderBy(x => x.Parameter.Name))
        {
            worksheet.Cells[row, 1].Value = element.ParentElementModel is null
                ? string.Empty
                : element.ParentElementModel.Classification;
            worksheet.Cells[row, 2].Value = element.ParentElementModel is null
                ? string.Empty
                : element.ParentElementModel.Name;
            worksheet.Cells[row, 3].Value = element.Classification;
            worksheet.Cells[row, 4].Value = element.Name;
            worksheet.Cells[row, 5].Value = elParameter.Parameter.Group;
            worksheet.Cells[row, 6].Value = elParameter.Parameter.Name;
            worksheet.Cells[row, 7].Value = elParameter.Parameter.Description;
            worksheet.Cells[row, 8].Value = elParameter.Parameter.DataType.ToString();
            worksheet.Cells[row, 9].Value = elParameter.Parameter.Unit;

            foreach (var stage in elParameter.ProjectStages)
            {
                foreach(var proffesion in stage.Professions)
                {
                    var stageAndProffesion = $"{stage.ProjectStage}:{proffesion}";
                    var col = GetCorrespondingColumn(stageAndProffesion, worksheet);
                    if (col == 0)
                        continue;
                    worksheet.Cells[row, col].Value = "x";
                }
            }

            row++;
        }
        return row;
    }
    private static int GetCorrespondingColumn(string code, ExcelWorksheet worksheet)
    {
        var index = 1;
        while (worksheet.Cells[1, index].Value is not null)
        {
            if (worksheet.Cells[1, index].Value.ToString() == code)
                return index;
            index++;
        }
        return 0;
    }
}
