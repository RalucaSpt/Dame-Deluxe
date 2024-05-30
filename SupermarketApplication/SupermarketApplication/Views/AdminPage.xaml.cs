using SupermarketApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupermarketApplication.Views
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public CategoriesVM CategoriesViewModel { get; set; }
        // Adaugă ViewModel-uri pentru alte modele aici

        public AdminPage()
        {
            CategoriesViewModel = new CategoriesVM();
            // Instanțiază ViewModel-urile pentru alte modele aici
        }
    }
}

