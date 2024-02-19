using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class ProductReturnDto 
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }
        [Required]
        public int ProductOnHand { get; set; }
        [Required]
        public string ProductBarcode { get; set; }
        [Required]
        public int ProductInComing { get; set; }
        [Required]
        public int ProductOutGoing { get; set; }
        [Required]
        public decimal ProductSellPrice { get; set; }
        [Required]
        public decimal ProductCostPrice { get; set; }
        [Required]
        public int ActiveOrder { get; set; }
        public string? AddedBy { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
    }
}
