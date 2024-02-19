using ERP.Core.Entities;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.PayementSpec
{
    public class PayementSpec : BaseSpecifications<Payment>
    {
        public PayementSpec(PaymentSpecParams paymentSpecParams , string userId) : base(p => p.DoneBy == userId)
        {
            Includes.Add(p => p.Supplier);
            AddPagination((paymentSpecParams.PageIndex - 1) * paymentSpecParams.PageSize, paymentSpecParams.PageSize);

        }
    }
}
