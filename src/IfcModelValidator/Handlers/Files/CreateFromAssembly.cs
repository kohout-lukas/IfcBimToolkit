// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using System.Reflection;

#endregion

namespace IfcModelValidator.Handlers.Files;

/// <summary>
///     Logic for saving assembly embedded file to local hard drive.
/// </summary>
public static class CreateFromAssembly
{
    /// <summary>
    ///     Creates new file from embedded resource.
    /// </summary>
    /// <param name="resourceName">Resource name to be created.</param>
    /// <param name="targetDir">Target folder for created file.</param>
    /// <param name="targetName">Target file name for created file.</param>
    public static void Create(string resourceName, string targetDir, string targetName)
    {
        var asm = Assembly.GetExecutingAssembly();
        var resource = $"{asm.GetName().Name}.Resources." + resourceName;
        var stream = asm.GetManifestResourceStream(resource);
        try
        {
            if (stream is null)
                return;

            using Stream file = File.Create(Path.Combine(targetDir, targetName));
            CopyStream(stream, file);

        }
        catch
        {
            throw new Exception();
        }
    }

    /// <summary>
    ///     Copy one stream to another one.
    /// </summary>
    /// <param name="input">Input stream.</param>
    /// <param name="output">Output stream.</param>
    private static void CopyStream(Stream input, Stream output)
    {
        var buffer = new byte[8 * 1024];
        int len = input.Read(buffer, 0, buffer.Length);
        while (len > 0)
            output.Write(buffer, 0, len);
    }
}
