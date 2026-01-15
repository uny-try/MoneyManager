using System;
using System.Globalization;
using MoneyManager.Models;

namespace MoneyManager.Converters;

public class TransactionTypeToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TransactionType type || parameter is not string param)
            return false;

        return type.ToString().Equals(param, StringComparison.OrdinalIgnoreCase);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool isChecked || !isChecked || parameter is not string param)
            return Binding.DoNothing;

        return Enum.Parse<TransactionType>(param, true);
    }

}
