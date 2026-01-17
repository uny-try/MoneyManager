using System;
using System.Globalization;
using MoneyManager.Models;

namespace MoneyManager.Converters;

public class TransactionTypeToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TransactionType transactionType && parameter is string targetTypeString)
        {
            if (Enum.TryParse<TransactionType>(targetTypeString, out var targetTransactionType))
            {
                return transactionType == targetTransactionType;
            }
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
