using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApplication.Models.BussinessLogicLayer
{
    public class ManufacturerBLL
    {
        public ObservableCollection<Manufacturer> ManufacturerList { get; set; }
        public string ErrorMessage { get; set; }

        public ManufacturerBLL()
        {
            ManufacturerList = new ObservableCollection<Manufacturer>(GetAllManufacturers());
        }

        public void AddMethod(object obj)
        {
            Manufacturer manufacturer = obj as Manufacturer;
            if (manufacturer != null)
            {
                if (string.IsNullOrEmpty(manufacturer.ManufacturerName))
                {
                    ErrorMessage = "Numele producătorului trebuie precizat";
                }
                else
                {
                    ManufacturerDAL.AddManufacturer(manufacturer);
                    manufacturer.ManufacturerId = DBContext.context.Manufacturers.Max(item => item.ManufacturerId);
                    ManufacturerList.Add(manufacturer);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Manufacturer manufacturer = obj as Manufacturer;
            if (manufacturer == null)
            {
                ErrorMessage = "Selectează un producător";
            }
            else if (string.IsNullOrEmpty(manufacturer.ManufacturerName))
            {
                ErrorMessage = "Numele producătorului trebuie precizat";
            }
            else
            {
                ManufacturerDAL.UpdateManufacturer(manufacturer);
                var itemIndex = ManufacturerList.IndexOf(manufacturer);
                if (itemIndex >= 0)
                {
                    ManufacturerList[itemIndex] = manufacturer;
                }
                ErrorMessage = "";
            }
        }

        public void DeleteMethod(object obj)
        {
            Manufacturer manufacturer = obj as Manufacturer;
            if (manufacturer == null)
            {
                ErrorMessage = "Selectează un producător";
            }
            else
            {
                ManufacturerDAL.DeleteManufacturer(manufacturer);
                ManufacturerList.Remove(manufacturer);
                ErrorMessage = "";
            }
        }

        public ObservableCollection<Manufacturer> GetAllManufacturers()
        {
            List<Manufacturer> manufacturers = ManufacturerDAL.GetAllManufacturers();
            ObservableCollection<Manufacturer> result = new ObservableCollection<Manufacturer>(manufacturers);
            return result;
        }
    }
}
