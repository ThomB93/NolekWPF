using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolekWPF.Model.Dto
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public List<CustomerDepartment> Departments { get; set; }
        public List<Equipment> Equipments { get; set; }

    }
}
