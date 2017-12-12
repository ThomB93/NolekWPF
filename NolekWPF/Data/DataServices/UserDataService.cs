using NolekWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Data.DataServices
{
    public class UserDataService : IUserDataService
    {
        public List<User> GetUser()
        {
            //TODO: Load data from real database
            List<User> Users = new List<User>();
            Users.Add(new User { Username = "UserAdmin", Password = "123", SecurityLevel = 1, LoggedIn = false});
            Users.Add(new User { Username = "UserRegular", Password = "123", SecurityLevel = 2, LoggedIn = false });
            return Users;
        }

        public User GetUserInstance(string username)
        {
            List<User> Users = GetUser();
            User user = new User();
            foreach (var u in Users)
            {
                if (u.Username == username)
                {
                    u.LoggedIn = true;
                    user.Username = u.Username;
                    user.SecurityLevel = u.SecurityLevel;
                    user.LoggedIn = u.LoggedIn;
                    break;
                }
            }
            return user;
        }
    }
}
