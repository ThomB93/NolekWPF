using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Model.Dto
{
    public class ComponentDto
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentDescription { get; set; }
        public string ComponentOrderNumber { get; set; }
        public string ComponentSerialNumber { get; set; }
        public int ComponentQuantity { get; set; }
        public string ComponentSupplyNumber { get; set; }
    }
}
