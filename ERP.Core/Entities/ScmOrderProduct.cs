using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class ScmOrderProduct 
    {
        public int Quantity { get; set; }
        #region Navigation Property [One] => Product
        public int ProductId { get; set; } 
        public Product Product { get; set; }
        #endregion
        #region Navigation Property [One] => ScmOrder
        public int ScmId { get; set; }
        public ScmOrder ScmOrder { get; set; }
        #endregion

    }
}
