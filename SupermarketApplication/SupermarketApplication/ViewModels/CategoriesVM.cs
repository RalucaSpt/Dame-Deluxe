using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.BussinessLogicLayer;
using SupermarketApplication.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SupermarketApplication.ViewModels
{
    public class CategoriesVM : BasePropertyChanged
    {
        private CategoriesBLL categoryBLL;
        public CategoriesVM()
        {
            categoryBLL = new CategoriesBLL();
            CategoryList = categoryBLL.GetAllCategories();
        }

        #region Data Members

        public ObservableCollection<Category> CategoryList
        {
            get => categoryBLL.CategoryList;
            set => categoryBLL.CategoryList = value;
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
            categoryBLL.AddMethod(obj);
            ErrorMessage = categoryBLL.ErrorMessage;
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
            categoryBLL.UpdateMethod(obj);
            ErrorMessage = categoryBLL.ErrorMessage;
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
            categoryBLL.DeleteMethod(obj);

            ErrorMessage = categoryBLL.ErrorMessage;
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

        //public void GetPhonesForPerson(object obj)
        //{
        //    categoryBLL.GetPhonesForPerson(obj);
        //    ErrorMessage = categoryBLL.ErrorMessage;
        //}

        ////asta este pt SelectionChanged pe ComboBox
        //private ICommand changePersonCommand;
        //public ICommand ChangePersonCommand
        //{
        //    get
        //    {
        //        if (changePersonCommand == null)
        //        {
        //            changePersonCommand = new RelayCommand(GetPhonesForPerson);
        //        }
        //        return changePersonCommand;
        //    }
        //}
        #endregion
    }

}
