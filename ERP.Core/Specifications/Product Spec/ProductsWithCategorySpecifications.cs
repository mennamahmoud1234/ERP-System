using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ERP.Core.Specifications.Product_Spec
{
    public class ProductsWithCategorySpecifications : BaseSpecifications<Product>
    {
       
        public ProductsWithCategorySpecifications(ProductSpecParams productSpecParams) : base()
        {
            Includes.Add(P => P.SubCategory);
            Includes.Add(P => P.Employee);

            AddPagination((productSpecParams.PageIndex - 1) * productSpecParams.PageSize, productSpecParams.PageSize);


        }



        public ProductsWithCategorySpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(P => P.SubCategory);
            Includes.Add(P => P.Employee);
        }


       

    }
}
