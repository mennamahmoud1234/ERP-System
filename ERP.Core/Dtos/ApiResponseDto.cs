using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class ApiResponseDto<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        
        public T Data { get; set; }
    }
}
