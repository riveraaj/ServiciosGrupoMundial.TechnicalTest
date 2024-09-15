using Moq;
using ServiciosGrupoMundial.TechnicalTest.BusinessLogic.Services;
using ServiciosGrupoMundial.TechnicalTest.BusinessObjects.Interfaces;
using ServiciosGrupoMundial.TechnicalTest.BusinessObjects.POCOs;

namespace ServiciosGrupoMundial.TechnicalTest.UnitTests;
public class EvaluateLoanRequestTests
{
    [Fact]
    public void Validate_ShouldReturnSuccess()
    {
        // Arrange
        var mockValidator = new Mock<IValidator<LoanRequest>>();
        var loanRequest = new LoanRequest(TestDataHelper.GetDefaultDataElements());
        var validationMessage = ""; // Mensaje vacío indica validación exitosa
        mockValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Returns(validationMessage);

        var evaluator = new EvaluateLoanRequest(mockValidator.Object);

        // Act
        var result = evaluator.Validate(loanRequest);

        // Assert
        Assert.Equal("El XML ID: 60080928, fue aceptado con el nombre Lisa Price y su último trabajo fue el 05/29/2020", result.Message);
        Assert.Equal(0, result.Code);
    }

    [Fact]
    public void Validate_ShouldReturnErrorMessage()
    {
        // Arrange
        var mockValidator = new Mock<IValidator<LoanRequest>>();
        var loanRequest = new LoanRequest(TestDataHelper.GetDefaultDataElements());
        var validationMessage = "Error de validación"; // Mensaje de error
        mockValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Returns(validationMessage);

        var evaluator = new EvaluateLoanRequest(mockValidator.Object);

        // Act
        var result = evaluator.Validate(loanRequest);

        // Assert
        Assert.Equal("Error de validación", result.Message);
        Assert.Equal(-1, result.Code);
    }

    [Fact]
    public void Validate_ShouldReturnNullRequestError()
    {
        // Arrange
        var mockValidator = new Mock<IValidator<LoanRequest>>();
        var evaluator = new EvaluateLoanRequest(mockValidator.Object);

        // Act
        var result = evaluator.Validate(null!);

        // Assert
        Assert.Equal("No se ha proporcionado un XML válido", result.Message);
        Assert.Equal(-1, result.Code);
    }

    [Fact]
    public void Validate_ShouldReturnUnknownError()
    {
        // Arrange
        var mockValidator = new Mock<IValidator<LoanRequest>>();
        var loanRequest = new LoanRequest(TestDataHelper.GetDefaultDataElements());
        mockValidator.Setup(v => v.Validate(It.IsAny<LoanRequest>())).Throws(new Exception("Error inesperado"));

        var evaluator = new EvaluateLoanRequest(mockValidator.Object);

        // Act
        var result = evaluator.Validate(loanRequest);

        // Assert
        Assert.Equal("Error desconocido: Error inesperado", result.Message);
        Assert.Equal(-2, result.Code);
    }
}