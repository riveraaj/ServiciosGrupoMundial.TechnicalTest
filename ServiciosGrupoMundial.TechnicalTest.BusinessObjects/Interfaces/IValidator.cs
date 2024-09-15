namespace ServiciosGrupoMundial.TechnicalTest.BusinessObjects.Interfaces;
public interface IValidator<T>
{
    string Validate(T obj);
}