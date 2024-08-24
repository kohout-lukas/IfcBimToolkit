// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using DataStandardRepository.Actions.LiteDB;
using DataStandardRepository.Enums;
using DataStandardRepository.Models;
using IfcModelValidator.Handlers.Ifc;
using IfcModelValidator.Models;
using IfcModelValidator.Validation.Actions;
using LiteDB;
using Xbim.Ifc4.Interfaces;

#endregion

namespace IfcModelValidator.Validation;

/// <summary>
///     Logic for validating IFC elements against data standard.
/// </summary>
/// <remarks>
///     Base class for validating IFC elements against data standard.
/// </remarks>
/// <param name="ifcElements">Collection of IFC elements to validate against data standard.</param>
public class IdsValidationManager(IEnumerable<IIfcElement> ifcElements,
    string idsFilePath) : IValidationManager
{
    /// <summary>
    ///     IFC element collection to be validated against data standard.
    /// </summary>
    private readonly IEnumerable<IIfcElement> _ifcElements = ifcElements;
    private readonly string _idsFilePath = idsFilePath;

    /// <summary>
    ///     Validates every IFC element in class instance collection against data standard database,
    /// </summary>
    /// <param name="withPsets">True when validation should check property sets.</param>
    /// <returns>collection of elements with respective result of validation.</returns>
    public IEnumerable<ValidationResult> GetValidation(bool withPsets)
    {
        // TODO
        return [];
    }
}
