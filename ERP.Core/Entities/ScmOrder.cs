using ERP.Core.Identity;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class ScmOrder : BaseEntity
    {
        public string Reference { get; set; }
        
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;

        public int Status { get; set; }

        // Navigation property [many] => ScmOrderProduct
        public ICollection<ScmOrderProduct> ScmOrderProducts { get; set; }=new HashSet<ScmOrderProduct>();

        #region Navigation property [one] => Employee
        public string ScmEmployeeId { get; set; }
        [ForeignKey(nameof(ScmEmployeeId))]
        //[InverseProperty(nameof(Employee.ScmOrders))]
        public  Employee EmployeeScm { get; set; }
        #endregion

        #region Navigation property [one] => Employee
        public string AccEmployeeId { get; set; }
        [ForeignKey(nameof(AccEmployeeId))]
        //[InverseProperty(nameof(Employee.ScmOrders))]
        public Employee EmployeeAcc { get; set; }
        #endregion

        public Invoice Invoice { get; set; }
    }
}
