using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SupermarketApplication.Helpers;
using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;
using MVVMAgenda_EFCore.Helpers;
using SupermarketApplication.Models.BussinessLogicLayer;

namespace SupermarketApplication.ViewModels
{
    public class CashierViewModel : BasePropertyChanged
    {
        public CashierViewModel()
        {
            LoadData();
            SearchCriteria = new ProductSearchCriteria();
            ManufacturerList = new ObservableCollection<Manufacturer>(ManufacturerDAL.GetAllManufacturers() ?? new List<Manufacturer>());
            CategoryList = new ObservableCollection<Category>(CategoryDAL.GetAllCategories() ?? new List<Category>());
            SearchResults = new ObservableCollection<Product>();

            SearchProductsCommand = new RelayCommand(SearchProducts);
            AddToReceiptCommand = new RelayCommand(AddToReceipt);
            FinalizeReceiptCommand = new RelayCommand(FinalizeReceipt);
        }

        public ProductSearchCriteria SearchCriteria { get; set; }
        public ObservableCollection<Manufacturer> ManufacturerList { get; set; }
        public ObservableCollection<Category> CategoryList { get; set; }
        public ObservableCollection<Product> ProductList { get; set; }
        public ObservableCollection<Product> SearchResults { get; set; }
        public ObservableCollection<ReceiptDetailVM> CurrentReceiptDetails { get; set; }
        public Product SelectedProduct { get; set; }
        public int SelectedQuantity { get; set; }
        public decimal CurrentReceiptTotal => CurrentReceiptDetails.Sum(detail => detail.Subtotal);

        public ICommand SearchProductsCommand { get; }
        public ICommand AddToReceiptCommand { get; }
        public ICommand FinalizeReceiptCommand { get; }

        private void LoadData()
        {
            ProductList = new ObservableCollection<Product>(ProductDAL.GetAllProducts());
            CurrentReceiptDetails = new ObservableCollection<ReceiptDetailVM>();
            StockDAL.CheckAndMarkExpiredStocks();
        }

        private void SearchProducts(object parameter)
        {
            if (SearchCriteria == null)
            {
                throw new ArgumentNullException(nameof(SearchCriteria));
            }

            var results = ProductDAL.SearchProducts(SearchCriteria) ?? new List<Product>();
            SearchResults.Clear();
            foreach (var result in results)
            {
                SearchResults.Add(result);
            }
        }

        private void AddToReceipt(object parameter)
        {
            if (SelectedProduct != null && SelectedQuantity > 0)
            {
                var stock = StockDAL.GetStockByProductId(SelectedProduct.ProductID);
                if (stock != null && stock.Quantity >= SelectedQuantity)
                {
                    stock.Quantity -= SelectedQuantity;
                    StockDAL.UpdateStock(stock);

                    var receiptDetail = new ReceiptDetailVM
                    {
                        ProductName = SelectedProduct.ProductName,
                        Quantity = SelectedQuantity,
                        Price = stock.SellingPrice,
                        Subtotal = SelectedQuantity * stock.SellingPrice                        
                    };

                    CurrentReceiptDetails.Add(receiptDetail);
                    ReceiptDetailDAL.AddReceiptDetail(new ReceiptDetail
                    {
                        ProductID = SelectedProduct.ProductID,
                        Quantity = SelectedQuantity,
                        Subtotal = receiptDetail.Subtotal,
                        ReceiptID = ReceiptDAL.GetMaxID()
                    });
                    NotifyPropertyChanged(nameof(CurrentReceiptDetails));
                    NotifyPropertyChanged(nameof(CurrentReceiptTotal));

                    // Reset selected quantity after adding to receipt
                    SelectedQuantity = 0;
                    NotifyPropertyChanged(nameof(SelectedQuantity));
                }
                else
                {
                    MessageBox.Show("Not enough stock available for the selected product.", "Stock Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FinalizeReceipt(object parameter)
        {
            if (CurrentReceiptDetails.Count == 0)
            {
                MessageBox.Show("No products added to the receipt.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var receipt = new Receipt
            {
                Date = DateTime.Now,
                TotalAmount = CurrentReceiptTotal,
                CashierID = DBContext.idUser,
                ReceiptDetails = CurrentReceiptDetails.Select(d => new ReceiptDetail
                {
                    ProductID = ProductDAL.GetProductIDByName(d.ProductName),
                    Quantity = d.Quantity,
                    Subtotal = d.Subtotal
                }).ToList()
            };

            ReceiptDAL.AddReceipt(receipt);
            CurrentReceiptDetails.Clear();
            NotifyPropertyChanged(nameof(CurrentReceiptDetails));
            NotifyPropertyChanged(nameof(CurrentReceiptTotal));
            StockDAL.CheckAndMarkExpiredStocks();
        }
    }

    public class ProductSearchCriteria : BasePropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        private string barcode;
        public string Barcode
        {
            get => barcode;
            set
            {
                barcode = value;
                NotifyPropertyChanged(nameof(Barcode));
            }
        }

        private DateTime expiryDate;
        public DateTime ExpiryDate
        {
            get => expiryDate;
            set
            {
                expiryDate = value;
                NotifyPropertyChanged(nameof(ExpiryDate));
            }
        }

        private Manufacturer selectedManufacturer;
        public Manufacturer SelectedManufacturer
        {
            get => selectedManufacturer;
            set
            {
                selectedManufacturer = value;
                NotifyPropertyChanged(nameof(SelectedManufacturer));
            }
        }

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                NotifyPropertyChanged(nameof(SelectedCategory));
            }
        }
    }
}
