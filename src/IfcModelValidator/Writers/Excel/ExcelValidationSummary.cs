// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using IfcModelValidator.Handlers.Files;
using IfcModelValidator.Models;
using OfficeOpenXml;

#endregion

namespace IfcModelValidator.Writers.Excel;

/// <summary>
///     Logic for writing validation results into Excel file.
/// </summary>
public class ExcelValidationSummary : IValidationSummary
{
    /// <summary>
    ///     Writes validation into Excel file.
    /// </summary>
    /// <param name="outputFilePath">Output Excel file path.</param>
    /// <param name="output">Collection of validated elements with validation result.</param>
    /// <returns>true if process was successful.</returns>
    public async Task<bool> Write(string outputFilePath, IEnumerable<ValidationResult> output)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var outputExcelFilePath = outputFilePath.Replace(".ifc", ".xlsx");

        using ExcelPackage excel = new();
        excel.Compatibility.IsWorksheets1Based = true;
        var worksheetName = outputExcelFilePath
            .Split("\\")
            .Last()
            .Replace(".xlsx", "");

        var worksheet = excel.Workbook.Worksheets.Add(worksheetName);
        var row = 2;

        WriteHeader(worksheet);

        foreach (var element in output)
        {
            if (element.ValidSortingCode == "ANO" && element.Properties is null)
                continue;
            WriteElementWithProperties(worksheet, element, row);
            row++;
        }
        FileManipulation.DeleteAndCreate(outputExcelFilePath);
        await Task.Run(() => File.WriteAllBytes(outputExcelFilePath, excel.GetAsByteArray()));
        return true;
    }

    /// <summary>
    ///     Writes static header to first row in given Excel worksheet.
    /// </summary>
    /// <param name="worksheet">Target Excel worksheet in Excel workbook.</param>
    private static void WriteHeader(ExcelWorksheet worksheet)
    {
        worksheet.Cells[1, 1].Value = "Název prvku v IFC";
        worksheet.Cells[1, 2].Value = "IfcType";
        worksheet.Cells[1, 3].Value = "IfcGlobalId";
        worksheet.Cells[1, 4].Value = "Skupina elementů";
        worksheet.Cells[1, 5].Value = "Označení elementu";
        worksheet.Cells[1, 6].Value = "Klasifikace dle DS";
        worksheet.Cells[1, 7].Value = "Chybějící vlastnosti";
        worksheet.Cells["A1:G1"].AutoFilter = true;
    }

    /// <summary>
    ///     Writes validation information about element into given worksheet row.
    /// </summary>
    /// <param name="worksheet">Target Excel worksheet in Excel workbook.</param>
    /// <param name="result">Validated element with validation result.</param>
    /// <param name="row">Row number to write the information.</param>
    private static bool WriteElementWithProperties(ExcelWorksheet worksheet,
        ValidationResult result, int row)
    {
        worksheet.Cells[row, 1].Value = result.Name;
        worksheet.Cells[row, 2].Value = result.IfcType;
        worksheet.Cells[row, 3].Value = result.IfcGlobalId;
        worksheet.Cells[row, 4].Value = result.IfcObjectType;
        worksheet.Cells[row, 5].Value = result.IfcElementType;
        worksheet.Cells[row, 6].Value = result.ValidSortingCode;
        if (result.Properties is not null)
            worksheet.Cells[row, 7].Value = string.Join(";", result.Properties);
        worksheet.Cells[$"A1:H{row}"].AutoFitColumns();
        return true;
    }
}
