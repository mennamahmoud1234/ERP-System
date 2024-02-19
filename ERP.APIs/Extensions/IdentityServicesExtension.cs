using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ERP.Core.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ERP.Core.Data;
using ERP.Core.Services.Contract;
using ERP.Service;

namespace ERP.APIs.Extentions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration configuration)
        {
            #region Allow DI For Identity Services
            services.AddIdentity<Employee, IdentityRole>()
            .AddEntityFrameworkStores<ERPDBContext>();


            //JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"]))
                };
            });
            #endregion
            return services;
        }
    }
}

