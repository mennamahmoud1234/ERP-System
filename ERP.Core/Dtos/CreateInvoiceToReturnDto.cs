using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
     public class CreateInvoiceToReturnDto
    {
        public int Id { get; set; }
        public DateTimeOffset BillDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Total { get; set; }
        public decimal Paid { get; set; }
        public decimal ToPay { get; set; }
        public int SupplierId { get; set; }
        public string OrderBy { get; set; }
        public int? InventoryOrderId { get; set; }
        public int? ScmOrderId { get; set; }
        public int TaxValue { get; set; }
    }
}
