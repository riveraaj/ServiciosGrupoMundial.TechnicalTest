namespace ServiciosGrupoMundial.TechnicalTest.BusinessObjects.ValueObjects;
public class DataElement
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlText]
    public string Value { get; set; }
}