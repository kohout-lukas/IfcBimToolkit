// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

#region

using DataStandardRepository.Models;
using DataStandardRepository.Parsers;

#endregion

namespace DataStandardRepository.Factories;

public class ElementFactory
{
    /// <summary>
    ///     Creates one element model from given input row.
    /// </summary>
    /// <param name="parser">Input parser.</param>
    /// <param name="identifier">Row number to get data from.</param>
    /// <returns>created ElementModel based on given input row.</returns>
    public static ElementModel Create(IInputParser parser,
        int identifier)
    {
        if (parser.GetValueAsString(identifier, InputIndex.ElementName) == string.Empty)
        {
            return new
                (
                    parser.GetValueAsString(identifier, InputIndex.ParentElementName),
                    string.Empty,
                    parser.GetValueAsString(identifier, InputIndex.IfcObjectType),
                    parser.GetValueAsString(identifier, InputIndex.DocumentationPart)
                );
        }
        else
        {
            ElementModel elementModel = new
                (
                    parser.GetValueAsString(identifier, InputIndex.ElementName),
                    parser.GetValueAsString(identifier, InputIndex.IfcElementType),
                    parser.GetValueAsString(identifier, InputIndex.IfcObjectType),
                    parser.GetValueAsString(identifier, InputIndex.DocumentationPart)
                )
            {
                ParentElementModel = new ElementModel
                    (
                        parser.GetValueAsString(identifier, InputIndex.ParentElementName),
                        string.Empty,
                        parser.GetValueAsString(identifier, InputIndex.IfcObjectType),
                        parser.GetValueAsString(identifier, InputIndex.DocumentationPart)
                    )
                {
                    UniqueClassification = new()
                    {
                        { parser.GetValueAsString(identifier, InputIndex.IfcObjectType), string.Empty }
                    }
                }
            };
            elementModel.UniqueClassification = new()
            {
                { elementModel.ParentElementModel.Classification, elementModel.Classification }
            };

            return elementModel;
        }
    }
    /// <summary>
    ///     Creates one element model from given input row.
    /// </summary>
    /// <param name="parser">Input parser.</param>
    /// <param name="identifier">Row number to get data from.</param>
    /// <param name="ifcObjectType">Parent element code.</param>
    /// <param name="ifcElementType">Element code.</param>
    /// <param name="elementParameter">Element parameter to be associated with created element.</param>
    /// <returns>created ElementModel based on given input row.</returns>
    public static ElementModel Create(IInputParser parser,
        int identifier,
        string ifcObjectType,
        string ifcElementType,
        ElementParameterModel elementParameter)
    {
        ElementModel elementModel = new
            (
                string.Empty,
                ifcElementType,
                ifcObjectType,
                parser.GetValueAsString(identifier, InputIndex.DocumentationPart)
            )
        {
            ParentElementModel = new ElementModel
                (
                    parser.GetValueAsString(identifier, InputIndex.ParentElementName),
                    string.Empty,
                    ifcObjectType,
                    parser.GetValueAsString(identifier, InputIndex.DocumentationPart)
                )
            {
                UniqueClassification = new()
                {
                    { ifcObjectType, string.Empty }
                }
            },
            UniqueClassification = new()
            {
                { ifcObjectType, ifcElementType }
            },
            ElementParameters =
            [
                elementParameter
            ]
        };

        return elementModel;
    }

    /// <summary>
    ///     Creates one element model from given input row already with given parameter.
    /// </summary>
    /// <param name="parser">Input parser.</param>
    /// <param name="row">Row number to get data from.</param>
    /// <param name="elementParameter">Element parameter to be associated with created element.</param>
    /// <returns>created ElementModel based on given input row.</returns>
    public static ElementModel Create(IInputParser parser,
        int row,
        ElementParameterModel elementParameter)
    {
        var elementModel = Create(parser, row);
        elementModel.ElementParameters =
        [
            elementParameter
        ];
        return elementModel;
    }
}
