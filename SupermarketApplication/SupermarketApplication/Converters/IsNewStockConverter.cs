using System;
using System.Globalization;
using System.Windows.Data;

namespace SupermarketApplication.Converters
{
    public class IsNewStockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int stockId)
            {
                // Enable the control if the stock ID is zero (indicating a new stock entry)
                return stockId == 0;
            }

            // Default to disabling the control if value is not a valid integer
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
