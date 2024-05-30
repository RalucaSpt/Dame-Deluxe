using System;
using System.Globalization;
using System.Windows.Data;
using SupermarketApplication.Models.EntityLayer;

namespace SupermarketApplication.Converters
{
    public class StockConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int productId = 0;
            int quantity = 0;
            DateTime supplyDate = DateTime.Now;
            DateTime expiryDate = DateTime.Now;
            decimal purchasePrice = 0;
            string unitOfMeasure = string.Empty;

            if (values[0] != null && int.TryParse(values[0].ToString(), out var tempProductId))
            {
                productId = tempProductId;
            }

            if (values[1] != null && int.TryParse(values[1].ToString(), out var tempQuantity))
            {
                quantity = tempQuantity;
            }

            if (values[2] != null && DateTime.TryParse(values[2].ToString(), out var tempSupplyDate))
            {
                supplyDate = tempSupplyDate;
            }

            if (values[3] != null && DateTime.TryParse(values[3].ToString(), out var tempExpiryDate))
            {
                expiryDate = tempExpiryDate;
            }

            if (values[4] != null && decimal.TryParse(values[4].ToString(), out var tempPurchasePrice))
            {
                purchasePrice = tempPurchasePrice;
            }

            if (values[5] != null)
            {
                unitOfMeasure = values[5].ToString();
            }

            return new Stock
            {
                ProductID = productId,
                Quantity = quantity,
                SupplyDate = supplyDate,
                ExpiryDate = expiryDate,
                PurchasePrice = purchasePrice,
                Unit = unitOfMeasure
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
