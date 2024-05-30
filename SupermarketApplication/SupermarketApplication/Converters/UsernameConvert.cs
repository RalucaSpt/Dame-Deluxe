using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SupermarketApplication.Models.EntityLayer;

namespace SupermarketApplication.Converters
{
    public class UsernameConvert : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            return new Username
            {
                UserName = values[0] as string,
                Password = values[1] as string,
                UserType = values[2] as string
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
