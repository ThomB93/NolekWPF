using NolekWPF.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.Model;
using System.Data.Entity;
namespace NolekWPF.Data.Repositories
{
    class CustomerReporsitory : ICustomerReporsitory
    {
        private wiki_nolek_dk_dbEntities _context;

        public CustomerReporsitory(wiki_nolek_dk_dbEntities context)
        {
            _context = context; //context is kept alive throughout the application lifetime
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer); //call insert to add new equipement to table
        }

        public async Task<Customer> GetByIdAsync(int customId)
        {
            return await _context.Customers.SingleAsync(f => f.CustomerId == customId); //return equipement with the id
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
