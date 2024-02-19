using ERP.Core.Entities;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.SCmOrderProduct
{
      public class ScmOrderSpec :BaseSpecifications<ScmOrder> 
    {
        public ScmOrderSpec() : base()
        {
            Includes.Add(S0 => S0.ScmOrderProducts);
            IncludeStrings.Add($"{nameof(ScmOrder.ScmOrderProducts)}.{nameof(ScmOrderProduct.Product)}");
        }
        public ScmOrderSpec(ProductSpecParams productSpecParams, string AccEmployee) : base((I => I.AccEmployeeId== AccEmployee && I.Status == 0))
        {
           AddPagination((productSpecParams.PageIndex - 1) * productSpecParams.PageSize, productSpecParams.PageSize);
            Includes.Add(S0 => S0.ScmOrderProducts);
            IncludeStrings.Add($"{nameof(ScmOrder.ScmOrderProducts)}.{nameof(ScmOrderProduct.Product)}");
        }
        public ScmOrderSpec(int ScmId) : base(SO=>SO.Id==ScmId)
        {
            Includes.Add(S0 => S0.ScmOrderProducts);
            IncludeStrings.Add($"{nameof(ScmOrder.ScmOrderProducts)}.{nameof(ScmOrderProduct.Product)}");
        }

    }
}

