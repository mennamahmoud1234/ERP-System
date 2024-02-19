using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class EmployeeWorkInformation :BaseEntity
    {
        public string WorkMobile { get; set; }

        public string WorkPhone { get; set; }

        public string WorkEmail { get; set; }
        
        public string BankAccount { get; set; }
        
        public string WorkPermitNo { get; set; }

        public DateTime WorkPermitExpirationDate { get; set; }

        #region Navigation property [one] =>Employee

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        #endregion

    }
}
