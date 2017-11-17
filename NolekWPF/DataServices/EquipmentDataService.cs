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
        //get equipment by id
        public async Task<Equipment> GetByIdAsync(int equipmentId) 
        {
            using (var ctx = _contextCreator()) 
            {
                var equipments = await ctx.Equipments.AsNoTracking().SingleAsync(f => f.EquipmentId == equipmentId); 
                return equipments;
            }
        }
        //insert new equipment into db
        public async void InsertNewEquipment(Equipment equipment)
        {
            using (var ctx = _contextCreator())
            {
                ctx.Equipments.Add(equipment);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
