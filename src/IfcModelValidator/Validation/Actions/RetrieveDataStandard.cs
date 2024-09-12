// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

#region
using DataStandardRepository.Enums;
using DataStandardRepository.Models;

#endregion

namespace IfcModelValidator.Validation.Actions;

/// <summary>
///     Logic for retrieving parts of data standard from database.
/// </summary>
public static class RetrieveDataStandard
{
    /// <summary>
    ///     Gets all parameters from element model.
    /// </summary>
    /// <param name="elementModel">Element model containing information.</param>
    /// <returns>collection of element parameters.</returns>
    public static IEnumerable<ParameterModel> GetAllElementParameters(ElementModel elementModel)
    {
        if (elementModel.ElementParameters is null)
            throw new ArgumentNullException(nameof(elementModel), "Element has no parameters.");

        return elementModel.ElementParameters
            .Select(p => p.Parameter)
            .DistinctBy(x => x.Name);
    }

    /// <summary>
    ///     Gets all parameters from element model for given project stage.
    /// </summary>
    /// <param name="elementModel">Element model containing information.</param>
    /// <param name="stage">Project stage to filter element parameters.</param>
    /// <returns>collection of element parameters.</returns>
    public static IEnumerable<ParameterModel> GetAllElementParameters(ElementModel elementModel, ProjectStage stage)
    {
        if (elementModel.ElementParameters is null)
            throw new ArgumentNullException(nameof(elementModel), "Element has no parameters.");

        return elementModel.ElementParameters
            .Where(x => x.ProjectStages.Any(s => s.ProjectStage == stage))
            .Select(x => x.Parameter)
            .DistinctBy(x => x.Name);
    }
    /// <summary>
    ///     Gets all parameters from element model for given project stage.
    /// </summary>
    /// <param name="elementModel">Element model containing information.</param>
    /// <param name="stage">Project stage to filter element parameters.</param>
    /// <param name="proffession">Proffesion for given element and project stage.</param>
    /// <returns>collection of element parameters.</returns>
    public static IEnumerable<ParameterModel> GetAllElementParameters(ElementModel elementModel, ProjectStage stage, Profession proffession)
    {
        if (elementModel.ElementParameters is null)
            throw new ArgumentNullException(nameof(elementModel), "Element has no parameters.");

        return elementModel.ElementParameters
            .Where(x => x.ProjectStages.Any(s => s.ProjectStage == stage)
                    && x.ProjectStages.Any(s => s.Professions.Any(p => p == proffession)))
            .Select(x => x.Parameter)
            .DistinctBy(x => x.Name);
    }
}
