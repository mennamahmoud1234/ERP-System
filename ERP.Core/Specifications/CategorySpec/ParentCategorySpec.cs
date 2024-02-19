using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.CategorySpec
{
    public class ParentCategorySpec : BaseSpecifications<ParentCategory>
    {
        public ParentCategorySpec() : base()
        {
            Includes.Add(pc => pc.SubCategories);
        }
        public ParentCategorySpec(string parentCategoryName) : base(pc => pc.ParentCategoryName == parentCategoryName)
        {
        }
        public ParentCategorySpec(int parentCategoryId) : base(pc=>pc.Id == parentCategoryId)
        {
        }
        public ParentCategorySpec(string parentCategoryName,int parentCategoryId) : base(pc=> pc.ParentCategoryName== parentCategoryName && pc.Id == parentCategoryId )
        {
        }

        
    }
}
