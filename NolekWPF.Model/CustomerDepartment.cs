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
    
    public partial class CustomerDepartment
    {
        public int CustomerDepartmentId { get; set; }
        public string CustomerDepartmentName { get; set; }
        public int CustomerId { get; set; }
        public int CountryId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Customer Customer { get; set; }
    }
}