using ERP.Core.Entities;
using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
     public class DepartmentDto
    {
        [Required]
        [StringLength(50)]
        public string DepartmentName { get; set; }
       
        public int? ParentDepartmentId { get; set; }
    }
}
