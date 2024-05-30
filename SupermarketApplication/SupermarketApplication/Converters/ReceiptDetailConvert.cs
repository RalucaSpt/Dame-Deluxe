using System;
using System.Globalization;
using System.Windows.Data;
using SupermarketApplication.Models.EntityLayer;

namespace SupermarketApplication.Converters
{
    public class ReceiptDetailConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int receiptId = 0;
            int productId = 0;
            int quantity = 0;
            decimal subtotal = 0;

            if (values[0] != null && int.TryParse(values[0].ToString(), out var tempReceiptId))
            {
                receiptId = tempReceiptId;
            }

            if (values[1] != null && int.TryParse(values[1].ToString(), out var tempProductId))
            {
                productId = tempProductId;
            }

            if (values[2] != null && int.TryParse(values[2].ToString(), out var tempQuantity))
            {
                quantity = tempQuantity;
            }

            if (values[3] != null && decimal.TryParse(values[3].ToString(), out var tempSubtotal))
            {
                subtotal = tempSubtotal;
            }

            return new ReceiptDetail
            {
                ReceiptID = receiptId,
                ProductID = productId,
                Quantity = quantity,
                Subtotal = subtotal
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
