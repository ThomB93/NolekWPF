﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.Data.DataServices
{
    public interface IComponentDataService
    {
        Task<Component> GetByIdAsync(int componentId);
        Task<IEnumerable<Component>> GetComponentLookupAsync();
    }
}