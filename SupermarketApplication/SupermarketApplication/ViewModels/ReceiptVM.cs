using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.BussinessLogicLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SupermarketApplication.ViewModels
{
    public class ReceiptVM : BasePropertyChanged
    {
        private ReceiptBLL receiptBLL;
        public ReceiptVM()
        {
            receiptBLL = new ReceiptBLL();
        }

        public ObservableCollection<ReceiptDetail> ReceiptDetail { get; set; }


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

        public void AddMethod(object obj)
        {
            receiptBLL.AddMethod(obj);
            ErrorMessage = receiptBLL.ErrorMessage;
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
            receiptBLL.UpdateMethod(obj);
            ErrorMessage = receiptBLL.ErrorMessage;
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
            receiptBLL.DeleteMethod(obj);
            ErrorMessage = receiptBLL.ErrorMessage;
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
    }
}
