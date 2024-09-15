using ServiciosGrupoMundial.TechnicalTest.BusinessObjects.ValueObjects;

namespace ServiciosGrupoMundial.TechnicalTest.UnitTests;
public class TestDataHelper
{
    public static List<DataElement> GetDefaultDataElements()
    {
        return
            [
                new DataElement { Name = "sub_id", Value = "7545320" },
                new DataElement { Name = "bank_account_type", Value = "CHECKING" },
                new DataElement { Name = "bank_name", Value = "TRUSTONE FINANCIAL FEDERAL CR UN" },
                new DataElement { Name = "client_url_root", Value = "lendyou.com" },
                new DataElement { Name = "customer_id", Value = "60080928" },
                new DataElement { Name = "employer_length_months", Value = "12" },
                new DataElement { Name = "employer_name", Value = "walgreens" },
                new DataElement { Name = "ext_work", Value = "" },
                new DataElement { Name = "home_city", Value = "Milwaukee" },
                new DataElement { Name = "home_state", Value = "CR" },
                new DataElement { Name = "home_street", Value = "4758 n 30th st" },
                new DataElement { Name = "home_zip", Value = "53209" },
                new DataElement { Name = "income_date1_d", Value = "19" },
                new DataElement { Name = "income_date1_m", Value = "01" },
                new DataElement { Name = "income_date1_y", Value = "2018" },
                new DataElement { Name = "income_date2_d", Value = "02" },
                new DataElement { Name = "income_date2_m", Value = "02" },
                new DataElement { Name = "income_date2_y", Value = "2018" },
                new DataElement { Name = "income_frequency", Value = "" },
                new DataElement { Name = "income_amount", Value = @"[ { ""Company"":""AT"", ""LastDate"":""01/05/2018"", ""Position"":""Software Developer Level 2"", ""Salary"": ""2500"" },
                                                                                   { ""Company"":""Microsoft"", ""LastDate"":""05/29/2020"", ""Position"":""Software Developer Level 1"", ""Salary"": ""2200"" },
                                                                                   { ""Company"":""Oracle"", ""LastDate"":""07/10/2014"", ""Position"":""Software Developer Level 3"", ""Salary"": ""1100"" } ]" },
                new DataElement { Name = "income_type", Value = "EMPLOYMENT" },
                new DataElement { Name = "loan_amount_desired", Value = "500" },
                new DataElement { Name = "military", Value = "1" },
                new DataElement { Name = "name_first", Value = "Lisa" },
                new DataElement { Name = "name_last", Value = "Price" },
                new DataElement { Name = "name_middle", Value = "" },
                new DataElement { Name = "name_title", Value = "MR" },
                new DataElement { Name = "phone_home", Value = "7981541924" },
                new DataElement { Name = "phone_cell", Value = "1541541924" },
                new DataElement { Name = "date_dob_d", Value = "14" },
                new DataElement { Name = "date_dob_m", Value = "9" },
                new DataElement { Name = "date_dob_y", Value = "2006" },
                new DataElement { Name = "residence_length_months", Value = "12" },
                new DataElement { Name = "residence_type", Value = "OWN" },
                new DataElement { Name = "ssn_part_1", Value = "000" },
                new DataElement { Name = "ssn_part_2", Value = "00" },
                new DataElement { Name = "ssn_part_3", Value = "0000" },
                new DataElement { Name = "state_id_number", Value = "0000406" },
                new DataElement { Name = "state_issued_id", Value = "WI" },
                new DataElement { Name = "bank_check_number", Value = "" }
            ];
    }
}
