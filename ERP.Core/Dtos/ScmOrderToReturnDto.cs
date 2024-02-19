using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class ScmOrderToReturnDto
    {
       
        public IReadOnlyList<ScmOrderProductsReturnDto> ScmOrderProducts { get; set; }
        public string Reference { get; set; }
       
        
    }
}
