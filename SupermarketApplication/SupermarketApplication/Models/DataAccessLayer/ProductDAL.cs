using Microsoft.EntityFrameworkCore;
using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;
using SupermarketApplication.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketApplication.Models.BussinessLogicLayer
{
    static class ProductDAL
    {
        public static List<Product> GetAllProducts()
        {
            return DBContext.context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Where(p => p.IsActive) 
                .ToList();
        }

        public static void AddProduct(Product product)
        {
            product.IsActive = true; 
            DBContext.context.Database.ExecuteSqlRaw(
                "EXEC sp_InsertProduct @p0, @p1, @p2, @p3",
                product.ProductName, product.Barcode, product.CategoryID, product.ManufacturerID);
            DBContext.context.SaveChanges();
        }

        public static void UpdateProduct(Product product)
        {
            DBContext.context.Database.ExecuteSqlRaw(
                "EXEC sp_UpdateProduct @p0, @p1, @p2, @p3, @p4",
                product.ProductID, product.ProductName, product.Barcode, product.CategoryID, product.ManufacturerID);
            DBContext.context.SaveChanges();
        }

        public static void DeleteProduct(Product product)
        {
            product.IsActive = false; 
            DBContext.context.Products.Update(product);
            DBContext.context.SaveChanges();
        }

        public static List<Product> GetProductsByManufacturer(int manufacturerId)
        {
            return DBContext.context.Products
                .Where(p => p.ManufacturerID == manufacturerId)
                .Include(p => p.Category)
                .ToList();
        }

        public static List<Product> SearchProducts(ProductSearchCriteria criteria)
        {
            var query = DBContext.context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(p => p.ProductName.Contains(criteria.Name));
            }
            if (!string.IsNullOrEmpty(criteria.Barcode))
            {
                query = query.Where(p => p.Barcode == criteria.Barcode);
            }
            if (criteria.ExpiryDate != default(DateTime))
            {
                query = query.Where(p => p.Stocks.Any(s => s.ExpiryDate == criteria.ExpiryDate));
            }
            if (criteria.SelectedManufacturer != null)
            {
                query = query.Where(p => p.ManufacturerID == criteria.SelectedManufacturer.ManufacturerId);
            }
            if (criteria.SelectedCategory != null)
            {
                query = query.Where(p => p.CategoryID == criteria.SelectedCategory.CategoryId);
            }

            return query.Include(p => p.Manufacturer)
                        .Include(p => p.Category)
                        .Include(p => p.Stocks)
                        .ToList();
        }

        public static int GetProductIDByName(string productName)
        {
            return DBContext.context.Products
                .FirstOrDefault(p => p.ProductName == productName)
                .ProductID;
        }
    }
}
