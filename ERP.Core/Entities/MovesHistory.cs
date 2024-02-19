using ERP.Core.Identity;
using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Entities
{
    public class MovesHistory : BaseEntity
    {
        public string Reference { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        
        #region Navigation property [One] => Employee
        public string DoneBy { get; set; }
        public Employee Employee { get; set; } 
        #endregion


        #region Navigation property [One] => Product
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
