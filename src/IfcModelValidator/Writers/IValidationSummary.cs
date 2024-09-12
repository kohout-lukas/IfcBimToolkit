// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using IfcModelValidator.Models;

namespace IfcModelValidator.Writers;
public interface IValidationSummary
{
    Task<bool> Write(string outputFilePath, IEnumerable<ValidationResult> output);
}
