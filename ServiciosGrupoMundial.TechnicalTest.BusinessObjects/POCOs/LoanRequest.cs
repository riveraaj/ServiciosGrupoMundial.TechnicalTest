namespace ServiciosGrupoMundial.TechnicalTest.BusinessObjects.POCOs;

/// <summary>
/// Represents a loan request that encapsulates all the essential details
/// required for evaluating a loan application.
/// This class extracts data from a list of `DataElement` objects, 
/// storing it in a dictionary for easier access to values.
/// </summary>
/// <param name="dataElements">A list of DataElement objects representing the loan request data.</param>
public class LoanRequest(List<DataElement> dataElements)
{
    private readonly IReadOnlyDictionary<string, string> _dataElements
        = dataElements.ToDictionary(x => x.Name, x => x.Value ?? string.Empty);

    public int SubId => ConversorHelper.ConvertToInt(GetValue("sub_id"));
    public string BankAccountType => GetValue("bank_account_type");
    public string BankName => GetValue("bank_name");
    public string ClientUrlRoot => GetValue("client_url_root");
    public int CustomerId => ConversorHelper.ConvertToInt(GetValue("customer_id"));
    public int EmployerLengthMonths => ConversorHelper.ConvertToInt(GetValue("employer_length_months"));
    public string EmployerName => GetValue("employer_name");
    public string ExtWork => GetValue("ext_work");
    public string HomeCity => GetValue("home_city");
    public string HomeState => GetValue("home_state");
    public string HomeStreet => GetValue("home_street");
    public string HomeZip => GetValue("home_zip");
    public DateTime IncomeDate1 => ConversorHelper.ParseDate(GetValue("income_date1_d"), GetValue("income_date1_m"), GetValue("income_date1_y"));
    public DateTime IncomeDate2 => ConversorHelper.ParseDate(GetValue("income_date2_d"), GetValue("income_date2_m"), GetValue("income_date2_y"));
    public string IncomeFrequency => GetValue("income_frequency");
    public List<IncomeDetail> IncomeDetails => GetIncomeDetails();
    public string IncomeType => GetValue("income_type");
    public decimal LoanAmountDesired => ConversorHelper.ConvertToDecimal(GetValue("loan_amount_desired"));
    public bool Military => ConversorHelper.ConvertToBool(GetValue("military"));
    public string NameFirst => GetValue("name_first");
    public string NameLast => GetValue("name_last");
    public string NameMiddle => GetValue("name_middle");
    public string NameTitle => GetValue("name_title");
    public string PhoneHome => GetValue("phone_home");
    public string PhoneCell => GetValue("phone_cell");
    public DateTime DateOfBirth => ConversorHelper.ParseDate(GetValue("date_dob_d"), GetValue("date_dob_m"), GetValue("date_dob_y"));
    public int ResidenceLengthMonths => ConversorHelper.ConvertToInt(GetValue("residence_length_months"));
    public string ResidenceType => GetValue("residence_type");
    public string SsnPart1 => GetValue("ssn_part_1");
    public string SsnPart2 => GetValue("ssn_part_2");
    public string SsnPart3 => GetValue("ssn_part_3");
    public string StateIdNumber => GetValue("state_id_number");
    public string StateIssuedId => GetValue("state_issued_id");
    public string BankCheckNumber => GetValue("bank_check_number");

    /// <summary>
    /// Retrieves the value associated with the specified name from the `_dataElements` dictionary.
    /// If the key is not found, an empty string is returned.
    /// </summary>
    /// <param name="name">The key representing the name of the data element.</param>
    /// <returns>The value associated with the specified key, or an empty string if the key is not found.</returns>
    public string GetValue(string name)
        => _dataElements.TryGetValue(name, out var value) ? value : string.Empty;

    /// <summary>
    /// Deserializes and returns the list of income details from a JSON string.
    /// Orders the list by the last income date in descending order.
    /// If deserialization fails, an empty list is returned.
    /// </summary>
    /// <returns>A list of `IncomeDetail` objects, ordered by the last income date, or an empty list on failure.</returns>
    private List<IncomeDetail> GetIncomeDetails()
    {
        try
        {
            var list = JsonSerializer.Deserialize<List<IncomeDetail>>(GetValue("income_amount"));
            if (list == null || list.Count == 0) return [];

            return [.. list.OrderByDescending(job => DateTime.ParseExact(job.LastDate, "MM/dd/yyyy", CultureInfo.InvariantCulture))];
        }
        catch
        {
            return [];
        }
    }
}