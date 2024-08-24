// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using IfcModelValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IfcModelValidator.Validation;
public interface IValidationManager
{
    IEnumerable<ValidationResult> GetValidation(bool withPsets);
}
