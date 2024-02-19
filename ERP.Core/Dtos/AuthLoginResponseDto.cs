using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Dtos
{
    public class AuthLoginResponseDto : AuthRegisterResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public UserResponseDto User { get; set; }
    }
}
