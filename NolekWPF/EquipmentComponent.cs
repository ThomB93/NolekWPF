//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NolekWPF
{
    using System;
    using System.Collections.Generic;
    
    public partial class EquipmentComponent
    {
        public int ComponentID { get; set; }
        public int EquipmentID { get; set; }
        public int EquipmentComponentQuantity { get; set; }
    
        public virtual Component Component { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
