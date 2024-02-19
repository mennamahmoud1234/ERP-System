using ERP.Core.Entities;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.SupplierSpec
{
    public class SupplierSpec : BaseSpecifications<Supplier>
    {
        public SupplierSpec(string supplierEmailOrPhone) : base(s => (s.SupplierEmail == supplierEmailOrPhone || s.SupplierPhone == supplierEmailOrPhone))
        {
            Includes.Add(P => P.Employee);
        }
        public SupplierSpec(int supplierId) : base(s => (s.Id == supplierId))
        {
            Includes.Add(P => P.Employee);
        }
        public SupplierSpec(SupplierSpecParams supplierSpecParams) : base()
        {
            Includes.Add(P => P.Employee);
            AddPagination((supplierSpecParams.PageIndex - 1) * supplierSpecParams.PageSize, supplierSpecParams.PageSize);

        }
    }
}
