using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.DataAccess;
using NolekWPF.Model;

namespace NolekWPF.Data.Repositories
{
    public class ErrorRepository : IErrorRepository
    {
        private wiki_nolek_dk_dbEntities _context;

        public ErrorRepository(wiki_nolek_dk_dbEntities context)
        {
            _context = context;
        }
        public void Add(Error error)
        {
            _context.Errors.Add(error);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); //save all changes to the current context
        }
    }
}
