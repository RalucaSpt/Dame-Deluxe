using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.BussinessLogicLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SupermarketApplication.ViewModels
{
    public class StockVM : BasePropertyChanged
    {
        private StockBLL stockBLL;

        public StockVM()
        {
            stockBLL = new StockBLL();
            StockList = stockBLL.GetAllStocks();
        }

        #region Data Members

        public ObservableCollection<Stock> StockList
        {
            get => stockBLL.StockList;
            set
            {
                stockBLL.StockList = value;
                NotifyPropertyChanged("StockList");
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
            stockBLL.AddMethod(obj);
            ErrorMessage = stockBLL.ErrorMessage;
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
            stockBLL.UpdateMethod(obj);
            ErrorMessage = stockBLL.ErrorMessage;
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
            stockBLL.DeleteMethod(obj);
            ErrorMessage = stockBLL.ErrorMessage;
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
