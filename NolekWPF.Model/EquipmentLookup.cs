using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Model
{
    public class EquipmentLookup
    {
        public int EquipmentId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string SerialNumber { get; set; }
        public string MainEquipmentNumber { get; set; }
        public bool Status { get; set; }
        public Nullable<int> ContactPersonId { get; set; }
        public string TypeName { get; set; }
        public string Category { get; set; }
        public string Configuration { get; set; }
        public string ImagePath { get; set; }
        public string ContactPersonFirstName { get; set; }
        public string ContactPersonLastName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonTelephone{ get; set; }
    }
}
