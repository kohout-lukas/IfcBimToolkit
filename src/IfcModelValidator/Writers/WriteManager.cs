// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using IfcModelValidator.Models;

#endregion

namespace IfcModelValidator.Writers;

/// <summary>
///     Logic for different possible output methods (file formats, structures etc.).
/// </summary>
/// <remarks>
///     Write manager for different possible output methods (file formats, structures etc.).
/// </remarks>
/// <param name="outputFilePath">Target file path to save output into.</param>
public class WriteManager(string outputFilePath, IValidationSummary validationSummary)
{
    private readonly string _outputFilePath = outputFilePath;
    private readonly IValidationSummary _validationSummary = validationSummary;

    /// <summary>
    ///     Writes validation summary into Excel file.
    /// </summary>
    /// <param name="output">Output to write.</param>
    /// <returns>true if process is successful.</returns>
    public bool WriteValidationSummary(IEnumerable<ValidationResult> output)
    {
        return _validationSummary.Write(_outputFilePath, output).Result;
    }
}
