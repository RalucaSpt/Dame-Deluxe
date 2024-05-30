using Microsoft.EntityFrameworkCore;
using SupermarketApplication.Models.EntityLayer;
using SupermarketApplication.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketApplication.Models.DataAccessLayer
{
    public static class ReceiptDAL
    {
        public static List<Receipt> GetAllReceipts()
        {
            return DBContext.context.Receipts
                .Include(r => r.ReceiptDetails)
                    .ThenInclude(rd => rd.Product)
                .Include(r => r.Cashier)
                .ToList();
        }


        public static void AddReceipt(Receipt receipt)
        {
            receipt.IsActive = true;
            DBContext.context.Receipts.Add(receipt);
            DBContext.context.SaveChanges();
        }

        public static void UpdateReceipt(Receipt receipt)
        {
            DBContext.context.Receipts.Update(receipt);
            DBContext.context.SaveChanges();
        }

        public static void DeleteReceipt(Receipt receipt)
        {
            receipt.IsActive = false;
            DBContext.context.Receipts.Update(receipt);
            DBContext.context.SaveChanges();
        }

        public static List<ReceiptDetail> GetReceiptDetails(Receipt receipt)
        {
            return DBContext.context.ReceiptDetails.Where(detail => detail.ReceiptID == receipt.ReceiptID).ToList();
        }

        public static int GetMaxID()
        {
            return DBContext.context.Receipts.Max(item => item.ReceiptID);
        }

        public static List<SalesByDay> GetSalesByUserAndMonth(int userId, DateTime month)
        {
            return DBContext.context.Receipts
                .Where(r => r.CashierID == userId && r.Date.Year == month.Year && r.Date.Month == month.Month)
                .GroupBy(r => r.Date.Date)
                .Select(g => new SalesByDay
                {
                    Day = g.Key,
                    TotalSales = g.Sum(r => r.TotalAmount)
                })
                .ToList();
        }

        public static List<Receipt> GetLargestReceiptOfDay(DateTime date)
        {
            return DBContext.context.Receipts
                .Include(r => r.ReceiptDetails)
                    .ThenInclude(rd => rd.Product) // Ensure Product is included
                .Where(r => r.Date.Date == date)
                .OrderByDescending(r => r.TotalAmount)
                .Take(1)
                .ToList();
        }

        public static List<ReceiptDetail> GetReceiptDetailsByReceipt(Receipt receipt)
        {
            return DBContext.context.ReceiptDetails
                .Include(rd => rd.Product)
                .Include(rd => rd.Receipt)
                .Where(rd => rd.ReceiptID == receipt.ReceiptID)
                .ToList();
        }
    }
}