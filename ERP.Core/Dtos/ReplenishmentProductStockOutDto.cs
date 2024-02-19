using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
     public class ReplenishmentProductStockOutDto
    {   
        public int Id { get; set; }    
        public int?  ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductBarcode { get; set; }
        public int ProductOnHand { get; set; }
        public decimal ProductSellPrice { get; set; }

    }
}
