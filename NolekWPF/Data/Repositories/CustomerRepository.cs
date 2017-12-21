using NolekWPF.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.Model;
using System.Data.Entity;
using NolekWPF.Model.Dto;

namespace NolekWPF.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private wiki_nolek_dk_dbEntities _context;

        public CustomerRepository(wiki_nolek_dk_dbEntities context)
        {
            _context = context; //context is kept alive throughout the application lifetime
        }

        public void AddCustomer(Customer customer)
        {
             _context.Customers.Add(customer); //call insert to add new equipement to table
        }

        public void AddCustomerDepartment(CustomerDepartment customerDepartment)
        {
             _context.CustomerDepartments.Add(customerDepartment);
        }

        public void AddCustomerEquipment(Model.Equipment equipment, int customerId)
        {
             _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault().Equipments.Add(equipment);
        }

        public async Task<Customer> GetByIdAsync(int customId)
        {
            return await _context.Customers.SingleAsync(f => f.CustomerId == customId); //return equipement with the id
        }
        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
                return await _context.Customers.Select(f => new CustomerDto
                {
                    CustomerID = f.CustomerId,
                    CustomerName = f.CustomerName,
                    EquipmentsList = f.Equipments.ToList(),
                    DepartmentsList = f.CustomerDepartments.ToList()
                }).ToListAsync();
        }


        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges(); //return true if current equipement has changes
        }

        public void Remove(Customer model)
        {
            _context.Customers.Remove(model); //delete equipment from the db
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); //save all changes to the current context
        }
    }
}
