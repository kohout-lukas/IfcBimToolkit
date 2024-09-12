// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using DataStandardRepository.Models;

namespace InformationDeliverySpecification;

public class IdsFactory(IEnumerable<ElementModel> standard, string filePath)
{
    private readonly IEnumerable<ElementModel> _standard = standard;
    private readonly string _filePath = filePath;

    public void GenerateIds()
    {
        var uniqueElements = _standard.Where(x => x.ParentElementModel is not null);
        var idsFile = new ids
        {
            info = new()
            {
                title = _filePath,
                copyright = "BIMCON",
                version = "1.0.0",
                description = _filePath,
                author = "lukas.kohout@bimcon.cz",
                date = DateTime.Now,
                purpose = "IFC validation",
                milestone = "Current"
            }
        };


        foreach (var element in uniqueElements)
        {

        }
    }



}
