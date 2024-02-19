using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class SubCategory : BaseEntity
    {
        public string SubCategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public ParentCategory ParentCategory { get; set; }

        // Navigation Property [Many] For products
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
