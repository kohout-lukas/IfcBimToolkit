// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using DataStandardRepository.Models;


namespace DataStandardRepository.Parsers;
public interface IInputParser
{
    string GetValueAsString(int identifier, InputIndex columnName);
    string GetValueAsString(int identifier, InputIndex first, InputIndex second);
    IEnumerable<ElementParameterModel> GetCommonParameters(int identifier);
}
