using SupermarketApplication.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace SupermarketApplication.Models.DataAccessLayer
{
    static class ManufacturerDAL
    {
        public static List<Manufacturer> GetAllManufacturers()
        {
            return DBContext.context.Manufacturers.FromSqlRaw("EXEC sp_SelectAllActiveManufacturers").ToList();
        }

        public static void AddManufacturer(Manufacturer manufacturer)
        {
            DBContext.context.Database.ExecuteSqlRaw("EXEC sp_InsertManufacturer @p0, @p1", manufacturer.ManufacturerName, manufacturer.Country);
            DBContext.context.SaveChanges();
        }

        public static void UpdateManufacturer(Manufacturer manufacturer)
        {
            DBContext.context.Database.ExecuteSqlRaw("EXEC sp_UpdateManufacturer @p0, @p1, @p2", manufacturer.ManufacturerId, manufacturer.ManufacturerName, manufacturer.Country);
            DBContext.context.SaveChanges();
        }

        public static void DeleteManufacturer(Manufacturer manufacturer)
        {
            DBContext.context.Database.ExecuteSqlRaw("EXEC sp_SoftDeleteManufacturer @p0", manufacturer.ManufacturerId);
            DBContext.context.SaveChanges();
        }
    }
}
