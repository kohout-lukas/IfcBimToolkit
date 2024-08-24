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
public class DbValidationManager : IValidationManager
{
    /// <summary>
    ///     IFC element collection to be validated against data standard.
    /// </summary>
    private readonly IEnumerable<IIfcElement> _ifcElements;
    private readonly string _databaseFilePath;
    private readonly ProjectStage _projectStage;
    private readonly Profession _profession;
    private readonly string IfcObjectTypeName;
    private readonly string IfcElementTypeName;

    /// <summary>
    ///     Base class for validating IFC elements against data standard.
    /// </summary>
    /// <param name="ifcElements">Collection of IFC elements to validate against data standard.</param>
    public DbValidationManager(IEnumerable<IIfcElement> ifcElements,
        string databaseFilePath,
        string ifcObjectTypoName,
        string ifcElementTypeName,
        string projectStage,
        string profession)
    {
        _ifcElements = ifcElements;
        _databaseFilePath = databaseFilePath;
        _projectStage = projectStage == ""
            ? ProjectStage.None
            : Enum.Parse<ProjectStage>(projectStage, true);
        var success = Enum.TryParse(profession, out Profession _);
        _profession = success
            ? Enum.Parse<Profession>(profession)
            : Profession.None;
  
        IfcObjectTypeName = ifcObjectTypoName;
        IfcElementTypeName = ifcElementTypeName;
    }

    /// <summary>
    ///     Validates every IFC element in class instance collection against data standard database,
    /// </summary>
    /// <param name="withPsets">True when validation should check property sets.</param>
    /// <returns>collection of elements with respective result of validation.</returns>
    public IEnumerable<ValidationResult> GetValidation(bool withPsets)
    {
        var result = new List<ValidationResult>();
        using LiteDatabase db = new(_databaseFilePath);
        var collection = CollectionRetrieval<ElementModel>.GetCollection(db, "elements");
        var elementDictionary = new Dictionary<string, List<ElementModel>>();
        foreach (var element in collection)
        {
            var key = element.UniqueClassification.Keys.First();
            if (elementDictionary.TryGetValue(key, out List<ElementModel>? value))
            {
                value.Add(element);
                continue;
            }
            elementDictionary.Add(key, [element]);
        }

        foreach (var ifcElement in _ifcElements)
        {
            var typeProperties = IfcPropertyHandler.GetElementPropertiesOfType(ifcElement);
            var instanceProperties = IfcPropertyHandler.GetElementPropertiesOfInstance(ifcElement);
            var allProperties = instanceProperties.Concat(typeProperties).ToList();

            // Can apply filter to checked elements here
            if (FilterElements.FilterByParameterValues(allProperties, "Fáze vytvoření", ["Existující"]))
                continue;

            var classification = IfcPropertyHandler.GetClassification(allProperties, IfcObjectTypeName, [.. IfcElementTypeName.Split(";")]);


            if (classification is null)
            {
                ValidationResult validationResult = new()
                {
                    Name = ifcElement.Name ?? string.Empty,
                    IfcType = ifcElement.ExpressType.ToString(),
                    IfcGlobalId = ifcElement.GlobalId.ToString(),
                    ValidSortingCode = "NE",
                };
                result.Add(validationResult);
                continue;
            }

            var ifcObjectType = (string)classification.IfcObjectTypeProperty.Value;
            var ifcElementType = (string)classification.IfcElementTypeProperty.Value;

            // Standard            
            //if (ifcElementType.Length > 3)
            //{

            //    ifcObjectType = ifcElementType[..2];
            //    ifcElementType = ifcElementType.Substring(2, 2);
            //    // KKN TZB only
            //    //ifcElementType = ifcElementType.Replace("*", string.Empty);
            //    //ifcElementType = "00";
            //    // KKN TZB only
            //}

            //var elementInDb = ValidateClassification.Validate(ifcObjectType, ifcElementType, elementDictionary);

            //SFDI
            //ifcObjectType = Helpers.StringHelpers.FirstLetterToLower(ifcObjectType);
            //ifcElementType = Helpers.StringHelpers.FirstLetterToLower(ifcElementType);
            var elementInDb = string.IsNullOrEmpty(ifcObjectType)
               ? ValidateClassification.Validate(ifcElementType.Trim(), elementDictionary)
               : ValidateClassification.Validate(ifcObjectType.Trim(), ifcElementType.Trim(), elementDictionary);

            //KKN
            //if (!elementInDb.UniqueClassification.Keys.Any())
            //{
            //    elementInDb = string.IsNullOrEmpty(ifcObjectType)
            //        ? ValidateClassification.Validate(ifcElementType.Trim(), elementDictionary)
            //        : ValidateClassification.Validate(ifcObjectType.Trim(), "00", elementDictionary); 
            //}

            if (elementInDb.UniqueClassification.Keys.Count == 0)
            {
                ValidationResult validationResult = new()
                {
                    Name = ifcElement.Name ?? string.Empty,
                    IfcType = ifcElement.ExpressType.ToString(),
                    IfcGlobalId = ifcElement.GlobalId.ToString(),
                    IfcElementType = ifcElementType,
                    IfcObjectType = ifcObjectType,
                    ValidSortingCode = "NE",
                };
                result.Add(validationResult);
                continue;
            }
            

            var properties = ValidateProperties.Validate(allProperties, elementInDb, withPsets, _projectStage, _profession);

            if (properties.Count != 0)
            {
                ValidationResult validationResult = new()
                {
                    Name = ifcElement.Name ?? string.Empty,
                    IfcType = ifcElement.ExpressType.ToString(),
                    IfcGlobalId = ifcElement.GlobalId.ToString(),
                    IfcElementType = ifcElementType,
                    IfcObjectType = ifcObjectType,
                    ValidSortingCode = "ANO",
                    Properties = properties
                };
                result.Add(validationResult);
            }
        }
        return result;
    }
}
