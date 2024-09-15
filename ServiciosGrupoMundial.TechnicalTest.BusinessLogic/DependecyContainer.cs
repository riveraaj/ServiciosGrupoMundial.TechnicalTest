namespace ServiciosGrupoMundial.TechnicalTest.BusinessLogic;
public static class DependecyContainer
{
    public static IServiceCollection AddBusinessLogicServices
        (this IServiceCollection services)
    {
        services.AddScoped<IIncomeValidator, IncomeValidator>();
        services.AddScoped<IValidator<LoanRequest>, LoanRequestValidator>();
        services.AddScoped<EvaluateLoanRequest>();

        return services;
    }
}
