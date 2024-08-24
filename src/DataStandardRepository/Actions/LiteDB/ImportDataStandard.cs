// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using DataStandardRepository.Models;
using LiteDB;

#endregion

namespace DataStandardRepository.Actions.LiteDB;

/// <summary>
///     Logic for complete import of data standard from Excel file. Data are structured and stored in LiteDB database.
/// </summary>
public class ImportDataStandard(string databaseFilePath, IEnumerable<ElementModel> data)
{
    private readonly string _databaseFilePath = databaseFilePath;
    private readonly IEnumerable<ElementModel> _data = data;

    /// <summary>
    ///     Sequence logic for importing complete data standard from Excel file
    /// </summary>    
    /// <returns>true if process was successful.</returns>
    public bool CreateCompleteDataStandard()
    {
        if (_data is null || !_data.Any())
            return false;

        CreateAllParentElements();
        CreateAllParameters();
        CreateAllElementsWithData();

        return true;
    }

    /// <summary>
    ///     Creates all parent elements in the database.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private void CreateAllParentElements()
    {
        var parentElements = _data
            .Where(x => x.ParentElementModel is not null)
            .Select(x => x.ParentElementModel)
            .GroupBy(x =>
            {
                if (x != null)
                    return new { x.Name, x.Classification };
                throw new InvalidOperationException();
            })
            .Select(x => x.First());

        var elementsWithoutParent = _data
            .Where(x => x.ParentElementModel is null)
            .GroupBy(x => new { x.Name, x.Classification })
            .Select(x => x.First());

        var allParents = parentElements.Concat(elementsWithoutParent)
            .GroupBy(x =>
            {
                if (x != null)
                    return new { x.Name, x.Classification };
                throw new InvalidOperationException();
            })
            .Select(x => x!.First())
            .OrderBy(x =>
            {
                if (x != null)
                    return x.Name;
                throw new InvalidOperationException();
            });

        if (!allParents.Any())
            return;

        using LiteDatabase db = new(_databaseFilePath);
        var elementCollection = db.GetCollection<ElementModel>("elements");
        foreach (var parent in allParents)
        {
            if (parent is not null && parent.Name == "XX")
                continue;
            CreateParentElementInDb(elementCollection, parent ?? throw new InvalidOperationException());
        }
    }

    /// <summary>
    ///     Creates all parameters in the database.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    private void CreateAllParameters()
    {
        if (!ElementsInCollectionHasParameters())
            return;

        var allParameters = _data
            .SelectMany(x => x.ElementParameters ?? throw new InvalidOperationException())
            .Select(x => x.Parameter)
            .GroupBy(x => x.Name)
            .Select(x => x.First())
            .Where(x => x.Name is not "")
            .OrderBy(x => x.Name);

        using LiteDatabase db = new(_databaseFilePath);
        var parameterCollection = db.GetCollection<ParameterModel>("parameters");

        List<string> createdParameterNames = [];
        if (createdParameterNames == null)
            throw new ArgumentNullException(nameof(createdParameterNames));

        foreach (var parameter in allParameters)
        {
            if (!createdParameterNames.Contains(parameter?.Name ?? string.Empty))
            {
                CreateParameterInDb(parameterCollection,
                    ref createdParameterNames,
                    parameter ?? throw new InvalidOperationException());
            }
        }
    }

    /// <summary>
    ///     Creates all elements with all data in the database.
    /// </summary>
    private void CreateAllElementsWithData()
    {
        using LiteDatabase db = new(_databaseFilePath);

        var elementCollection = db.GetCollection<ElementModel>("elements");
        var parameterCollection = db.GetCollection<ParameterModel>("parameters");

        foreach (var element in _data)
        {
            if (element.Name == "XX")
                continue;
            if (element.ElementParameters is not null)
                UpdateElementParameterWithDbIds(parameterCollection, element);
            if (element.ParentElementModel == null)
            {
                UpdateElementInDb(elementCollection, element);
                continue;
            }
            CreateElementInDb(elementCollection, element);
        }
    }

    /// <summary>
    ///     Checks if given element has associated parameters.
    /// </summary>
    /// <returns>true if at least one connected parameter is found.</returns>
    private bool ElementsInCollectionHasParameters()
    {
        return _data.Any(x => x.ElementParameters is not null);
    }

    /// <summary>
    ///     Inserts one parent element into database collection.
    /// </summary>
    /// <param name="elementCollection">Element collection in database. Creates one if collection does not exist.</param>
    /// <param name="parent">Parent element model to be inserted into database.</param>
    private static void CreateParentElementInDb(ILiteCollection<ElementModel> elementCollection, ElementModel parent)
    {
        if (parent is not null)
        {
            ElementModel elementModel = new(parent.Name!, string.Empty, parent.Classification, parent.DocumentationPart!);
            elementCollection.Insert(elementModel);
        }
    }

    /// <summary>
    ///     Inserts one element into database collection.
    /// </summary>
    /// <param name="elementCollection">Element collection in database. Creates one if collection does not exist.</param>
    /// <param name="elementModel">Element model to be inserted into database.</param>
    private static void CreateElementInDb(ILiteCollection<ElementModel> elementCollection,
        ElementModel elementModel)
    {
        if (elementModel.ParentElementModel is null)
            return;

        elementModel.ParentElementModel.Id = elementCollection.Query()
            .Where(x => x.Classification == elementModel.ParentElementModel.Classification)
            .First()
            .Id;
        elementCollection.Insert(elementModel);
    }

    /// <summary>
    ///     Updates existing element model in database collection with element parameters.
    /// </summary>
    /// <param name="elementCollection">Element collection in database. Creates one if collection does not exist.</param>
    /// <param name="elementModel">Element model to be updated in database.</param>
    private static void UpdateElementInDb(ILiteCollection<ElementModel> elementCollection,
        ElementModel elementModel)
    {
        var elementId = elementCollection.Query()
            .Where(x => x.Classification == elementModel.Classification)
            .First()
            .Id;
        if (elementModel.ElementParameters is null) 
            return;

        var elToUpdate = elementCollection.FindById(elementId);
        elToUpdate.ElementParameters ??= [];
        elToUpdate.ElementParameters.AddRange(elementModel.ElementParameters);

        elementCollection.Update(elToUpdate);
    }

    /// <summary>
    ///     Updates element parameters with ids obtained from the database parameter collection.
    /// </summary>
    /// <param name="parameterCollection">Parameter collection in database.</param>
    /// <param name="elementModel">Element model to be updated in database</param>
    private static void UpdateElementParameterWithDbIds(ILiteCollection<ParameterModel> parameterCollection,
        ElementModel elementModel)
    {
        if (elementModel.ElementParameters is null)
            return;
        foreach (var elementParameter in elementModel.ElementParameters)
        {
            UpdateParameterWithDbId(parameterCollection, elementParameter);
        }
    }

    /// <summary>
    ///     Inserts one parameter into database collection.
    /// </summary>
    /// <param name="parameterCollection">Parameter collection in database. Creates one if collection does not exist.</param>
    /// <param name="createdParameterNames">Collection of all parameter names created in the database collection.</param>
    /// <param name="parameterModel">Parameter model to be inserted into database.</param>
    private static void CreateParameterInDb(ILiteCollection<ParameterModel> parameterCollection,
        ref List<string> createdParameterNames, ParameterModel parameterModel)
    {
        parameterCollection.Insert(parameterModel);
        createdParameterNames.Add(parameterModel.Name);
    }

    /// <summary>
    ///     Updates parameter with id obtained from the database parameter collection.
    /// </summary>
    /// <param name="parameterCollection">Parameter collection in database.</param>
    /// <param name="elementParameterModel">Element parameter model to be inserted into database.</param>
    private static void UpdateParameterWithDbId(ILiteCollection<ParameterModel> parameterCollection,
        ElementParameterModel elementParameterModel)
    {
        var parameter = parameterCollection.Query()
            .Where(x => elementParameterModel.Parameter != null && x.Name == elementParameterModel.Parameter.Name)
            .FirstOrDefault();
        if (elementParameterModel.Parameter != null && parameter != null)
            elementParameterModel.Parameter.Id = parameter.Id;
    }
}
