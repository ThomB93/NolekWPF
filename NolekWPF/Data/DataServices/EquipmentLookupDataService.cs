using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.DataAccess;
using NolekWPF.Model;
using System.Data.Entity;

namespace NolekWPF.Data.DataServices
{
    public class EquipmentLookupDataService : IEquipmentLookupDataService
    {
        private Func<wiki_nolek_dk_dbEntities> _contextCreator;

        public EquipmentLookupDataService(Func<wiki_nolek_dk_dbEntities> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<EquipmentLookup>> GetEquipmentLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Equipments.AsNoTracking().Select(f => new EquipmentLookup
                {
                    EquipmentId = f.EquipmentId,
                    SerialNumber = f.EquipmentSerialnumber,
                    TypeName = f.EquipmentType.EquipmentTypeName,
                    DateCreated = f.EquipmentDateCreated,
                    MainEquipmentNumber = f.EquipmentMainEquipmentNumber,
                    Status = f.EquipmentStatus,
                    Category = f.EquipmentCategory.CategoryName,
                    Configuration = f.EquipmentConfiguration.EquipmentConfigurationDescription,
                    ContactPersonId = f.ContactPersonId,
                    ImagePath = f.EquipmentImagePath,
                    ContactPersonFirstName = f.ContactPerson.ContactPersonFirstName,
                    ContactPersonLastName = f.ContactPerson.ContactPersonLastName,
                    ContactPersonEmail = f.ContactPerson.ContactPersonEmail,
                    ContactPersonTelephone = f.ContactPerson.ContactPersonTlf
                }).ToListAsync();
            }
        }
    }

}
