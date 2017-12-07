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
            Users.Add(new User { Username = "UserAdmin", Password = "123", Role = "Admin" });
            Users.Add(new User { Username = "UserRegular", Password = "123", Role = "Regular" });
            return Users;
        }
    }
}
