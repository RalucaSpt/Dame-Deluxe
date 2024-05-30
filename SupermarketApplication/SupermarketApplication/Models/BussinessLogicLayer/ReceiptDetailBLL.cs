using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SupermarketApplication.Models.BussinessLogicLayer
{
    public class ReceiptDetailBLL
    {
        public ObservableCollection<ReceiptDetail> ReceiptDetailList { get; set; }
        public string ErrorMessage { get; set; }

        public ReceiptDetailBLL()
        {
            ReceiptDetailList = new ObservableCollection<ReceiptDetail>(GetAllReceiptDetails());
        }

        public void AddMethod(object obj)
        {
            ReceiptDetail receiptDetail = obj as ReceiptDetail;
            if (receiptDetail != null)
            {
                ReceiptDetailDAL.AddReceiptDetail(receiptDetail);
                receiptDetail.ReceiptDetailID = DBContext.context.ReceiptDetails.Max(item => item.ReceiptDetailID);
                ReceiptDetailList.Add(receiptDetail);
                ErrorMessage = "";
            }
        }

        public void UpdateMethod(object obj)
        {
            ReceiptDetail receiptDetail = obj as ReceiptDetail;
            if (receiptDetail == null)
            {
                ErrorMessage = "Select a receipt detail";
            }
            else
            {
                ReceiptDetailDAL.UpdateReceiptDetail(receiptDetail);
                var itemIndex = ReceiptDetailList.IndexOf(receiptDetail);
                if (itemIndex >= 0)
                {
                    ReceiptDetailList[itemIndex] = receiptDetail;
                }
                ErrorMessage = "";
            }
        }

        public void DeleteMethod(object obj)
        {
            ReceiptDetail receiptDetail = obj as ReceiptDetail;
            if (receiptDetail == null)
            {
                ErrorMessage = "Select a receipt detail";
            }
            else
            {
                ReceiptDetailDAL.DeleteReceiptDetail(receiptDetail);
                ReceiptDetailList.Remove(receiptDetail);
                ErrorMessage = "";
            }
        }

        public ObservableCollection<ReceiptDetail> GetAllReceiptDetails()
        {
            List<ReceiptDetail> receiptDetails = ReceiptDetailDAL.GetAllReceiptDetails();
            ObservableCollection<ReceiptDetail> result = new ObservableCollection<ReceiptDetail>(receiptDetails);
            return result;
        }
    }
}
