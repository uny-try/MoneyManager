using System.Globalization;
using MoneyManager.Models;

namespace MoneyManager.Converters;

public class TransactionTypeToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TransactionType transactionType)
        {
            return transactionType switch
            {
                TransactionType.Income => "収入",
                TransactionType.Expense => "支出",
                TransactionType.Transfer => "振替",
                _ => "不明",
            };
        }

        return "不明a";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
