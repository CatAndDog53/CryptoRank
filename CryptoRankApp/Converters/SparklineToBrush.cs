using Models;
using System.Globalization;
using System.Windows.Data;

namespace CryptoRankApp.Converters
{
    public class SparklineToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var markets = value as CoinMarketData;
            if (markets.SparklineIn7D.Price.First() > markets.SparklineIn7D.Price.Last())
            {
                return "#d90404";
            }
            return "#48db48";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
