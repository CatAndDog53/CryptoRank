using System.Globalization;
using System.Windows.Data;

namespace CryptoRankApp.Converters
{
    public class DecimalToCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue.ToString("C2", CultureInfo.CurrentCulture);
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (decimal.TryParse(stringValue, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal result))
                {
                    return result;
                }
            }

            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }
    }
}
