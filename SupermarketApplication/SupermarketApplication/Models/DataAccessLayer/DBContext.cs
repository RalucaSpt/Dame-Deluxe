using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApplication.Models.DataAccessLayer
{
    static class DBContext
    {
        public static SupermarketDbContext context = new SupermarketDbContext();
        static public int idUser = 2;
    }
}
