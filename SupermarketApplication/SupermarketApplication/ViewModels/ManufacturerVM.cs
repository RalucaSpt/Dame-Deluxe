using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.BussinessLogicLayer;
using SupermarketApplication.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SupermarketApplication.ViewModels
{
    public class ManufacturerVM : BasePropertyChanged
    {
        private ManufacturerBLL manufacturerBLL;

        public ManufacturerVM()
        {
            manufacturerBLL = new ManufacturerBLL();
            ManufacturerList = manufacturerBLL.GetAllManufacturers();
        }

        #region Data Members

        public ObservableCollection<Manufacturer> ManufacturerList
        {
            get => manufacturerBLL.ManufacturerList;
            set => manufacturerBLL.ManufacturerList = value;
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
            manufacturerBLL.AddMethod(obj);
            ErrorMessage = manufacturerBLL.ErrorMessage;
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
            manufacturerBLL.UpdateMethod(obj);
            ErrorMessage = manufacturerBLL.ErrorMessage;
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
            manufacturerBLL.DeleteMethod(obj);
            ErrorMessage = manufacturerBLL.ErrorMessage;
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
