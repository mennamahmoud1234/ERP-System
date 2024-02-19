using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
     public class ScmOrderDto
    {
       
        public string Reference { get; set; }
        public string AccEmployeeId { get; set; }
        public List<ScmOrderProductsDto> Products { get; set; } = new List<ScmOrderProductsDto>();
    }
}
