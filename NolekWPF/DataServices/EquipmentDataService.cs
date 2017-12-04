using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.DataAccess;
using NolekWPF.Model;
using System.Data.Entity;

namespace NolekWPF.DataServices
{
    public class EquipmentDataService : IEquipmentDataService
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
                return await ctx.Equipments.AsNoTracking().SingleAsync(f => f.EquipmentId == equipmentId); 
            }
        }
        public async Task<EquipmentView> GetViewByIdAsync(int equipmentId)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.EquipmentViews.AsNoTracking().SingleAsync(
                    f => f.EquipmentId == equipmentId);
            }
        }

        public async Task<IEnumerable<EquipmentType>> GetTypesAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.EquipmentTypes.AsNoTracking().Select(t => new EquipmentType()
                {
                    EquipmentTypeID = t.EquipmentTypeID,
                    EquipmentTypeDescription = t.EquipmentTypeDescription,
                    EquipmentTypeName = t.EquipmentTypeName

                }).ToListAsync();
            }
        }
    }
}
