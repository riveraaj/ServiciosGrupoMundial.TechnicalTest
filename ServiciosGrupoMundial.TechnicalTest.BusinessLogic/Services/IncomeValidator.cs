namespace ServiciosGrupoMundial.TechnicalTest.BusinessLogic.Services;

/// <summary>
/// This class validates the income details of a loan request, ensuring the borrower's monthly salary
/// falls within a permissible range based on the base salary and a tolerance percentage.
/// </summary>
public class IncomeValidator : IIncomeValidator
{
    private const double BASE_MONTHLY_SALARY = 2000;
    private const double TOLERANCE_PERCENTAGE = 0.15;

    /// <summary>
    /// Validates the income details of the loan request by ensuring that the borrower's salary falls
    /// within an acceptable range, based on the base salary and the specified tolerance percentage.
    /// </summary>
    /// <param name="oLoanRequest">The loan request object containing the income details to validate.</param>
    /// <returns>A validation error message if the income is outside the allowed range, or an empty string if valid.</returns>
    public string Validate(LoanRequest oLoanRequest)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(oLoanRequest);

            var lastIncome = GetLastIncome(oLoanRequest.IncomeDetails);

            if (lastIncome == null) return "El XML no cumple con el formato válido: No se encontraron trabajos en el historial.";

            var monthlySalary = CalculateMonthlySalary(double.Parse(lastIncome.Salary),
                                                        oLoanRequest.IncomeFrequency);

            double lowerBound = BASE_MONTHLY_SALARY * (1 - TOLERANCE_PERCENTAGE);
            double upperBound = BASE_MONTHLY_SALARY * (1 + TOLERANCE_PERCENTAGE);

            if (monthlySalary < lowerBound || monthlySalary > upperBound)
                return $"El XML no cumple con el formato válido: El salario mensual de {monthlySalary} está fuera del rango permitido de {lowerBound} a {upperBound}.";

            return string.Empty;
        }
        catch (Exception ex)
        {
            return ($"Error en la validación de ingresos: {ex.Message}");
        }
    }

    /// <summary>
    /// Calculates the monthly salary based on the provided income frequency and the salary value.
    /// Supports weekly, biweekly, twice a month, and monthly frequencies.
    /// </summary>
    /// <param name="salary">The salary amount provided by the borrower.</param>
    /// <param name="incomeFrequency">The frequency of the income (e.g., weekly, biweekly).</param>
    /// <returns>The calculated monthly salary based on the income frequency.</returns>
    /// <exception cref="ArgumentException">Thrown if the income frequency is invalid or null/empty.</exception>
    private static double CalculateMonthlySalary(double salary, string incomeFrequency)
    {
        if (string.IsNullOrWhiteSpace(incomeFrequency))
            throw new ArgumentException("La frecuencia de los ingresos no puede ser nula ni estar vacía", nameof(incomeFrequency));

        return incomeFrequency.ToLower() switch
        {
            "weekly" => salary * 4.33,
            "biweekly" => salary * 2,
            "twice" => salary * 2,
            "monthly" => salary,
            _ => throw new ArgumentException("Frecuencia de ingresos no válida", nameof(incomeFrequency))
        };
    }

    /// <summary>
    /// Retrieves the most recent income detail from the list of income details, based on the 'LastDate' field.
    /// </summary>
    /// <param name="incomeDetails">The list of income details associated with the loan request.</param>
    /// <returns>The most recent <see cref="IncomeDetail"/> object, or null if no details are found.</returns>
    private static IncomeDetail? GetLastIncome(IEnumerable<IncomeDetail> incomeDetails)
        => incomeDetails.OrderByDescending(detail => DateTime.ParseExact(detail.LastDate,
                                                                         "MM/dd/yyyy",
                                                                         CultureInfo.InvariantCulture))
                                                                         .FirstOrDefault();
}