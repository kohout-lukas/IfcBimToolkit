// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using BimCollabRulesCreator.Models.SmartViews;
using DataStandardRepository.Models;
using System.Collections.Generic;

namespace IfcBimToolkitApp.Mappers;
public static class ClassMapper
{
    public static IEnumerable<Element> MapElementModelToRuleElement(IEnumerable<ElementModel> data)
    {
        var output = new List<Element>();
        foreach (var elementModel in data)
        {
            var element = new Element()
            {
                Name = elementModel.Name,
                ParentName = elementModel.ParentElementModel is null
                    ? string.Empty
                    : elementModel.ParentElementModel.Name,
                Classification = elementModel.Classification,
                ParentClassification = elementModel.ParentElementModel is null
                    ? string.Empty
                    : elementModel.ParentElementModel.Classification
            };

            if (elementModel.ElementParameters is null)
            {
                output.Add(element);
                continue;
            }

            var parameters = new List<Property>();

            foreach (var parameter in elementModel.ElementParameters)
            {
                var p = new Property
                {
                    Name = parameter.Parameter.Name,
                    PropertySet = parameter.Parameter.Group,
                    Type = parameter.Parameter.DataType.ToString()
                };
                parameters.Add(p);
            }
            element.Properties = parameters;
            output.Add(element);
        }
        return output;
    }
}
