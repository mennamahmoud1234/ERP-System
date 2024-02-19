using ERP.Core.Dtos;
using ERP.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Services.Contract
{
    public interface IAuthService
    {
        Task<AuthRegisterResponseDto> RegisterAsync(RegisterDto model);
        Task<AuthLoginResponseDto> LoginAsync(LoginDto model);
        Task<string> CreateTokenAsync(Employee user);

    }
}
