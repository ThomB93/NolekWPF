using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.Repositories
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        Task<Customer> GetByIdAsync(int customId);
        Task<IEnumerable<CustomerDto>> GetCustomers();
        bool HasChanges();
        void Remove(Customer model);
        Task SaveAsync();
    }
}