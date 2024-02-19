using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class CategoryToReturnDto
    {
        public int ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public List<SubCategoryToReturnDto> SubCategories { get; set; }

    }
}
