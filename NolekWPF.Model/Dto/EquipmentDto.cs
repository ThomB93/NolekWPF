using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Model.Dto
{
    public class EquipmentDto
    {
        public int EquipmentId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string Serialnumber { get; set; }
        public string MainEquipmentNumber { get; set; }
        public bool Status { get; set; }
        public Nullable<int> ContactPersonId { get; set; }
        public string TypeName { get; set; }
        public string Category { get; set; }
        public string Configuration { get; set; }
    }
}
