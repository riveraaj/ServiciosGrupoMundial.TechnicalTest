namespace ServiciosGrupoMundial.TechnicalTest.BusinessObjects.ValueObjects;

[XmlRoot("tss_loan_request")]
public class LoanRequestXML
{
    [XmlElement("data")]
    public List<DataElement> DataElements { get; set; }
}