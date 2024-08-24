// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using DataStandardRepository.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IfcBimToolkitApp.Mappers;

/// <summary>
///     Logic for creating Revit Mapping Table for exporting IFC files.
/// </summary>
public static class RevitMappingTable
{
    /// <summary>
    ///     Collection of all IFC element types that are exported with this mapping table.
    /// </summary>
    private static readonly List<string> IfcElementTypes =
    [
        "IfcBeam", "IfcBuildingElementProxy", "IfcColumn", "IfcCovering", "IfcCurtainWall", "IfcDoor",
        "IfcElementAssembly", "IfcFlowTerminal", "Ifcfooting", "IfcFurnishingElement", "IfcMember",
        "IfcOpeningElement", "IfcPile", "IfcPlate", "IfcRailing", "IfcRoof", "IfcSlab", "IfcSpace", "IfcStair", "IfcStairFlight",
        "IfcWall", "IfcWallStandardCase", "IfcWindow", "IfcFlowSegment", "IfcFlowFitting", "IfcFlowController"
    ];

    private static readonly string ElementTypesString = string.Join(", ", IfcElementTypes);
    private static readonly string PropertySetHeaderTemplate = "PropertySet:\txx_placeholder\tI\t" + ElementTypesString;

    /// <summary>
    ///     Creates complete mapping table for exporting project IFC file from Revit. Uses parameter data stored in database.
    /// </summary>
    /// <param name="outputFilePath">Complete file path to database file stored on the hard drive.</param>
    /// <returns>true if mapping was successful.</returns>
    public static bool CreateMappingTable(IEnumerable<ParameterModel> collection, string outputFilePath)
    {
        var sortedParameterCollection = collection
            .OrderBy(x => x.Group)
            .ToList();
        var actualGroupName = sortedParameterCollection.First().Group;

        using var str = new StreamWriter(outputFilePath);
        str.WriteLine(PropertySetHeaderTemplate.Replace("xx_placeholder", actualGroupName));

        foreach (var parameter in sortedParameterCollection)
        {
            if (parameter.Group != actualGroupName)
            {
                actualGroupName = parameter.Group;
                str.WriteLine(PropertySetHeaderTemplate.Replace("xx_placeholder", actualGroupName));
            }

            str.WriteLine(MappingLine(parameter));
        }

        str.Close();

        return true;
    }

    /// <summary>
    ///     Creating single line from given parameter and its properties stored in database.
    /// </summary>
    /// <param name="parameter">Parameter stored in database.</param>
    /// <returns>Returns single line for mapping table.</returns>
    private static string MappingLine(ParameterModel parameter)
    {
        var line = string.Empty;
        line += "\t";
        line += parameter.Name;
        line += "\t";
        line += "Number";

        return line;
    }
}
