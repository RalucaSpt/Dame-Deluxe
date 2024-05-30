using SupermarketApplication.Views;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SupermarketApplication.Helpers
{
    public class NavigationService
    {
        public void NavigateTo(string pageName)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow == null)
                throw new InvalidOperationException("Main window not found");

            switch (pageName)
            {
                case "AdminPage":
                    mainWindow.MainFrame.Navigate(new Uri("Views/AdminPage.xaml", UriKind.Relative));
                    break;
                case "CashierPage":
                    mainWindow.MainFrame.Navigate(new Uri("Views/CashierPage.xaml", UriKind.Relative));
                    break;
                default:
                    throw new InvalidOperationException("Page not found");
            }
        }
    }
}
