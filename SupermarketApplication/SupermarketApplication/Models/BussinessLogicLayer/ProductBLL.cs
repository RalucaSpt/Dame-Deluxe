using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SupermarketApplication.Models.BussinessLogicLayer
{
    public class ProductBLL
    {
        public ObservableCollection<Product> ProductList { get; set; }
        public string ErrorMessage { get; set; }

        public ProductBLL()
        {
            ProductList = new ObservableCollection<Product>(GetAllProducts());
        }

        public void AddMethod(object obj)
        {
            Product product = obj as Product;
            if (product != null)
            {
                if (string.IsNullOrEmpty(product.ProductName))
                {
                    ErrorMessage = "Numele produsului trebuie precizat";
                }
                else
                {
                    ProductDAL.AddProduct(product);
                    product.ProductID = DBContext.context.Products.Max(item => item.ProductID);
                    ProductList.Add(product);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Product product = obj as Product;
            if (product == null)
            {
                ErrorMessage = "Selectează un produs";
            }
            else if (string.IsNullOrEmpty(product.ProductName))
            {
                ErrorMessage = "Numele produsului trebuie precizat";
            }
            else
            {
                ProductDAL.UpdateProduct(product);
                var itemIndex = ProductList.IndexOf(product);
                if (itemIndex >= 0)
                {
                    ProductList[itemIndex] = product;
                }
                ErrorMessage = "";
            }
        }

        public void DeleteMethod(object obj)
        {
            Product product = obj as Product;
            if (product == null)
            {
                ErrorMessage = "Selectează un produs";
            }
            else
            {
                ProductDAL.DeleteProduct(product);
                ProductList.Remove(product);
                ErrorMessage = "";
            }
        }

        public ObservableCollection<Product> GetAllProducts()
        {
            List<Product> products = ProductDAL.GetAllProducts();
            ObservableCollection<Product> result = new ObservableCollection<Product>(products);
            return result;
        }
    }
}
