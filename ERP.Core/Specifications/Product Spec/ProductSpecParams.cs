using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Specifications.Product_Spec
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 15;

        private int pageSize = 15;

        private int pageIndex = 1;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize || value <= 0)  ? MaxPageSize : value; }
        }
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value >= pageIndex ? value : pageIndex; }
        }


    }
}
