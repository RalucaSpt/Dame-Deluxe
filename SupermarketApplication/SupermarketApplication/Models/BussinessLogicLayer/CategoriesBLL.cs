using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SupermarketApplication.Models;
using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;

namespace SupermarketApplication.Models.BussinessLogicLayer
{
    public class CategoriesBLL
    {
        public ObservableCollection<Category> CategoryList { get; set; }
        public string ErrorMessage { get; set; }

        public CategoriesBLL()
        {
            CategoryList = new ObservableCollection<Category>(GetAllCategories());
        }

        public void AddMethod(object obj)
        {
            Category category = obj as Category;
            if (category != null)
            {
                if (string.IsNullOrEmpty(category.CategoryName))
                {
                    ErrorMessage = "Numele categoriei trebuie precizat";
                }
                else
                {
                    CategoryDAL.AddCategory(category);
                    category.CategoryId = DBContext.context.Categories.Max(item => item.CategoryId);
                    CategoryList.Add(category);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Category category = obj as Category;
            if (category == null)
            {
                ErrorMessage = "Selectează o categorie";
            }
            else if (string.IsNullOrEmpty(category.CategoryName))
            {
                ErrorMessage = "Numele categoriei trebuie precizat";
            }
            else
            {
                CategoryDAL.UpdateCategory(category);
                var itemIndex = CategoryList.IndexOf(category);
                if (itemIndex >= 0)
                {
                    CategoryList[itemIndex] = category;
                }
                ErrorMessage = "";
            }
        }

        public void DeleteMethod(object obj)
        {
            Category category = obj as Category;
            if (category == null)
            {
                ErrorMessage = "Selectează o categorie";
            }
            else
            {
                CategoryDAL.DeleteCategory(category);
                CategoryList.Remove(category);
                ErrorMessage = "";
            }
        }

        public ObservableCollection<Category> GetAllCategories()
        {
            List<Category> categories = CategoryDAL.GetAllCategories();
            ObservableCollection<Category> result = new ObservableCollection<Category>(categories);
            return result;
        }
    }
}
