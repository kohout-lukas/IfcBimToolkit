// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

#region

using IfcModelValidator.Models;
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
