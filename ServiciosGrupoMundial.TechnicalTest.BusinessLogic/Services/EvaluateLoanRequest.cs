namespace ServiciosGrupoMundial.TechnicalTest.BusinessLogic.Services;

/// <summary>
/// This class evaluates a loan request by validating the loan data using a provided validator.
/// </summary>
/// <param name="validator">An implementation of IValidator for LoanRequest validation.</param>
public class EvaluateLoanRequest(IValidator<LoanRequest> validator)
{
    private readonly IValidator<LoanRequest> _validator = validator;

    /// <summary>
    /// Validates a loan request using the provided validator and returns a result message based on the validation.
    /// </summary>
    /// <param name="oLoanRequest">The loan request object to be validated.</param>
    /// <returns>A <see cref="ValidationResult"/> object containing the validation result message and status code.</returns>
    public ValidationResult Validate(LoanRequest oLoanRequest)
    {
        try
        {
            if (oLoanRequest == null) return new ValidationResult("No se ha proporcionado un XML válido", -1);

            var validationMessage = _validator.Validate(oLoanRequest);
            if (!string.IsNullOrEmpty(validationMessage))
                return new ValidationResult(validationMessage, -1);

            var message = $"El XML ID: {oLoanRequest.CustomerId}, fue aceptado con el nombre {oLoanRequest.NameFirst} {oLoanRequest.NameLast} y su último trabajo fue el {oLoanRequest.IncomeDetails.FirstOrDefault()!.LastDate}";
            return new ValidationResult(message, 0);
        }
        catch (Exception ex)
        {
            return new ValidationResult($"Error desconocido: {ex.Message}", -2);
        }
    }
}