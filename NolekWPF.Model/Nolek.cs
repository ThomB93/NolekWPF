//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NolekWPF.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Nolek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nolek()
        {
            this.Technicians = new HashSet<Technician>();
        }
    
        public int NolekId { get; set; }
        public int CountryId { get; set; }
        public string DepartmentName { get; set; }
        public int ContactPersonId { get; set; }
    
        public virtual ContactPerson ContactPerson { get; set; }
        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Technician> Technicians { get; set; }
    }
}
