namespace ServiciosGrupoMundial.TechnicalTest.BusinessObjects.Helper;

/// <summary>
/// A helper class that provides methods for converting strings to various data types
/// such as integers, decimals, booleans, and DateTime objects. These methods also handle 
/// default values in case the conversion fails.
/// </summary>
public static class ConversorHelper
{
    public static int ConvertToInt(string value, int defaultValue = default) => int.TryParse(value, out var result) ? result : defaultValue;
    public static decimal ConvertToDecimal(string value, decimal defaultValue = default) => decimal.TryParse(value, out var result) ? result : defaultValue;
    public static bool ConvertToBool(string value, bool defaultValue = default)
    {
        if (int.TryParse(value, out var intValue))
            return intValue == 1;
        return bool.TryParse(value, out var result) ? result : defaultValue;
    }
    public static DateTime ParseDate(string day, string month, string year)
    {
        if (int.TryParse(day, out var d) && int.TryParse(month, out var m) && int.TryParse(year, out var y))
            return new DateTime(y, m, d);

        return default;
    }
}