// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using IfcModelValidator.Models;

namespace IfcModelValidator.Writers;
public interface IValidationSummary
{
    Task<bool> Write(string outputFilePath, IEnumerable<ValidationResult> output);
}
