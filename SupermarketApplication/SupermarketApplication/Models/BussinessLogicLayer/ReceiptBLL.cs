using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SupermarketApplication.Models.BussinessLogicLayer
{
    public class ReceiptBLL
    {
        public ObservableCollection<Receipt> ReceiptList { get; set; }
        public string ErrorMessage { get; set; }

        ObservableCollection<ReceiptDetail> ReceiptDetails { get; set; }

        public ReceiptBLL()
        {
            ReceiptList = new ObservableCollection<Receipt>(GetAllReceipts());
        }

        public void AddMethod(object obj)
        {
            Receipt receipt = obj as Receipt;
            if (receipt != null)
            {
                if (receipt.CashierID == 0)
                {
                    ErrorMessage = "Cashier ID must be specified";
                }
                else
                {
                    ReceiptDAL.AddReceipt(receipt);
                    receipt.ReceiptID = DBContext.context.Receipts.Max(item => item.ReceiptID);
                    ReceiptList.Add(receipt);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Receipt receipt = obj as Receipt;
            if (receipt == null)
            {
                ErrorMessage = "Select a receipt";
            }
            else
            {
                ReceiptDAL.UpdateReceipt(receipt);
                var itemIndex = ReceiptList.IndexOf(receipt);
                if (itemIndex >= 0)
                {
                    ReceiptList[itemIndex] = receipt;
                }
                ErrorMessage = "";
            }
        }

        public void DeleteMethod(object obj)
        {
            Receipt receipt = obj as Receipt;
            if (receipt == null)
            {
                ErrorMessage = "Select a receipt";
            }
            else
            {
                ReceiptDAL.DeleteReceipt(receipt);
                ReceiptList.Remove(receipt);
                ErrorMessage = "";
            }
        }

        public ObservableCollection<Receipt> GetAllReceipts()
        {
            List<Receipt> receipts = ReceiptDAL.GetAllReceipts();
            ObservableCollection<Receipt> result = new ObservableCollection<Receipt>(receipts);
            return result;
        }

    }
}
