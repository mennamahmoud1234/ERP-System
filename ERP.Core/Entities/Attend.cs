using ERP.Core.Identity;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
     public class Attend :BaseEntity
    {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;

        public string CheckIn { get; set; }

        public string CheckOut { get; set; }

        public string Delay { get; set; }

        //Navigation property[many] => Employee
        //public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();    
    }
}
