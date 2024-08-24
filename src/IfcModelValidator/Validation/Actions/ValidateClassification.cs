// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using DataStandardRepository.Models;

#endregion

namespace IfcModelValidator.Validation.Actions;

public class ValidateClassification
{
    /// <summary>
    ///     Validates sorting code of the IFC element in database with data standard in database.
    /// </summary>
    /// <param name="ifcObjectType"></param>
    /// <param name="ifcElementType"></param>
    /// <param name="collection"></param>
    /// <returns>true if IFC element sorting code is in the database.</returns>
    public static ElementModel Validate(string ifcObjectType, string ifcElementType, IEnumerable<ElementModel> collection)
    {
        if (string.IsNullOrEmpty(ifcElementType) || string.IsNullOrEmpty(ifcObjectType))
            return new ElementModel();

        return collection
            .Where(x => x.UniqueClassification.Keys.First() == ifcObjectType)
            .FirstOrDefault(x => x.UniqueClassification.Values.First() == ifcElementType, new ElementModel());
    }
    /// <summary>
    ///     Validates sorting code of the IFC element in database with data standard in database.
    /// </summary>
    /// <param name="ifcObjectType"></param>
    /// <param name="ifcElementType"></param>
    /// <param name="collection"></param>
    /// <returns>true if IFC element sorting code is in the database.</returns>
    public static ElementModel Validate(string ifcObjectType, string ifcElementType, Dictionary<string, List<ElementModel>> collection)
    {
        if (string.IsNullOrEmpty(ifcElementType) || string.IsNullOrEmpty(ifcObjectType))
            return new ElementModel();
        if (!collection.TryGetValue(ifcObjectType, out List<ElementModel>? value))
            return new ElementModel();

        return value.FirstOrDefault(x => x.UniqueClassification.Values.First() == ifcElementType, new ElementModel());
    }
    /// <summary>
    ///     Validates sorting code of the IFC element in database with data standard in database.
    /// </summary>
    /// <param name="ifcElementType"></param>
    /// <param name="collection"></param>
    /// <returns>true if IFC element sorting code is in the database.</returns>
    public static ElementModel Validate(string ifcElementType, Dictionary<string, List<ElementModel>> collection)
    {
        if (string.IsNullOrEmpty(ifcElementType))
            return new ElementModel();

        return collection
            .SelectMany(x => x.Value)
            .FirstOrDefault(x => x.UniqueClassification.Values.First() == ifcElementType, new ElementModel());
    }
}
