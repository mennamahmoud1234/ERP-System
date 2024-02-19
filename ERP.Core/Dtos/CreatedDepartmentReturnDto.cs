using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
     public class CreatedDepartmentReturnDto
    {
        public int Id { get; set; } 
        public string DepartmentName { get; set; }

        public int? ParentDepartmentId { get; set; }
    }
}
