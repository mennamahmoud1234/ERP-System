using ERP.Core.Entities;
using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
     public class DepartmentToReturnDto
    {
        public int Id { get; set; }
        
        public string DepartmentName { get; set; }
       
        public ICollection<JobPositionData> JobPositions { get; set; } 

        public ICollection<EmployeeData> Employees { get; set; } 

        public int? ParentDepartmentId { get; set; }
        public string? ParentDepartmentName { get; set; }
        public ICollection<DepartmentData> ChildDepartment { get; set; }
    }
}
