// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region
using Xbim.Ifc;
#endregion


namespace IfcModelValidator.Handlers.Ifc;

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
