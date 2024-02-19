using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class Department :BaseEntity
    {
        public string DepartmentName { get; set; }
        // Navigtional proberty [many] =>obPosition
        public ICollection<JobPosition> JobPositions { get; set; }=new HashSet<JobPosition>(); 
       
         // Navigtional proberty [many] => Employee
        public ICollection<Employee> Employees { get; set; } =new HashSet<Employee>();

        #region Self Relationship
        public int? ParentDepartmentId { get; set; }
        public Department? ParentDepartment { get; set; }
        public ICollection<Department>  ChildDepartment { get; set; }
        #endregion
    }
}
