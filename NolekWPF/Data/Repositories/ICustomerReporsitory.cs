using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.Data.Repositories
{
    interface ICustomerReporsitory
    {
        void Add(Customer customer);
        Task<Customer> GetByIdAsync(int customId);
        bool HasChanges();
        void Remove(Customer model);
        Task SaveAsync();
    }
}