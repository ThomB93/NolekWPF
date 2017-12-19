using NolekWPF.DataAccess;
using NolekWPF.Model;
using NolekWPF.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Data.DataServices
{
    public class CustomerDataService : ICustomerDataService
    {
        private Func<wiki_nolek_dk_dbEntities> _contextCreator;

        public CustomerDataService(Func<wiki_nolek_dk_dbEntities> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Customers.AsNoTracking().SingleAsync(f => f.CustomerId == customerId);
            }
        }

        //return all customers with associated departments and equipment, save to customerDto class instances
        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Customers.AsNoTracking().Select(f => new CustomerDto
                {
                    CustomerID = f.CustomerId,
                    CustomerName = f.CustomerName,
                    EquipmentsList = f.Equipments.ToList(),
                    DepartmentsList = f.CustomerDepartments.ToList()
                }).ToListAsync();
            }
        }

        
    }
}
