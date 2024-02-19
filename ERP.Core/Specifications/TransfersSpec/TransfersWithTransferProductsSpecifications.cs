using ERP.Core.Entities;
using ERP.Core.Specifications.Product_Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ERP.Core.Specifications.TransfersSpec
{
    public class TransfersWithTransferProductsSpecifications : BaseSpecifications<Transfer>
    {
        public TransfersWithTransferProductsSpecifications(ProductSpecParams productSpecParams) :base()
        {
            Includes.Add(P => P.TransferProducts);

            AddPagination((productSpecParams.PageIndex - 1) * productSpecParams.PageSize , productSpecParams.PageSize );
                

        }

    }
}
