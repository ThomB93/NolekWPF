using NolekWPF.DataAccess;
using NolekWPF.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Data.DataServices
{
    public class ComponentDataService : IComponentDataService
    {
        private Func<wiki_nolek_dk_dbEntities> _contextCreator;

        public ComponentDataService(Func<wiki_nolek_dk_dbEntities> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<Component> GetByIdAsync(int componentId)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Components.AsNoTracking().SingleAsync(f => f.ComponentId == componentId);
            }
        }

        public async Task<IEnumerable<Component>> GetComponentLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Components.AsNoTracking().Select(f => new Component
                {
                    ComponentId = f.ComponentId,
                    ComponentDescription = f.ComponentDescription,
                    ComponentName = f.ComponentName,
                    ComponentOrderNumber = f.ComponentOrderNumber,
                    ComponentQuantity = f.ComponentQuantity,
                    ComponentSerialNumber = f.ComponentSerialNumber,
                    ComponentSupplyNumber = f.ComponentSupplyNumber,
                    EquipmentComponents = f.EquipmentComponents

                }).ToListAsync();
            }
        }
    }
}
