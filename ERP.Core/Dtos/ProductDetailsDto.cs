using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class ProductDetailsDto : ProductDto
    {
        public string ProductBarcode { get; set; }
        public int ProductInComing { get; set; }
        public int ProductOutGoing { get; set; }
        public decimal ProductSellPrice { get; set; }
        public decimal ProductCostPrice { get; set; }
        public int ActiveOrder { get; set; }
        public string Employee { get; set; }

    }
}
