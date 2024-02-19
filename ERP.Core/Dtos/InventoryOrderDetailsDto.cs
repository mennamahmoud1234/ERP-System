using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class InventoryOrderDetailsDto : InventoryOrderDto
    {
        public DateTimeOffset Date { get; set; }
        public string Reference { get; set; }
        public int Quantity { get; set; }

        public string InventoryEmployee { get; set; }
        public string AccEmployee { get; set; }
        public string Product { get; set; }





    }
}
