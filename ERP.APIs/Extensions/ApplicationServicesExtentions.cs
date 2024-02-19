using ERP.APIs.Errors;
using ERP.APIs.Helper;
using ERP.Core;
using ERP.Core.Identity;
using ERP.Core.RepositryContract;
using ERP.Core.Services.Contract;
using ERP.Repository;
using ERP.Service;
using Microsoft.AspNetCore.Mvc;


namespace ERP.APIs.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static  IServiceCollection AddApplicationSevice( this IServiceCollection services)
        {
            services.AddScoped(typeof(ISCMService), typeof(SCMService));
            services.AddScoped(typeof(IInventoryService), typeof(InventoryService));
            services.AddScoped(typeof(IAccountingService), typeof(AccountingService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));
            services.AddScoped(typeof(ICategoryRepo), typeof(CategoryRepo));
            services.AddScoped(typeof(IHRServices), typeof(HRServices));
            services.AddScoped<IAuthService, AuthService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var error = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0).SelectMany(p => p.Value.Errors).Select(E => E.ErrorMessage).ToArray();  // كدا مسكت البارامتر اللى فاليو بتاعتهم  فيها مشكله
                    var validationError = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };

                    return new BadRequestObjectResult(validationError);


                };

            }); // validation error configuration change default behavior
            return services;
        
        }

    }
}
