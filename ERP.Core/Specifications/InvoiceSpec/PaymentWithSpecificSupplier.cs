using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.InvoiceSpec
{
    public class PaymentWithSpecificSupplier : BaseSpecifications<Payment>
    {
        public PaymentWithSpecificSupplier(int Id):base(P => P.SupplierId == Id) 
        {
            Includes.Add(P => P.Employee);
            Includes.Add(P => P.Invoice);
            Includes.Add(P => P.Supplier);
        }
    }
}
