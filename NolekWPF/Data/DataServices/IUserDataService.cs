using System.Collections.Generic;
using NolekWPF.Model;

namespace NolekWPF.Data.DataServices
{
    public interface IUserDataService
    {
        List<User> GetUser();
    }
}