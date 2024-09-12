// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using System.Xml.Serialization;

namespace InformationDeliverySpecification;

[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
[XmlRootAttribute(Namespace = "http://standards.buildingsmart.org/IDS", IsNullable = false)]
public class ids
{
    private idsInfo infoField;

    private specificationType[] specificationsField;


    public idsInfo info
    {
        get
        {
            return this.infoField;
        }
        set
        {
            this.infoField = value;
        }
    }


    [XmlArrayItemAttribute("specification", IsNullable = false)]
    public specificationType[] specifications
    {
        get
        {
            return this.specificationsField;
        }
        set
        {
            this.specificationsField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
public class idsInfo
{

    private string titleField;

    private string copyrightField;

    private string versionField;

    private string descriptionField;

    private string authorField;

    private System.DateTime dateField;

    private bool dateFieldSpecified;

    private string purposeField;

    private string milestoneField;


    public string title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }


    public string copyright
    {
        get
        {
            return this.copyrightField;
        }
        set
        {
            this.copyrightField = value;
        }
    }


    public string version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }


    public string description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }


    public string author
    {
        get
        {
            return this.authorField;
        }
        set
        {
            this.authorField = value;
        }
    }


    [XmlElementAttribute(DataType = "date")]
    public System.DateTime date
    {
        get
        {
            return this.dateField;
        }
        set
        {
            this.dateField = value;
        }
    }


    [XmlIgnoreAttribute()]
    public bool dateSpecified
    {
        get
        {
            return this.dateFieldSpecified;
        }
        set
        {
            this.dateFieldSpecified = value;
        }
    }


    public string purpose
    {
        get
        {
            return this.purposeField;
        }
        set
        {
            this.purposeField = value;
        }
    }


    public string milestone
    {
        get
        {
            return this.milestoneField;
        }
        set
        {
            this.milestoneField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class requirementsType
{

    private requirementsTypeEntity[] entityField;

    private requirementsTypePartOf[] partOfField;

    private requirementsTypeClassification[] classificationField;

    private requirementsTypeAttribute[] attributeField;

    private requirementsTypeProperty[] propertyField;

    private requirementsTypeMaterial[] materialField;


    [XmlElementAttribute("entity")]
    public requirementsTypeEntity[] entity
    {
        get
        {
            return this.entityField;
        }
        set
        {
            this.entityField = value;
        }
    }


    [XmlElementAttribute("partOf")]
    public requirementsTypePartOf[] partOf
    {
        get
        {
            return this.partOfField;
        }
        set
        {
            this.partOfField = value;
        }
    }


    [XmlElementAttribute("classification")]
    public requirementsTypeClassification[] classification
    {
        get
        {
            return this.classificationField;
        }
        set
        {
            this.classificationField = value;
        }
    }


    [XmlElementAttribute("attribute")]
    public requirementsTypeAttribute[] attribute
    {
        get
        {
            return this.attributeField;
        }
        set
        {
            this.attributeField = value;
        }
    }


    [XmlElementAttribute("property")]
    public requirementsTypeProperty[] property
    {
        get
        {
            return this.propertyField;
        }
        set
        {
            this.propertyField = value;
        }
    }


    [XmlElementAttribute("material")]
    public requirementsTypeMaterial[] material
    {
        get
        {
            return this.materialField;
        }
        set
        {
            this.materialField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
public class requirementsTypeEntity : entityType
{

    private string instructionsField;


    [XmlAttributeAttribute()]
    public string instructions
    {
        get
        {
            return this.instructionsField;
        }
        set
        {
            this.instructionsField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class entityType
{

    private idsValue nameField;

    private idsValue predefinedTypeField;


    public idsValue name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }


    public idsValue predefinedType
    {
        get
        {
            return this.predefinedTypeField;
        }
        set
        {
            this.predefinedTypeField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class idsValue
{

    private object itemField;


    [XmlElementAttribute("restriction", typeof(object))]
    [XmlElementAttribute("simpleValue", typeof(string))]
    public object Item
    {
        get
        {
            return this.itemField;
        }
        set
        {
            this.itemField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
public class requirementsTypePartOf : partOfType
{

    private simpleCardinality cardinalityField;

    private string instructionsField;

    public requirementsTypePartOf()
    {
        this.cardinalityField = simpleCardinality.required;
    }


    [XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(simpleCardinality.required)]
    public simpleCardinality cardinality
    {
        get
        {
            return this.cardinalityField;
        }
        set
        {
            this.cardinalityField = value;
        }
    }


    [XmlAttributeAttribute()]
    public string instructions
    {
        get
        {
            return this.instructionsField;
        }
        set
        {
            this.instructionsField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public enum simpleCardinality
{


    required,


    prohibited,
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class partOfType
{

    private entityType entityField;

    private relations relationField;

    private bool relationFieldSpecified;


    public entityType entity
    {
        get
        {
            return this.entityField;
        }
        set
        {
            this.entityField = value;
        }
    }


    [XmlAttributeAttribute()]
    public relations relation
    {
        get
        {
            return this.relationField;
        }
        set
        {
            this.relationField = value;
        }
    }


    [XmlIgnoreAttribute()]
    public bool relationSpecified
    {
        get
        {
            return this.relationFieldSpecified;
        }
        set
        {
            this.relationFieldSpecified = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public enum relations
{


    IFCRELAGGREGATES,


    IFCRELASSIGNSTOGROUP,


    IFCRELCONTAINEDINSPATIALSTRUCTURE,


    IFCRELNESTS,


    [XmlEnumAttribute("IFCRELVOIDSELEMENT IFCRELFILLSELEMENT")]
    IFCRELVOIDSELEMENTIFCRELFILLSELEMENT,
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
public class requirementsTypeClassification : classificationType
{

    private string uriField;

    private conditionalCardinality cardinalityField;

    private string instructionsField;

    public requirementsTypeClassification()
    {
        this.cardinalityField = conditionalCardinality.required;
    }


    [XmlAttributeAttribute(DataType = "anyURI")]
    public string uri
    {
        get
        {
            return this.uriField;
        }
        set
        {
            this.uriField = value;
        }
    }


    [XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(conditionalCardinality.required)]
    public conditionalCardinality cardinality
    {
        get
        {
            return this.cardinalityField;
        }
        set
        {
            this.cardinalityField = value;
        }
    }


    [XmlAttributeAttribute()]
    public string instructions
    {
        get
        {
            return this.instructionsField;
        }
        set
        {
            this.instructionsField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public enum conditionalCardinality
{


    required,


    prohibited,


    optional,
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class classificationType
{

    private idsValue valueField;

    private idsValue systemField;


    public idsValue value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }


    public idsValue system
    {
        get
        {
            return this.systemField;
        }
        set
        {
            this.systemField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
public class requirementsTypeAttribute : attributeType
{

    private conditionalCardinality cardinalityField;

    private string instructionsField;

    public requirementsTypeAttribute()
    {
        this.cardinalityField = conditionalCardinality.required;
    }


    [XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(conditionalCardinality.required)]
    public conditionalCardinality cardinality
    {
        get
        {
            return this.cardinalityField;
        }
        set
        {
            this.cardinalityField = value;
        }
    }


    [XmlAttributeAttribute()]
    public string instructions
    {
        get
        {
            return this.instructionsField;
        }
        set
        {
            this.instructionsField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class attributeType
{

    private idsValue nameField;

    private idsValue valueField;


    public idsValue name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }


    public idsValue value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
public class requirementsTypeProperty : propertyType
{

    private string uriField;

    private conditionalCardinality cardinalityField;

    private string instructionsField;

    public requirementsTypeProperty()
    {
        this.cardinalityField = conditionalCardinality.required;
    }


    [XmlAttributeAttribute(DataType = "anyURI")]
    public string uri
    {
        get
        {
            return this.uriField;
        }
        set
        {
            this.uriField = value;
        }
    }


    [XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(conditionalCardinality.required)]
    public conditionalCardinality cardinality
    {
        get
        {
            return this.cardinalityField;
        }
        set
        {
            this.cardinalityField = value;
        }
    }


    [XmlAttributeAttribute()]
    public string instructions
    {
        get
        {
            return this.instructionsField;
        }
        set
        {
            this.instructionsField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class propertyType
{

    private idsValue propertySetField;

    private idsValue baseNameField;

    private idsValue valueField;

    private string dataTypeField;


    public idsValue propertySet
    {
        get
        {
            return this.propertySetField;
        }
        set
        {
            this.propertySetField = value;
        }
    }


    public idsValue baseName
    {
        get
        {
            return this.baseNameField;
        }
        set
        {
            this.baseNameField = value;
        }
    }


    public idsValue value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }


    [XmlAttributeAttribute(DataType = "normalizedString")]
    public string dataType
    {
        get
        {
            return this.dataTypeField;
        }
        set
        {
            this.dataTypeField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
public class requirementsTypeMaterial : materialType
{

    private string uriField;

    private conditionalCardinality cardinalityField;

    private string instructionsField;

    public requirementsTypeMaterial()
    {
        this.cardinalityField = conditionalCardinality.required;
    }


    [XmlAttributeAttribute(DataType = "anyURI")]
    public string uri
    {
        get
        {
            return this.uriField;
        }
        set
        {
            this.uriField = value;
        }
    }


    [XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(conditionalCardinality.required)]
    public conditionalCardinality cardinality
    {
        get
        {
            return this.cardinalityField;
        }
        set
        {
            this.cardinalityField = value;
        }
    }


    [XmlAttributeAttribute()]
    public string instructions
    {
        get
        {
            return this.instructionsField;
        }
        set
        {
            this.instructionsField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class materialType
{

    private idsValue valueField;


    public idsValue value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class applicabilityType
{

    private entityType entityField;

    private partOfType[] partOfField;

    private classificationType[] classificationField;

    private attributeType[] attributeField;

    private propertyType[] propertyField;

    private materialType[] materialField;


    public entityType entity
    {
        get
        {
            return this.entityField;
        }
        set
        {
            this.entityField = value;
        }
    }


    [XmlElementAttribute("partOf")]
    public partOfType[] partOf
    {
        get
        {
            return this.partOfField;
        }
        set
        {
            this.partOfField = value;
        }
    }


    [XmlElementAttribute("classification")]
    public classificationType[] classification
    {
        get
        {
            return this.classificationField;
        }
        set
        {
            this.classificationField = value;
        }
    }


    [XmlElementAttribute("attribute")]
    public attributeType[] attribute
    {
        get
        {
            return this.attributeField;
        }
        set
        {
            this.attributeField = value;
        }
    }


    [XmlElementAttribute("property")]
    public propertyType[] property
    {
        get
        {
            return this.propertyField;
        }
        set
        {
            this.propertyField = value;
        }
    }


    [XmlElementAttribute("material")]
    public materialType[] material
    {
        get
        {
            return this.materialField;
        }
        set
        {
            this.materialField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(Namespace = "http://standards.buildingsmart.org/IDS")]
public class specificationType
{

    private applicabilityType applicabilityField;

    private specificationTypeRequirements requirementsField;

    private string nameField;

    private specificationTypeIfcVersion ifcVersionField;

    private string identifierField;

    private string descriptionField;

    private string instructionsField;


    public applicabilityType applicability
    {
        get
        {
            return this.applicabilityField;
        }
        set
        {
            this.applicabilityField = value;
        }
    }


    public specificationTypeRequirements requirements
    {
        get
        {
            return this.requirementsField;
        }
        set
        {
            this.requirementsField = value;
        }
    }


    [XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }


    [XmlAttributeAttribute()]
    public specificationTypeIfcVersion ifcVersion
    {
        get
        {
            return this.ifcVersionField;
        }
        set
        {
            this.ifcVersionField = value;
        }
    }


    [XmlAttributeAttribute()]
    public string identifier
    {
        get
        {
            return this.identifierField;
        }
        set
        {
            this.identifierField = value;
        }
    }


    [XmlAttributeAttribute()]
    public string description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }


    [XmlAttributeAttribute()]
    public string instructions
    {
        get
        {
            return this.instructionsField;
        }
        set
        {
            this.instructionsField = value;
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
public class specificationTypeRequirements : requirementsType
{

    private string descriptionField;


    [XmlAttributeAttribute()]
    public string description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }
}


[System.FlagsAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[XmlTypeAttribute(AnonymousType = true, Namespace = "http://standards.buildingsmart.org/IDS")]
public enum specificationTypeIfcVersion
{
    IFC2X3 = 1,
    IFC4 = 2,
    IFC4X3_ADD2 = 4,
}