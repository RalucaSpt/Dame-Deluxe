using Microsoft.EntityFrameworkCore;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketApplication.Models.DataAccessLayer
{
    static class ReceiptDetailDAL
    {
        public static List<ReceiptDetail> GetAllReceiptDetails()
        {
            return DBContext.context.ReceiptDetails
                .Include(rd => rd.Product)
                .Include(rd => rd.Receipt)
                .Where(rd => rd.IsActive)
                .ToList();
        }

        public static void AddReceiptDetail(ReceiptDetail receiptDetail)
        {
            receiptDetail.IsActive = true;
            DBContext.context.ReceiptDetails.Add(receiptDetail);
            DBContext.context.SaveChanges();
        }

        public static void UpdateReceiptDetail(ReceiptDetail receiptDetail)
        {
            DBContext.context.ReceiptDetails.Update(receiptDetail);
            DBContext.context.SaveChanges();
        }

        public static void DeleteReceiptDetail(ReceiptDetail receiptDetail)
        {
            receiptDetail.IsActive = false;
            DBContext.context.ReceiptDetails.Update(receiptDetail);
            DBContext.context.SaveChanges();
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
