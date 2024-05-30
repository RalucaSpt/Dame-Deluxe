using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using SupermarketApplication.Models.EntityLayer;

namespace SupermarketApplication.Converters
{
    public class ReceiptConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int cas = 0;
            // Handle UnsetValue and null values
            if (values[0] != null && values[0] != DependencyProperty.UnsetValue)
            {
                return new Receipt
                {
                    Date = DateTime.Now, // or any default value
                    CashierID = values[1] != null ? int.Parse(values[1].ToString()) : 0,
                    TotalAmount = values[2] != null ? decimal.Parse(values[2].ToString()) : 0,
                    IsActive = true
                };
            }

            // Parse values to correct types
            DateTime date;
            if (!DateTime.TryParse(values[0]?.ToString(), out date))
            {
                date = DateTime.Now; // Default value in case of parsing failure
            }

            int cashierId = 0;
            if (values[1] != null && int.TryParse(values[1].ToString(), out int parsedCashierId))
            {
                cashierId = parsedCashierId;
            }

            decimal totalAmount = 0;
            if (values[2] != null && decimal.TryParse(values[2].ToString(), out decimal parsedTotalAmount))
            {
                totalAmount = parsedTotalAmount;
            }

            return new Receipt
            {
                Date = date,
                CashierID = cashierId,
                TotalAmount = totalAmount,
                IsActive = true
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
