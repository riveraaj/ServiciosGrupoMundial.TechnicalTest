using Moq;
using ServiciosGrupoMundial.TechnicalTest.BusinessLogic.Services;
using ServiciosGrupoMundial.TechnicalTest.BusinessObjects.Interfaces;
using ServiciosGrupoMundial.TechnicalTest.BusinessObjects.POCOs;

namespace ServiciosGrupoMundial.TechnicalTest.UnitTests;
public class LoanRequestValidatorTests
{
    [Fact]
    public void ShouldReturnEmptyStringForValidData()
    {
        // Arrange
        var mockIncomeValidator = new Mock<IIncomeValidator>();
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "income_frequency").Value = "Monthly";
        dataElements.First(x => x.Name == "military").Value = "0";

        var validLoanRequest = new LoanRequest(dataElements);
        mockIncomeValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Returns(string.Empty);

        var validator = new LoanRequestValidator(mockIncomeValidator.Object);

        // Act
        var result = validator.Validate(validLoanRequest);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ShouldReturnErrorIfUnderage()
    {
        // Arrange
        var mockIncomeValidator = new Mock<IIncomeValidator>();
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "date_dob_d").Value = "1";
        dataElements.First(x => x.Name == "date_dob_m").Value = "9";
        dataElements.First(x => x.Name == "date_dob_y").Value = "2007";

        var loanRequest = new LoanRequest(dataElements);
        mockIncomeValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Returns(string.Empty);

        var validator = new LoanRequestValidator(mockIncomeValidator.Object);

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        Assert.Equal("El XML no cumple con el formato válido: Menor de 18 años.", result);
    }

    [Fact]
    public void ShouldReturnErrorIfEmploymentLessThan12Months()
    {
        // Arrange
        var mockIncomeValidator = new Mock<IIncomeValidator>();
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "employer_length_months").Value = "5";

        var loanRequest = new LoanRequest(dataElements);
        mockIncomeValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Returns(string.Empty);

        var validator = new LoanRequestValidator(mockIncomeValidator.Object);

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        Assert.Equal("El XML no cumple con el formato válido: Tiempo de empleo menor a 12 meses.", result);
    }

    [Fact]
    public void ShouldReturnErrorIfLoanAmountOutOfRange()
    {
        // Arrange
        var mockIncomeValidator = new Mock<IIncomeValidator>();
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "loan_amount_desired").Value = "600"; // Exceeds max range

        var loanRequest = new LoanRequest(dataElements);
        mockIncomeValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Returns(string.Empty);

        var validator = new LoanRequestValidator(mockIncomeValidator.Object);

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        Assert.Equal("El XML no cumple con el formato válido: Monto del préstamo fuera del rango permitido.", result);
    }

    [Fact]
    public void ShouldReturnErrorIfInvalidState()
    {
        // Arrange
        var mockIncomeValidator = new Mock<IIncomeValidator>();
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "home_state").Value = "DE"; // Invalid state

        var loanRequest = new LoanRequest(dataElements);
        mockIncomeValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Returns(string.Empty);

        var validator = new LoanRequestValidator(mockIncomeValidator.Object);

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        Assert.Equal("El XML no cumple con el formato válido: Estado inválido.", result);
    }

    [Fact]
    public void ShouldReturnErrorIfInvalidIncomeFrequency()
    {
        // Arrange
        var mockIncomeValidator = new Mock<IIncomeValidator>();
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "income_frequency").Value = "Yearly"; // Invalid frequency

        var loanRequest = new LoanRequest(dataElements);
        mockIncomeValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Returns(string.Empty);

        var validator = new LoanRequestValidator(mockIncomeValidator.Object);

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        Assert.Equal("El XML no cumple con el formato válido: Frecuencia de ingreso inválida.", result);
    }

    [Fact]
    public void ShouldReturnErrorIfMilitary()
    {
        // Arrange
        var mockIncomeValidator = new Mock<IIncomeValidator>();
        var dataElements = TestDataHelper.GetDefaultDataElements();
        dataElements.First(x => x.Name == "income_frequency").Value = "Monthly";
        dataElements.First(x => x.Name == "military").Value = "1"; // In military

        var loanRequest = new LoanRequest(dataElements);
        mockIncomeValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Returns(string.Empty);

        var validator = new LoanRequestValidator(mockIncomeValidator.Object);

        // Act
        var result = validator.Validate(loanRequest);

        // Assert
        Assert.Equal("El XML no cumple con el formato válido: El solicitante está en el servicio militar.", result);
    }

}