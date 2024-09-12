// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using DataStandardRepository.Models;


namespace DataStandardRepository.Parsers;
public interface IInputParser
{
    string GetValueAsString(int identifier, InputIndex columnName);
    string GetValueAsString(int identifier, InputIndex first, InputIndex second);
    IEnumerable<ElementParameterModel> GetCommonParameters(int identifier);
}
