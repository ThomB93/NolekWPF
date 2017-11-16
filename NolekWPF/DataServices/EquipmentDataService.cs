using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.DataAccess;
using System.Data.Entity;

namespace NolekWPF.DataServices
{
    public class EquipmentDataService
    {
        private Func<wiki_nolek_dk_dbEntities> _contextCreator; //using func allows for context to be used like a method

        public EquipmentDataService(Func<wiki_nolek_dk_dbEntities> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<Equipment> GetByIdAsync(int equipmentId) 
        {
            using (var ctx = _contextCreator()) 
            {
                var equipments = await ctx.Equipments.AsNoTracking().SingleAsync(f => f.EquipmentId == equipmentId); 
                //await Task.Delay(5000);
                return equipments;
            }
        }
    }
}
