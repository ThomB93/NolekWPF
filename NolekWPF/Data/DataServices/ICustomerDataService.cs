using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.DataServices
{
    public interface ICustomerDataService
    {
        Task<Customer> GetByIdAsync(int customerId);
        Task<IEnumerable<CustomerDto>> GetCustomers();
    }
}