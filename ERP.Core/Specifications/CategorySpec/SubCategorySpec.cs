using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.CategorySpec
{
    public class SubCategorySpec : BaseSpecifications<SubCategory>
    {
        public SubCategorySpec(string subCategoryName, int parentCategoryId) : base(sc => sc.SubCategoryName == subCategoryName && sc.ParentCategoryId == parentCategoryId)
        {
        }


        public SubCategorySpec(string subCategoryName, int parentCategoryId,int SubId) : base((sc => sc.SubCategoryName == subCategoryName && sc.ParentCategoryId == parentCategoryId && sc.Id == SubId))
        {
        }
        
    }
}
