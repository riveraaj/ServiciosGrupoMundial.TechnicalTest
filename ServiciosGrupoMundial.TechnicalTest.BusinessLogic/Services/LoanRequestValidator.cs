namespace ServiciosGrupoMundial.TechnicalTest.BusinessLogic.Services;

/// <summary>
/// This class is responsible for validating loan requests based on specific business rules.
/// It checks various fields of a loan request object to ensure that the input data adheres to
/// predefined criteria, such as age, employment duration, loan amount, and other conditions.
/// </summary>
/// <param name="incomeValidator">An instance of the <see cref="IncomeValidator"/> class used to validate income details.</param>
public class LoanRequestValidator(IIncomeValidator incomeValidator) : IValidator<LoanRequest>
{
    private readonly IIncomeValidator _incomeValidator = incomeValidator
        ?? throw new ArgumentNullException(nameof(incomeValidator));

    // <summary>
    /// Validates a loan request object by applying various business rules.
    /// Checks the request data for validity, such as age restrictions, employment length,
    /// loan amount ranges, and specific restrictions on the state, military status, and income frequency.
    /// </summary>
    /// <param name="oLoanRequest">The loan request object to be validated.</param>
    /// <returns>A validation error message if the request is invalid, or an empty string if it passes all validations.</returns>
    public string Validate(LoanRequest oLoanRequest)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(oLoanRequest);

            if (oLoanRequest.DateOfBirth == DateTime.MinValue)
                return "El XML no cumple con el formato válido: La fecha de nacimiento no es válida.";

            if (CalculateAge(oLoanRequest.DateOfBirth) < 18)
                return "El XML no cumple con el formato válido: Menor de 18 años.";

            if (oLoanRequest.EmployerLengthMonths < 12)
                return "El XML no cumple con el formato válido: Tiempo de empleo menor a 12 meses.";

            if (oLoanRequest.LoanAmountDesired < 200 || oLoanRequest.LoanAmountDesired > 500)
                return "El XML no cumple con el formato válido: Monto del préstamo fuera del rango permitido.";

            if (oLoanRequest.HomeState.Length != 2)
                return "El XML no cumple con el formato válido: El estado debe tener 2 letras.";

            if (Enum.TryParse(oLoanRequest.HomeState, true, out InvalidStates _))
                return "El XML no cumple con el formato válido: Estado inválido.";

            if (!Enum.TryParse(oLoanRequest.IncomeFrequency, true, out ValidFrequencies _))
                return "El XML no cumple con el formato válido: Frecuencia de ingreso inválida.";

            var incomeValidation = _incomeValidator.Validate(oLoanRequest);
            if (!string.IsNullOrEmpty(incomeValidation)) return incomeValidation;

            if (oLoanRequest.Military)
                return "El XML no cumple con el formato válido: El solicitante está en el servicio militar.";

            return string.Empty;
        }
        catch (Exception ex)
        {
            return $"Error en la validación del préstamo: {ex.Message}";
        }
    }

    /// <summary>
    /// Helper method to calculate the age of the borrower based on their date of birth.
    /// </summary>
    /// <param name="birthDate">The borrower's birth date.</param>
    /// <returns>The calculated age of the borrower in years.</returns>
    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age)) age--;

        return age;
    }
}