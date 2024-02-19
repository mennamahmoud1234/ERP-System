using ERP.Core.Identity;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class InventoryOrder : BaseEntity
    {  
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        public int Status { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }

        // Navigation property One => Employee
        public string InventoryEmployee { get; set; }
        public Employee InventoryEmp { get; set; }

        // Navigation property One => Employee
        public string AccEmployee { get; set; }
        public Employee AccEmp { get; set; }

        
        #region Navigation Property [One] => Product
        //Fk => Product
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        #endregion
        public Invoice Invoice { get; set; }
    }
}
