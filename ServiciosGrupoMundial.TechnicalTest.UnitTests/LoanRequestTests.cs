using ServiciosGrupoMundial.TechnicalTest.BusinessObjects.POCOs;

namespace ServiciosGrupoMundial.TechnicalTest.UnitTests
{
    public class LoanRequestTests
    {
        [Fact]
        public void ShouldParseDefaultDataElementsCorrectly()
        {
            // Arrange
            var dataElements = TestDataHelper.GetDefaultDataElements();
            var loanRequest = new LoanRequest(dataElements);

            // Act
            var actualNameFirst = loanRequest.NameFirst;
            var actualCustomerId = loanRequest.CustomerId;
            var actualLoanAmountDesired = loanRequest.LoanAmountDesired;

            // Assert
            Assert.Equal("Lisa", actualNameFirst);
            Assert.Equal(60080928, actualCustomerId);
            Assert.Equal(500, actualLoanAmountDesired);
        }

        [Fact]
        public void ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var dataElements = TestDataHelper.GetDefaultDataElements();
            var loanRequest = new LoanRequest(dataElements);

            // Act & Assert
            Assert.Equal(7545320, loanRequest.SubId);
            Assert.Equal("CHECKING", loanRequest.BankAccountType);
            Assert.Equal("TRUSTONE FINANCIAL FEDERAL CR UN", loanRequest.BankName);
            Assert.Equal("lendyou.com", loanRequest.ClientUrlRoot);
            Assert.Equal(60080928, loanRequest.CustomerId);
            Assert.Equal(12, loanRequest.EmployerLengthMonths);
            Assert.Equal("walgreens", loanRequest.EmployerName);
            Assert.Equal("", loanRequest.ExtWork);
            Assert.Equal("Milwaukee", loanRequest.HomeCity);
            Assert.Equal("CR", loanRequest.HomeState);
            Assert.Equal("4758 n 30th st", loanRequest.HomeStreet);
            Assert.Equal("53209", loanRequest.HomeZip);
            Assert.Equal(new DateTime(2018, 1, 19), loanRequest.IncomeDate1);
            Assert.Equal(new DateTime(2018, 2, 2), loanRequest.IncomeDate2);
            Assert.Equal("", loanRequest.IncomeFrequency);
            Assert.NotEmpty(loanRequest.IncomeDetails);
            Assert.Equal("EMPLOYMENT", loanRequest.IncomeType);
            Assert.Equal(500, loanRequest.LoanAmountDesired);
            Assert.True(loanRequest.Military);
            Assert.Equal("Lisa", loanRequest.NameFirst);
            Assert.Equal("Price", loanRequest.NameLast);
            Assert.Equal("", loanRequest.NameMiddle);
            Assert.Equal("MR", loanRequest.NameTitle);
            Assert.Equal("7981541924", loanRequest.PhoneHome);
            Assert.Equal("1541541924", loanRequest.PhoneCell);
            Assert.Equal(new DateTime(2006, 9, 14), loanRequest.DateOfBirth);
            Assert.Equal(12, loanRequest.ResidenceLengthMonths);
            Assert.Equal("OWN", loanRequest.ResidenceType);
            Assert.Equal("000", loanRequest.SsnPart1);
            Assert.Equal("00", loanRequest.SsnPart2);
            Assert.Equal("0000", loanRequest.SsnPart3);
            Assert.Equal("0000406", loanRequest.StateIdNumber);
            Assert.Equal("WI", loanRequest.StateIssuedId);
            Assert.Equal("", loanRequest.BankCheckNumber);
        }

        [Fact]
        public void GetValue_ShouldReturnCorrectValue()
        {
            // Arrange
            var dataElements = TestDataHelper.GetDefaultDataElements();
            var loanRequest = new LoanRequest(dataElements);

            // Act & Assert
            Assert.Equal("7545320", loanRequest.GetValue("sub_id"));
            Assert.Equal("CHECKING", loanRequest.GetValue("bank_account_type"));
            Assert.Equal("TRUSTONE FINANCIAL FEDERAL CR UN", loanRequest.GetValue("bank_name"));
        }

        [Fact]
        public void ShouldReturnEmptyStringForMissingKey()
        {
            // Arrange
            var dataElements = TestDataHelper.GetDefaultDataElements();
            var loanRequest = new LoanRequest(dataElements);

            // Act & Assert
            Assert.Equal("", loanRequest.GetValue("non_existent_key"));
        }

        [Fact]
        public void GetIncomeDetails_ShouldReturnOrderedList()
        {
            // Arrange
            var dataElements = TestDataHelper.GetDefaultDataElements();
            var loanRequest = new LoanRequest(dataElements);

            // Act
            var incomeDetails = loanRequest.IncomeDetails;

            // Assert
            Assert.Equal(3, incomeDetails.Count);
            Assert.Equal("05/29/2020", incomeDetails[0].LastDate); // New
            Assert.Equal("07/10/2014", incomeDetails[2].LastDate); // Old
        }

        [Fact]
        public void GetIncomeDetails_ShouldReturnEmptyListOnFailure()
        {
            // Arrange
            var dataElements = TestDataHelper.GetDefaultDataElements();
            dataElements.Find(de => de.Name == "income_amount").Value = "invalid_json"; // Datos JSON inválidos
            var loanRequest = new LoanRequest(dataElements);

            // Act
            var incomeDetails = loanRequest.IncomeDetails;

            // Assert
            Assert.Empty(incomeDetails);
        }
    }
}