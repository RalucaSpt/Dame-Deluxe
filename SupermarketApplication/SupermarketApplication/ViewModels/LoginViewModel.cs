using SupermarketApplication.Helpers;
using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MVVMAgenda_EFCore.Helpers;
using System.Collections.ObjectModel;

namespace SupermarketApplication.ViewModels
{
    public class LoginViewModel : BasePropertyChanged
    {
        private readonly NavigationService _navigationService;

        public LoginViewModel()
        {
            _navigationService = new NavigationService();
            Roles = new ObservableCollection<string> { "casier", "admin" };
            LoginCommand = new RelayCommand(Login);
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }

        private string selectedRole;
        public string SelectedRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                NotifyPropertyChanged(nameof(SelectedRole));
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                NotifyPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ObservableCollection<string> Roles { get; }

        public ICommand LoginCommand { get; }

        private void Login(object parameter)
        {
            // Perform authentication logic here
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(SelectedRole))
            {
                ErrorMessage = "All fields are required.";
                return;
            }

            // Validate user credentials using DAL
            var user = UsernameDAL.ValidateUser(Username, Password, SelectedRole);
            if (user != null)
            {
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                if (currentWindow != null)
                {
                    if (SelectedRole == "admin" || SelectedRole == "casier")
                    {
                        ErrorMessage = $"{SelectedRole} logged in successfully.";

                        // Open MainWindow
                        MainWindow mainWindow = new MainWindow();
                        DBContext.idUser = user.UserID;
                        Application.Current.MainWindow = mainWindow;
                        _navigationService.NavigateTo(SelectedRole == "admin" ? "AdminPage" : "CashierPage");
                        mainWindow.Show();

                        // Close LoginWindow
                        currentWindow.Close();
                    }
                }
            }
            else
            {
                ErrorMessage = "Invalid credentials.";
            }
        }
    }
}
