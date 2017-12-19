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
    class CustomerDataService : ICustomerDataService
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

        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Customers.AsNoTracking().Select(f => new CustomerDto
                {
                    CustomerID = f.CustomerId,
                    CustomerName = f.CustomerName,
                    Equipments = f.Equipments.ToList(),
                    Departments = f.CustomerDepartments.ToList()
                }).ToListAsync();
            }
        }

        //public async Task<IEnumerable<ComponentDto>> GetComponentsByEquipmentIdAsync(int equipmentId)
        //{
        //    using (var ctx = _contextCreator())
        //    {
        //        var result = from equipment in ctx.Equipments
        //                .Where(equipment => equipment.EquipmentId == equipmentId)
        //                     join equipmentmodel in ctx.EquipmentComponents
        //                         on equipment.EquipmentId equals equipmentmodel.EquipmentID
        //                     join model in ctx.Components
        //                         on equipmentmodel.ComponentID equals model.ComponentId
        //                     select new ComponentDto()
        //                     {
        //                         ComponentId = model.ComponentId,
        //                         ComponentName = model.ComponentName,
        //                         ComponentDescription = model.ComponentDescription,
        //                         ComponentOrderNumber = model.ComponentOrderNumber,
        //                         ComponentQuantity = model.ComponentQuantity,
        //                         ComponentSerialNumber = model.ComponentSerialNumber,
        //                         ComponentSupplyNumber = model.ComponentSupplyNumber
        //                     };
        //        return await result.ToListAsync(); // or whatever non-deferred you want
        //    }
        //}
    }
}
