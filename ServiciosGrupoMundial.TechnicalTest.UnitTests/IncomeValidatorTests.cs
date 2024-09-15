using ServiciosGrupoMundial.TechnicalTest.BusinessLogic.Services;
using ServiciosGrupoMundial.TechnicalTest.BusinessObjects.POCOs;

namespace ServiciosGrupoMundial.TechnicalTest.UnitTests;
public class IncomeValidatorTests
{
    [Fact]
    public void ShouldReturnEmptyString_WhenSalaryIsWithinRange()
    {
        // Arrange
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "income_frequency").Value = "Monthly";

        var loanRequest = new LoanRequest(dataElements);
        var validator = new IncomeValidator();

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenSalaryIsBelowLowerBound()
    {
        // Arrange
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "income_frequency").Value = "Monthly";
        dataElements.First(x => x.Name == "income_amount").Value = @"[ { ""Company"":""AT"", ""LastDate"":""01/05/2018"", ""Position"":""Software Developer Level 2"", ""Salary"": ""2500"" },
                                                          { ""Company"":""Microsoft"", ""LastDate"":""05/29/2020"", ""Position"":""Software Developer Level 1"", ""Salary"": ""2200"" },
                                                          { ""Company"":""Oracle"", ""LastDate"":""07/10/2024"", ""Position"":""Software Developer Level 3"", ""Salary"": ""1699"" } ]";

        var loanRequest = new LoanRequest(dataElements);
        var validator = new IncomeValidator();

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        var expected = "El XML no cumple con el formato válido: El salario mensual de 1699 está fuera del rango permitido de 1700 a 2300.";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenSalaryIsAboveUpperBound()
    {
        // Arrange
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "income_frequency").Value = "Monthly";
        dataElements.First(x => x.Name == "income_amount").Value = @"[ { ""Company"":""AT"", ""LastDate"":""01/05/2018"", ""Position"":""Software Developer Level 2"", ""Salary"": ""2500"" },
                                                          { ""Company"":""Microsoft"", ""LastDate"":""05/29/2020"", ""Position"":""Software Developer Level 1"", ""Salary"": ""2200"" },
                                                          { ""Company"":""Oracle"", ""LastDate"":""07/10/2024"", ""Position"":""Software Developer Level 3"", ""Salary"": ""2400"" } ]";

        var loanRequest = new LoanRequest(dataElements);
        var validator = new IncomeValidator();

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        var expected = "El XML no cumple con el formato válido: El salario mensual de 2400 está fuera del rango permitido de 1700 a 2300.";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenIncomeFrequencyIsInvalid()
    {
        // Arrange
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "income_frequency").Value = "Yearly";

        var loanRequest = new LoanRequest(dataElements);
        var validator = new IncomeValidator();

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        var expected = "Error en la validación de ingresos:";
        Assert.Contains(expected, result);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenNoIncomeDetailsProvided()
    {
        // Arrange
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "income_frequency").Value = "Monthly";
        dataElements.First(x => x.Name == "income_amount").Value = "";

        var loanRequest = new LoanRequest(dataElements);
        var validator = new IncomeValidator();

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        var expected = "El XML no cumple con el formato válido: No se encontraron trabajos en el historial.";
        Assert.Equal(expected, result);
    }
}