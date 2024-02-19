using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class PaymentDto
    {
        public decimal Amount { get; set; }
        public int InvoiceId { get; set; }
        public int SupplierId { get; set; }
    }
}
