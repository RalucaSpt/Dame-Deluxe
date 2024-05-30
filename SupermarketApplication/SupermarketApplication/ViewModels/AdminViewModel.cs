using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.BussinessLogicLayer;

namespace SupermarketApplication.ViewModels
{
    public class AdminViewModel : BasePropertyChanged
    {
        public AdminViewModel()
        {
            ManufacturerList = new ObservableCollection<Manufacturer>(ManufacturerDAL.GetAllManufacturers());
            UserList = new ObservableCollection<Username>(UsernameDAL.GetAllUsernames());
            ListProductsByManufacturerCommand = new RelayCommand(ListProductsByManufacturer);
            ShowCategoryValuesCommand = new RelayCommand(ShowCategoryValues);
            ShowSalesByUserAndMonthCommand = new RelayCommand(ShowSalesByUserAndMonth);
            ShowLargestReceiptCommand = new RelayCommand(ShowLargestReceipt);
            ReceiptList = new ObservableCollection<Receipt>(ReceiptDAL.GetAllReceipts());
            for (int i = 0; i < ReceiptList.Count(); i++)
            {
                ReceiptList[i].ReceiptDetails = ReceiptDAL.GetReceiptDetails(ReceiptList[i]);
            }

        }

        public ObservableCollection<Receipt> ReceiptList { get; set; }
        public ObservableCollection<Manufacturer> ManufacturerList { get; set; }
        public Manufacturer SelectedManufacturer { get; set; }
        public ObservableCollection<CategoryWithProducts> ProductsByManufacturer { get; set; } // Updated type

        public ObservableCollection<CategoryValue> CategoryValues { get; set; }

        public ObservableCollection<Username> UserList { get; set; }
        public Username SelectedUser { get; set; }
        public DateTime? SelectedMonth { get; set; }
        public ObservableCollection<SalesByDay> SalesByUserAndMonth { get; set; }

        public DateTime? SelectedDate { get; set; }
        public ObservableCollection<ReceiptDetail> LargestReceiptDetails { get; set; }

        public ICommand ListProductsByManufacturerCommand { get; }
        public ICommand ShowCategoryValuesCommand { get; }
        public ICommand ShowSalesByUserAndMonthCommand { get; }
        public ICommand ShowLargestReceiptCommand { get; }

        private void ListProductsByManufacturer(object parameter)
        {
            if (SelectedManufacturer != null)
            {
                var products = ProductDAL.GetProductsByManufacturer(SelectedManufacturer.ManufacturerId);
                var groupedProducts = products
                    .GroupBy(p => p.Category.CategoryName)
                    .Select(g => new CategoryWithProducts
                    {
                        CategoryName = g.Key,
                        Products = new ObservableCollection<Product>(g.ToList())
                    })
                    .ToList();
                ProductsByManufacturer = new ObservableCollection<CategoryWithProducts>(groupedProducts);
                NotifyPropertyChanged(nameof(ProductsByManufacturer));
            }
        }

        private void ShowCategoryValues(object parameter)
        {
            CategoryValues = new ObservableCollection<CategoryValue>(CategoryDAL.GetCategoryValues());
            NotifyPropertyChanged(nameof(CategoryValues));
        }

        private void ShowSalesByUserAndMonth(object parameter)
        {
            if (SelectedUser != null && SelectedMonth != null)
            {
                SalesByUserAndMonth = new ObservableCollection<SalesByDay>(ReceiptDAL.GetSalesByUserAndMonth(SelectedUser.UserID, SelectedMonth.Value));
                NotifyPropertyChanged(nameof(SalesByUserAndMonth));
            }
        }

        private void ShowLargestReceipt(object parameter)
        {
            if (SelectedDate != null)
            {
                var receipts = ReceiptDAL.GetLargestReceiptOfDay(SelectedDate.Value);
                if (receipts != null && receipts.Count > 0)
                {
                    LargestReceiptDetails = new ObservableCollection<ReceiptDetail>(receipts.First().ReceiptDetails);
                }
                else
                {
                    LargestReceiptDetails = new ObservableCollection<ReceiptDetail>();
                }
                NotifyPropertyChanged(nameof(LargestReceiptDetails));
            }
        }
    }

    public class CategoryValue
    {
        public string CategoryName { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class SalesByDay
    {
        public DateTime Day { get; set; }
        public decimal TotalSales { get; set; }
    }

    public class CategoryWithProducts
    {
        public string CategoryName { get; set; }
        public ObservableCollection<Product> Products { get; set; }
    }

}
