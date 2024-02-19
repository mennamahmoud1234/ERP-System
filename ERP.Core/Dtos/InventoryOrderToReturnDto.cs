using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class InventoryOrderToReturnDto
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Reference { get; set; }
        public string InventoryEmployee { get; set; }
  
        
    }
}
