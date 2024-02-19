using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
     public class JobPosition :BaseEntity
    {
        public string JobName { get; set; }

        
        
        #region Navigational proberty [one] => Department
        public int? DepartmentId { get; set; }

        public Department Department { get; set; } 
        #endregion

    }
}
