// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using BimCollabRulesCreator.Models.SmartViews;

namespace BimCollabRulesCreator.Models.ClashRules;
[XmlType("CLASHRULE")]
[XmlInclude(typeof(SmartView))]
public class ClashRule
{
    [XmlElement("TITLE")]
    public string Title { get; set; }
    [XmlElement("DESCRIPTION")]
    public string Description { get; set; } = string.Empty;
    [XmlElement("MODIFICATIONDATE")]
    public string ModificationDate { get; init; } = DateTime.UtcNow.ToString("s");
    [XmlElement("GUID")]
    public Guid Guid { get; init; } = Guid.NewGuid();
    [XmlArray("SOURCESET")]
    [XmlArrayItem("SMARTVIEW")]
    public List<SmartView> SourceSmartViews { get; set; } = [];
    [XmlArray("TARGETSET")]
    [XmlArrayItem("SMARTVIEW")]
    public List<SmartView> TargetSmartViews { get; set; } = [];
    [XmlElement("CLASHTYPE")]
    public bool ClashType { get; set; } = true;
    [XmlElement("DUPLICATETYPE")]
    public bool DuplicateType { get; set; } = true;
    [XmlElement("PENETRATIONDEPTHTOLERANCEENABLED")]
    public bool PenetrationDepthToleranceEnabled { get; set; } = false;
    [XmlElement("PENETRATIONDEPTHTOLERANCE")]
    public double PenetrationDepthTolerance { get; set; } = 0.0;
    [XmlElement("PENETRATIONDEPTHTOLERANCEUSESHORTEST")]
    public bool PenetrationDepthToleranceUSeShortest { get; set; } = true;
    [XmlElement("VOLUMETOLERANCEENABLED")]
    public bool VolumeToleranceEnabled { get; set; } = false;
    [XmlElement("VOLUMETOLERANCE")]
    public double VolumeTolerance { get; set; } = 0.0;
    [XmlElement("CLASHSAMEMODEL")]
    public bool ClashSameModel { get; set; } = true;
    [XmlElement("CLASHSAMESYSTEM")]
    public bool ClashSameSystem { get; set; } = false;
    [XmlElement("CLASHSUBPARTS")]
    public bool ClashSubparts { get; set; } = false;
}
