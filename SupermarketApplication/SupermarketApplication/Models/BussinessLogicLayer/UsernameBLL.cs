using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SupermarketApplication.Models.BussinessLogicLayer
{
    public class UsernameBLL
    {
        public ObservableCollection<Username> UsernameList { get; set; }
        public string ErrorMessage { get; set; }

        public UsernameBLL()
        {
            UsernameList = new ObservableCollection<Username>(GetAllUsernames());
        }

        public void AddMethod(object obj)
        {
            Username username = obj as Username;
            if (username != null)
            {
                if (string.IsNullOrEmpty(username.UserName))
                {
                    ErrorMessage = "Username must be specified";
                }
                else
                {
                    UsernameDAL.AddUsername(username);
                    username.UserID = DBContext.context.Usernames.Max(item => item.UserID);
                    UsernameList.Add(username);
                    ErrorMessage = "";
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Username username = obj as Username;
            if (username == null)
            {
                ErrorMessage = "Select a username";
            }
            else if (string.IsNullOrEmpty(username.UserName))
            {
                ErrorMessage = "Username must be specified";
            }
            else
            {
                UsernameDAL.UpdateUsername(username);
                var itemIndex = UsernameList.IndexOf(username);
                if (itemIndex >= 0)
                {
                    UsernameList[itemIndex] = username;
                }
                ErrorMessage = "";
            }
        }

        public void DeleteMethod(object obj)
        {
            Username username = obj as Username;
            if (username == null)
            {
                ErrorMessage = "Select a username";
            }
            else
            {
                UsernameDAL.DeleteUsername(username);
                UsernameList.Remove(username);
                ErrorMessage = "";
            }
        }

        public ObservableCollection<Username> GetAllUsernames()
        {
            List<Username> usernames = UsernameDAL.GetAllUsernames();
            ObservableCollection<Username> result = new ObservableCollection<Username>(usernames);
            return result;
        }
    }
}
