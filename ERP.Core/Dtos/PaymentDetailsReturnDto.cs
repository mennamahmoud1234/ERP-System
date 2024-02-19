using ERP.Core.Entities;
using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class PaymentDetailsReturnDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset PaymentDate { get; set; }
        public int InvoiceId { get; set; }
        public string Employee { get; set; }
        public string Supplier { get; set; }
    }
}
