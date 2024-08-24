// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

namespace BimCollabRulesCreator.Models.SmartViews;
public class Element
{
    public string Name { get; set; }
    public string Classification { get; set; }
    public string ParentName { get; set; }
    public string ParentClassification { get; set; }
    public List<Property> Properties { get; set; }
}
