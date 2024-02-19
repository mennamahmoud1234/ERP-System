using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class ParentCategory : BaseEntity
    {
        public string ParentCategoryName { get; set; }

        #region One To many Relationship
        public ICollection<SubCategory> SubCategories { get; set; }
        #endregion


    }
}
