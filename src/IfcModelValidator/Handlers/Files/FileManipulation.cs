// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

namespace IfcModelValidator.Handlers.Files;

/// <summary>
///     Logic for manipulating files (copy, create, delete etc.).
/// </summary>
public static class FileManipulation
{
    /// <summary>
    ///     Deletes existing file and copies new one into given location.
    /// </summary>
    /// <param name="sourceFilePath">Manipulated file source file path.</param>
    /// <param name="targetFilePath">Manipulated file target file path.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static void DeleteAndCopy(string sourceFilePath, string targetFilePath)
    {
        if (string.IsNullOrEmpty(sourceFilePath))
        {
            throw new ArgumentException("Source file path cannot be empty.", nameof(sourceFilePath));
        }
        if (string.IsNullOrEmpty(targetFilePath))
        {
            throw new ArgumentException("Target file path cannot be empty.", nameof(targetFilePath));
        }
        if (File.Exists(targetFilePath))
        {
            File.Delete(targetFilePath);
        }

        Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath) ?? throw new InvalidOperationException());
        File.Copy(sourceFilePath, targetFilePath);
    }

    /// <summary>
    ///     Deletes existing file and creates new one into given location.
    /// </summary>
    /// <param name="filePath">Target file path for new file.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static void DeleteAndCreate(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("File path cannot be empty.");
        }
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException());
        var fs = File.Create(filePath);
        fs.Close();
    }
}
