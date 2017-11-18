using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.DataAccess;
using NolekWPF.Model;

namespace NolekWPF.DataServices.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private wiki_nolek_dk_dbEntities _context;

        public EquipmentRepository(wiki_nolek_dk_dbEntities context)
        {
            _context = context; //context is kept alive throughout the application lifetime
        }

        public void Add(Equipment equipment)
        {
            _context.Equipments.Add(equipment); //call insert to add new equipement to table
        }

        public async Task<Equipment> GetByIdAsync(int equipId)
        {
            return await _context.Equipments.SingleAsync(f => f.EquipmentId == equipId); //return equipement with the id
        }
        public async Task<IEnumerable<EquipmentTypeDto>> GetEquipmentTypes()
        {
            return await _context.EquipmentTypes.Select(t => new EquipmentTypeDto()
            {
                EquipmentTypeID = t.EquipmentTypeID,
                EquipmentTypeDescription = t.EquipmentTypeDescription,
                EquipmentTypeName = t.EquipmentTypeName

            }).ToListAsync();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges(); //return true if current equipement has changes
        }

        public void Remove(Equipment model)
        {
            _context.Equipments.Remove(model); //delete equipment from the db
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); //save all changes to the current context
        }
    }
}
