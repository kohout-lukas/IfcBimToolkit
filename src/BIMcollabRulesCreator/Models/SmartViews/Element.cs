// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

namespace BimCollabRulesCreator.Models.SmartViews;
public class Element
{
    public string Name { get; set; }
    public string Classification { get; set; }
    public string ParentName { get; set; }
    public string ParentClassification { get; set; }
    public List<Property> Properties { get; set; }
}
