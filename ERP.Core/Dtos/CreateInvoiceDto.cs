using ERP.Core.Entities;
using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
     public class CreateInvoiceDto
    {

       
        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int TaxID { get; set; }

        public int? InventoryOrderId { get; set; }
       
        public  int? ScmOrderId { get; set; }



    }
}
