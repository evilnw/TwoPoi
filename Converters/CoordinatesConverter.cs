using System.Globalization;

namespace TwoPoi
{
    internal class CoordinatesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((double)value).ToString(null, CultureInfo.InvariantCulture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Double.Parse(value.ToString(), CultureInfo.InvariantCulture);
    }
}
