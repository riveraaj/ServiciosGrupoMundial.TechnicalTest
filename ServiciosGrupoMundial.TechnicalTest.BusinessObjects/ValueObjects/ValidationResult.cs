namespace ServiciosGrupoMundial.TechnicalTest.BusinessObjects.ValueObjects;
public class ValidationResult(string message, int code)
{
    public string Message { get; } = message;
    public int Code { get; } = code;
}