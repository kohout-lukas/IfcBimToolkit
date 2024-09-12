// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

#region

using DataStandardRepository.Models;
using DataStandardRepository.Parsers;

#endregion

namespace DataStandardRepository.Factories;

public static class ProjectStageFactory
{
    /// <summary>
    ///     Creates one project stage model from given input row.
    /// </summary>
    /// <param name="row">Number of row to get data from.</param>
    /// <returns>created ProjectStageModel based on given input row.</returns>
    public static IEnumerable<ProjectStageModel> Create(IInputParser parser,
        int row)
    {
        var stages = new List<ProjectStageModel>();
        foreach (ProjectStage projectStage in Enum.GetValues(typeof(ProjectStage)))
        {
            var professions = GetProfessionsForProjectStage(parser, row, projectStage).ToHashSet();
            if (professions.Count != 0)
            {
                var stage = new ProjectStageModel
                {
                    ProjectStage = projectStage,
                    Professions = professions
                };
                stages.Add(stage);
            }
        }
        return stages.Count != 0 ? stages : [new() { ProjectStage = ProjectStage.None }];
    }

    private static List<Profession> GetProfessionsForProjectStage(IInputParser parser,
        int row,
        ProjectStage projectStage)
    {
        InputIndex stage = Enum.TryParse(projectStage.ToString(), true, out InputIndex inputIndex1)
            ? inputIndex1
            : InputIndex.Unknown;
        if (stage == InputIndex.Unknown)
        {
            return [];
        }

        var professions = new List<Profession>();

        foreach (Profession profession in Enum.GetValues(typeof(Profession)))
        {
            InputIndex p = Enum.TryParse(profession.ToString(), true, out InputIndex inputIndex2)
                ? inputIndex2
                : InputIndex.Unknown;
            if (p == InputIndex.Unknown)
            {
                continue;
            }
            var value = parser.GetValueAsString(row, stage, p);
            if (value != "")
            {
                professions.Add(profession);
            }
        }
        return professions;
    }
}
