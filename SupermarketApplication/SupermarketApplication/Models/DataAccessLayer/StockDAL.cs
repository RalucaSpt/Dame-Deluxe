using Microsoft.EntityFrameworkCore;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketApplication.Models.DataAccessLayer
{
    static class StockDAL
    {
        public static List<Stock> GetAllStocks()
        {
            return DBContext.context.Stocks
                .Include(s => s.Product)
                //.Where(s => s.IsActive)
                .ToList();
        }

        public static void AddStock(Stock stock)
        {
            stock.IsActive = true;
            var config = AppConfig.SellingPriceMarkup;
            stock.CalculateSellingPrice(config);

            DBContext.context.Stocks.Add(stock);
            DBContext.context.SaveChanges();
        }

        public static void UpdateStock(Stock stock)
        {
            // Ensure the purchase price is not modified
            var existingStock = DBContext.context.Stocks.AsNoTracking().FirstOrDefault(s => s.StockID == stock.StockID);
            if (existingStock != null)
            {
                stock.PurchasePrice = existingStock.PurchasePrice;
            }

            // Ensure selling price is not less than purchase price
            if (stock.SellingPrice < stock.PurchasePrice)
            {
                throw new InvalidOperationException("Selling price cannot be less than purchase price");
            }

            DBContext.context.Stocks.Update(stock);
            DBContext.context.SaveChanges();
        }

        public static void DeleteStock(Stock stock)
        {
            stock.IsActive = false; // Implementing logical delete
            DBContext.context.Stocks.Update(stock);
            DBContext.context.SaveChanges();
        }

        public static Stock GetStockByProductId(int productId)
        {
            return DBContext.context.Stocks 
                .Include(s => s.Product)
                .FirstOrDefault(s => s.ProductID == productId && s.IsActive);
        }

        public static void CheckAndMarkExpiredStocks()
        {
            var expiredStocks = DBContext.context.Stocks.Where(s => s.ExpiryDate <= DateTime.Now && s.IsActive).ToList();
            foreach (var stock in expiredStocks)
            {
                stock.IsActive = false;
                DBContext.context.Stocks.Update(stock);
            }

            var zeroQuantityStocks = DBContext.context.Stocks.Where(s => s.Quantity == 0 && s.IsActive).ToList();
            foreach (var stock in zeroQuantityStocks)
            {
                stock.IsActive = false;
                DBContext.context.Stocks.Update(stock);
            }

            DBContext.context.SaveChanges();
        }
    }
}
