using ERP.Core;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Identity;
using ERP.Core.RepositryContract;
using ERP.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ERP.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager,IUnitOfWork unitOfWork,IEmployeeRepository employeeRepository, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _configuration = configuration;
        }
        public async Task<AuthRegisterResponseDto> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null) 
                return new AuthRegisterResponseDto { Message = "Email is already registered" };

            ///chack AddedBy
            var SpacificUser = await _employeeRepository.GetEmployeeAsync(model.AddedById);
            if (SpacificUser == null) return new AuthRegisterResponseDto { Message = "Invalid AddedById" };

            //chack EmployeeDepartment
            var SpacificDepartment = await _unitOfWork.Repositry<Department>().GetAsync(model.EmployeeDepartmentId);
            if (SpacificDepartment == null) return new AuthRegisterResponseDto { Message = "Invalid EmployeeDepartmentId" };


            var user = new Employee
            {
                UserName = model.UserName,
                Email = model.Email,
                EmployeeJob = model.EmployeeJob,
                AddedById = model.AddedById,
                EmployeeDepartment = model.EmployeeDepartmentId
            };
            //chack and add Role
            bool roleExists = await _roleManager.RoleExistsAsync(model.Role);

            if (!roleExists) return new AuthRegisterResponseDto { Message = "Invalid Role" };

            

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthRegisterResponseDto { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            return new AuthRegisterResponseDto()
            {
                Message = "Registered successfully",
                IsAuthenticated = true
            };
        }

        public async Task<AuthLoginResponseDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthLoginResponseDto { Message = "Email or Password is incorrect" };
            }

            //check if user deleted
            if(user.IsDeleted) return new AuthLoginResponseDto { Message = "Your account has been deleted" };

            var rolesList = await _userManager.GetRolesAsync(user);
            
            //Get Token
            string tokenString = await CreateTokenAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

            return new AuthLoginResponseDto
            {
                Message = "Login successfully",
                IsAuthenticated = true,
                Token = tokenString,
                ExpiresOn = token.ValidTo,
                User = new UserResponseDto
                {
                    UserID = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                    Roles = rolesList.ToList()
                }
            };

        }

        public async Task<string> CreateTokenAsync(Employee user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            // Private Claims (User-Defined)
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            authClaims.Union(userClaims);
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                    audience: _configuration["JWT:ValidAudience"],
                    issuer: _configuration["JWT:ValidIssuer"],
                    expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature
                    ));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}