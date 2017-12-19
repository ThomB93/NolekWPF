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
        public System.DateTime EquipmentDateCreated { get; set; }
        public string EquipmentImagePath { get; set; }
        public string EquipmentSerialnumber { get; set; }
        public string EquipmentMainEquipmentNumber { get; set; }
        public bool EquipmentStatus { get; set; }
        public Nullable<int> ContactPersonId { get; set; }
        public string TypeName { get; set; }
        public string Category { get; set; }
        public string Configuration { get; set; }
    }
}
