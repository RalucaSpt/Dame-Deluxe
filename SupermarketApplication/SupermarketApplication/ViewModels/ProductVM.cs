using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.BussinessLogicLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SupermarketApplication.ViewModels
{
    public class ProductVM : BasePropertyChanged
    {
        private ProductBLL productBLL;

        public ProductVM()
        {
            productBLL = new ProductBLL();
            ProductList = productBLL.GetAllProducts();
        }

        #region Data Members

        public ObservableCollection<Product> ProductList
        {
            get => productBLL.ProductList;
            set
            {
                   productBLL.ProductList = value; 
                NotifyPropertyChanged("ProductList");
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
            productBLL.AddMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
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
            productBLL.UpdateMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
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
            productBLL.DeleteMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
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
