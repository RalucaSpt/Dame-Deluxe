using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApplication.ViewModels
{
    public class MainViewModel : BasePropertyChanged
    {
        public CategoriesVM CategoriesViewModel { get; set; }
        // Adaugă ViewModel-uri pentru alte modele aici

        public MainViewModel()
        {
            CategoriesViewModel = new CategoriesVM();
            // Instanțiază ViewModel-urile pentru alte modele aici
        }
    }
}

