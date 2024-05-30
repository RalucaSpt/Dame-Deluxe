using SupermarketApplication.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupermarketApplication.ViewModels;


namespace SupermarketApplication.Models.DataAccessLayer
{
    static class CategoryDAL
    {
        public static List<Category> GetAllCategories()
        {
            return DBContext.context.Categories.FromSqlRaw("EXEC sp_SelectAllActiveCategories").ToList();
        }

        public static void AddCategory(Category category)
        {
            DBContext.context.Database.ExecuteSqlRaw("EXEC sp_InsertCategory @p0", category.CategoryName);
            DBContext.context.SaveChanges();
        }

        public static void UpdateCategory(Category category)
        {
            DBContext.context.Database.ExecuteSqlRaw("EXEC sp_UpdateCategory @p0, @p1", category.CategoryId, category.CategoryName);
            DBContext.context.SaveChanges();
        }

        public static void DeleteCategory(Category category)
        {
            DBContext.context.Database.ExecuteSqlRaw("EXEC sp_SoftDeleteCategory @p0", category.CategoryId);
            DBContext.context.SaveChanges();
        }

        public static List<CategoryValue> GetCategoryValues()
        {
            var stockValues = DBContext.context.Stocks
                .Include(s => s.Product)
                .ThenInclude(p => p.Category)
                .Where(s => s.IsActive)
                .Select(s => new
                {
                    CategoryName = s.Product.Category.CategoryName,
                    StockValue = s.SellingPrice * s.Quantity
                })
                .ToList();

            var categoryValues = stockValues
                .GroupBy(sv => sv.CategoryName)
                .Select(g => new CategoryValue
                {
                    CategoryName = g.Key,
                    TotalValue = g.Sum(sv => sv.StockValue)
                })
                .ToList();

            return categoryValues;
        }
    }
}
