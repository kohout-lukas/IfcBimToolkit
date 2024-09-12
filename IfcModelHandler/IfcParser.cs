// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

#region
using IfcModelHandler.Elements;
using Xbim.Ifc4.Interfaces;

#endregion

namespace IfcModelHandler;

/// <summary>
///     Logic for parsing IFC files.
/// </summary>
/// <remarks>
///     Logic for parsing IFC files.
/// </remarks>
/// <param name="inputIfcFilePath">Source IFC file path.</param>
public class IfcParser(string inputIfcFilePath)
{
    /// <summary>
    ///     Source file path of of IFC file.
    /// </summary>
    private readonly string _inputIfcFilePath = inputIfcFilePath;

    /// <summary>
    ///     Retrieves all IFC elements and all of their properties from source IFC file.
    /// </summary>
    /// <param name="success">Output result of this operation.</param>
    /// <returns>collection of parsed IFC elements.</returns>
    public IEnumerable<IIfcElement> GetElementsWithProperties(out bool success)
    {
        var allElementsInIfc = GetAllElements();
        var outputElements = allElementsInIfc
            .AsParallel()
            .Where(x => x is not (IIfcAnnotation or IIfcProject or IIfcSite or IIfcBuilding or IIfcBuildingStorey or IIfcGroup or IIfcSystem or IIfcOpeningElement));

        success = true;
        return outputElements;
    }

    /// <summary>
    ///     Retrieves all IFC elements from source IFC file.
    /// </summary>
    /// <returns>collection of parsed IFC elements.</returns>
    public IEnumerable<IIfcElement> GetAllElements()
    {
        var ifcModel = IfcFileHandler.OpenIfcFileForReading(_inputIfcFilePath);
        return IfcElementHandler<IIfcElement>.GetAllElementsInIfcModel(ifcModel);
    }
}
