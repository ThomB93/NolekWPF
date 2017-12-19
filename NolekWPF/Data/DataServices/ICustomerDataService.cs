using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.DataServices
{
    interface ICustomerDataService
    {
        Task<Customer> GetByIdAsync(int customerId);
        Task<IEnumerable<CustomerDto>> GetCustomerLookupAsync();
    }
}