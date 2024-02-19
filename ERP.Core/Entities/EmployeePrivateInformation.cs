using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
     public  class EmployeePrivateInformation :BaseEntity
    { 
        public string Address { get; set; } 

        public string Phone { get; set; }

        public string EmergencyName { get; set; }

        public string EmergencyPhone { get; set; }

        public string Nationality { get; set; }

        public string IdentificationNo { get; set; }

        public string PassportNo { get; set; }

        public int Gender { get; set; }


        public DateTime BirthDate { get; set; }


        #region Navigation property [one] => Employee

        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } 
        #endregion
    }
}
