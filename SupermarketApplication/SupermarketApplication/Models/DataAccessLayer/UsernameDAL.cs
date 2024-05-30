using Microsoft.EntityFrameworkCore;
using SupermarketApplication.Models.EntityLayer;

namespace SupermarketApplication.Models.DataAccessLayer
{
    public static class UsernameDAL
    {
        public static List<Username> GetAllUsernames()
        {
            return DBContext.context.Usernames.Include(u => u.Receipts).ToList();
        }

        public static void AddUsername(Username username)
        {
            DBContext.context.Usernames.Add(username);
            DBContext.context.SaveChanges();
        }

        public static void UpdateUsername(Username username)
        {
            DBContext.context.Usernames.Update(username);
            DBContext.context.SaveChanges();
        }

        public static void DeleteUsername(Username username)
        {
            DBContext.context.Usernames.Remove(username);
            DBContext.context.SaveChanges();
        }

        public static Username ValidateUser(string username, string password, string role)
        {
            return DBContext.context.Usernames.FirstOrDefault(u => u.UserName == username && u.Password == password && u.UserType == role);
        }
    }
}
