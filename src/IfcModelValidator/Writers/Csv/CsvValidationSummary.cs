// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using CsvHelper;
using CsvHelper.Configuration;
using IfcModelValidator.Handlers.Files;
using IfcModelValidator.Models;
using System.Globalization;
using System.Text;

#endregion

namespace IfcModelValidator.Writers.Csv;

/// <summary>
///     Logic for writing validation results into Excel file.
/// </summary>
public class CsvValidationSummary : IValidationSummary
{
    /// <summary>
    ///     Writes validation into Excel file.
    /// </summary>
    /// <param name="outputFilePath">Output Excel file path.</param>
    /// <param name="output">Collection of validated elements with validation result.</param>
    /// <returns>true if process was successful.</returns>
    public async Task<bool> Write(string outputFilePath, IEnumerable<ValidationResult> output)
    {
        var outputCsvFilePath = outputFilePath.Replace(".ifc", ".csv");
        FileManipulation.DeleteAndCreate(outputCsvFilePath);
        var writer = new StreamWriter(outputCsvFilePath, false, Encoding.UTF8);
        var writeConfiguration = new CsvConfiguration(CultureInfo.GetCultureInfo("cs-CZ"))
        {
            Mode = CsvMode.NoEscape
        };
        using var csv = new CsvWriter(writer, writeConfiguration);
        csv.WriteHeader<ValidationResult>();
        csv.NextRecord();
        await csv.WriteRecordsAsync(output);
        return true;
    }
}
