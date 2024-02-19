using ERP.Core.Entities;
using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateTimeOffset BillDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Paid { get; set; }
        public decimal ToPay { get; set; }

        public ICollection<PaymentReturnDto> Payment { get; set; }
        public string Employee { get; set; }
        public string Supplier { get; set; }

        public int? InventoryOrderId { get; set; }
        public int? ScmOrderId { get; set; }
        public string Tax { get; set; }
    }
}
