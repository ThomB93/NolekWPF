using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.Repositories
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        void AddCustomerDepartment(CustomerDepartment customerDepartment);
        void AddCustomerEquipment(Model.Equipment equipment, int customerId);
        Task<Customer> GetByIdAsync(int customId);
        Task<IEnumerable<CustomerDto>> GetCustomers();
        bool HasChanges();
        void Remove(Customer model);
        Task SaveAsync();
    }
}