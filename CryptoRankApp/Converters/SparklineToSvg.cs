using Models;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace CryptoRankApp.Converters
{
    public class SparklineToSvgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is CoinMarketData markets))
            {
                return null;
            }

            decimal max = markets.SparklineIn7D.Price.Max();
            decimal avg = markets.SparklineIn7D.Price.Average();
            decimal coef = max - avg;
            decimal scale = 14 / (coef == 0 ? 14 : coef);

            return ConstructSvgPath(markets, max, scale);
        }

        private static string ConstructSvgPath(CoinMarketData markets, decimal max, decimal scale)
        {
            var svgPath = new StringBuilder();
            for (int index = 0; index < markets.SparklineIn7D.Price.Length; index++)
            {
                char instruction = index == 0 ? 'M' : 'L';
                decimal scaledPrice = (max - markets.SparklineIn7D.Price[index]) * scale;
                svgPath.AppendFormat(CultureInfo.InvariantCulture, "{0}{1} {2:0.###} ", instruction, index, scaledPrice);
            }

            return svgPath.ToString().TrimEnd();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
