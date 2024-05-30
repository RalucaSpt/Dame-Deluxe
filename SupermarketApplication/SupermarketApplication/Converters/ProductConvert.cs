using System;
using System.Globalization;
using System.Windows.Data;
using SupermarketApplication.Models.EntityLayer;

namespace SupermarketApplication.Converters
{
    public class ProductConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int categoryId = 0;
            int manufacturerId = 0;

            Category category = values[2] as Category;

            Manufacturer manufacturer = values[3] as Manufacturer;

            if (values[2] != null)
            {
                categoryId = category.CategoryId;
            }

            if (values[3] != null )
            {
                manufacturerId = manufacturer.ManufacturerId;
            }

            return new Product
            {
                ProductName = values[0] as string,
                Barcode = values[1] as string,
                CategoryID = categoryId,
                ManufacturerID = manufacturerId,
                Category = category,
                Manufacturer = manufacturer
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
