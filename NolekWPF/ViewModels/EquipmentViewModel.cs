using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.ViewModels
{
    public class EquipmentViewModel
    {
        public int EquipmentId { get; set; }
        public System.DateTime EquipmentDateCreated { get; set; }
        public string EquipmentImagePath { get; set; }
        public string EquipmentSerialnumber { get; set; }
        public string EquipmentMainEquipmentNumber { get; set; }
        public bool EquipmentStatus { get; set; }
        public string EquipmentCategory { get; set; }
        public string EquipmentTypeName { get; set; } //from EquipmentType
        public string EquipmentConfigurationDescription { get; set; } //from EquipmntConfiguration
    }
}
