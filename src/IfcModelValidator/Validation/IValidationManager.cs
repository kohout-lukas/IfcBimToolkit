// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using IfcModelValidator.Models;

namespace IfcModelValidator.Validation;
public interface IValidationManager
{
    IEnumerable<ValidationResult> GetValidation(bool withPsets);
}
