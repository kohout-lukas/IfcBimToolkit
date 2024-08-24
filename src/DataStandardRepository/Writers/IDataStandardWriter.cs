// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using DataStandardRepository.Models;

namespace DataStandardRepository.Writers;
public interface IDataStandardWriter
{
    bool WriteDataStandard(IEnumerable<ElementModel> collection, string outputFilePath);
}
