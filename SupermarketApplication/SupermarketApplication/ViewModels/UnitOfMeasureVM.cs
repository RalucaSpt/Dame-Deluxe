using MVVMAgenda_EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApplication.ViewModels
{
    public class UnitOfMeasureViewModel : BasePropertyChanged
    {
        public ObservableCollection<string> UnitOfMeasureList { get; set; }

        public UnitOfMeasureViewModel()
        {
            UnitOfMeasureList = new ObservableCollection<string>
        {
            "kg",
            "g",
            "l",
            "ml",
            "pcs"
        };
        }
    }
}
