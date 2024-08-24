// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using DataStandardRepository.Enums;
using DataStandardRepository.Models;
using System.Collections.Generic;
using System.Xml.Linq;
using Xbim.Ifc4.Interfaces;

#endregion

namespace IfcModelValidator.Validation.Actions;

public static class ValidateProperties
{
    /// <summary>
    ///     Validates parameters of the IFC element in database with data standard in database.
    ///     User decides if PropertySets should be checked.
    /// </summary>
    /// <param name="ifcElement">IFC element to check.</param>
    /// <param name="uniqueClassification">Complete classification for element.</param>
    /// <param name="withPsets">User input to decide if PropertySets should be checked.</param>
    /// <returns>collection of parameters that are not present in the IFC elemment.</returns>
    public static List<string> Validate(List<(IIfcProperty, bool)> ifcProperties,
        ElementModel elementModel,
        bool withPsets,
        ProjectStage projectStage,
        Profession proffession)
    {
        try
        {
            var parameters = new List<ParameterModel>();

            if (projectStage == ProjectStage.None)
            {
                parameters.AddRange(RetrieveDataStandard.GetAllElementParameters(elementModel));
            }
            else if (proffession == Profession.None)
            {
                parameters.AddRange(RetrieveDataStandard.GetAllElementParameters(elementModel, projectStage));
            }
            else
            {
                parameters.AddRange(RetrieveDataStandard.GetAllElementParameters(elementModel, projectStage, proffession));
            }

            return withPsets
                ? ValidateWithPset(ifcProperties, parameters.Select(x => (x.Group, x.Name)))
                : ValidateWithoutPset(ifcProperties, parameters.Select(x => x.Name));
        }
        catch (Exception)
        {
            return [];
        }
    }
    /// <summary>
    ///     Validates parameters of the IFC element in database with data standard in database. Validation is without
    ///     PropertySet name.
    /// </summary>
    /// <param name="ifcProperties">IFC element to check.</param>
    /// <param name="parameters">Collection of parameters from adata standard database.</param>
    /// <returns>true if every relevant parameter from database collection is present int he IFC element.</returns>
    private static List<string> ValidateWithoutPset(List<(IIfcProperty, bool)> ifcProperties,
        IEnumerable<string> parameters)
    {
        List<string> result = [];
        foreach (var parameter in parameters)
        {
            //var exists = ifcElementModel.Properties.Any(p => p.Name.Trim().ToLower() == parameter.ToLower());

            var exists = ifcProperties.Any(p => p.Item1.Name.ToString().Trim().Contains(parameter));
            //var exists = ifcProperties.Any(p => p.Item1.Name.ToString().Trim() == parameter);
            if (!exists)
                result.Add(parameter);
        }
        return result;
    }

    /// <summary>
    ///     Validates parameters of the IFC element in database with data standard in database. Validation is with PropertySet
    ///     name.
    /// </summary>
    /// <param name="ifcProperties">IFC element to check.</param>
    /// <param name="parameters">Collection of parameters from adata standard database.</param>
    /// <returns>true if every relevant parameter from database collection is present int he IFC element.</returns>
    private static List<string> ValidateWithPset(List<(IIfcProperty, bool)> ifcProperties,
        IEnumerable<(string PropertySet, string Name)> parameters)
    {
        List<string> result = [];

        foreach (var (PropertySet, Name) in parameters)
        {
            var exists = ifcProperties.Any(x => x.Item1.Name.ToString().Trim() == Name && GetPropertySetName(x.Item1, x.Item2) == PropertySet);
            if (!exists)
                result.Add(PropertySet + "@" + Name);
        }

        return result;
    }

    private static string GetPropertySetName(IIfcProperty property, bool partOfComplex)
    {
        ArgumentNullException.ThrowIfNull(property);

        if (partOfComplex)
            return property.PartOfComplex.First().Name.ToString();
        var name = property.PartOfPset.First().Name;
        return name is null 
            ? throw new ArgumentNullException(nameof(property)) 
            : name;
    }
}
