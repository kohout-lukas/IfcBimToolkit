// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

#region
using Xbim.Ifc;
#endregion


namespace IfcModelHandler;

/// <summary>
///     Logic for creating new working copy of IFC file and opening it.
/// </summary>
public class IfcFileHandler
{
    /// <summary>
    ///     Opens working copy of selected IFC file.
    /// </summary>
    /// <returns>readable IfcStore with all information from IFC file.</returns>
    public static IfcStore OpenIfcFileForReading(string targetIfcFilePath)
    {
        return IfcStore.Open(targetIfcFilePath);
    }
}
