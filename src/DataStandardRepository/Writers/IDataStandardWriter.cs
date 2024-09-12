// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using DataStandardRepository.Models;

namespace DataStandardRepository.Writers;
public interface IDataStandardWriter
{
    bool WriteDataStandard(IEnumerable<ElementModel> collection, string outputFilePath);
}
