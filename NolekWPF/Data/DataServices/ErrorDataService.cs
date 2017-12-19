using NolekWPF.DataAccess;
using NolekWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Data.DataServices
{
    public class ErrorDataService : IErrorDataService
    {
        private Func<wiki_nolek_dk_dbEntities> _contextCreator; //using func allows for context to be used like a method

        public ErrorDataService(Func<wiki_nolek_dk_dbEntities> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task AddError(Error error)
        {
            using (var ctx = _contextCreator())
            {
                ctx.Errors.Add(error);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
