using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.BussinessLogicLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SupermarketApplication.ViewModels
{
    public class UsernameVM : BasePropertyChanged
    {
        private UsernameBLL usernameBLL;
        public UsernameVM()
        {
            usernameBLL = new UsernameBLL();
            UsernameList = usernameBLL.GetAllUsernames();
        }

        public ObservableCollection<Username> UsernameList
        {
            get => usernameBLL.UsernameList;
            set => usernameBLL.UsernameList = value;
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

        public void AddMethod(object obj)
        {
            usernameBLL.AddMethod(obj);
            ErrorMessage = usernameBLL.ErrorMessage;
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
            usernameBLL.UpdateMethod(obj);
            ErrorMessage = usernameBLL.ErrorMessage;
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
            usernameBLL.DeleteMethod(obj);
            ErrorMessage = usernameBLL.ErrorMessage;
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
