using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.BussinessLogicLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SupermarketApplication.ViewModels
{
    public class ReceiptDetailVM : BasePropertyChanged
    {
        private ReceiptDetailBLL receiptDetailBLL;
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }  
        public decimal Subtotal { get; set; }

        public ReceiptDetailVM()
        {
            receiptDetailBLL = new ReceiptDetailBLL();
            ReceiptDetailList = receiptDetailBLL.GetAllReceiptDetails();
        }

        #region Data Members

        public ObservableCollection<ReceiptDetail> ReceiptDetailList
        {
            get => receiptDetailBLL.ReceiptDetailList;
            set
            {
                receiptDetailBLL.ReceiptDetailList = value;
                NotifyPropertyChanged("ReceiptDetailList");
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        #endregion

        #region Command Members

        public void AddMethod(object obj)
        {
            receiptDetailBLL.AddMethod(obj);
            ErrorMessage = receiptDetailBLL.ErrorMessage;
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(AddMethod);
                }
                return addCommand;
            }
        }

        public void UpdateMethod(object obj)
        {
            receiptDetailBLL.UpdateMethod(obj);
            ErrorMessage = receiptDetailBLL.ErrorMessage;
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(UpdateMethod);
                }
                return updateCommand;
            }
        }

        public void DeleteMethod(object obj)
        {
            receiptDetailBLL.DeleteMethod(obj);
            ErrorMessage = receiptDetailBLL.ErrorMessage;
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(DeleteMethod);
                }
                return deleteCommand;
            }
        }

        #endregion
    }
}
