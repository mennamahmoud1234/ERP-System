
using ERP.APIs.Extensions;
using ERP.APIs.Extentions;
using ERP.APIs.Middleware;
using ERP.Core.Data;
using ERP.Core.Identity;
using ERP.Core.Services.Contract;
using ERP.Repository.SeedData;
using ERP.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var WebApplicationBuilder = WebApplication.CreateBuilder(args);

#region Configure Services

// Add services to the container.
WebApplicationBuilder.Services.AddControllers();

WebApplicationBuilder.Services.AddSwaggerServices(); // My Own Extension Method To Allow DI for Swagger Services => To Make Much Readable
#region Allow DI For DbContext
WebApplicationBuilder.Services.AddDbContext<ERPDBContext>(Options =>
{
    string Connection;
    //Local Connection
    Connection = WebApplicationBuilder.Configuration.GetConnectionString("DefaultConnection");

    //Remote Connection
    //Connection = WebApplicationBuilder.Configuration.GetConnectionString("RemoteConnection");

    Options.UseSqlServer(Connection);
});



#endregion
WebApplicationBuilder.Services.AddApplicationSevice();// use extension method i created
WebApplicationBuilder.Services.AddIdentityServices(WebApplicationBuilder.Configuration);
WebApplicationBuilder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
#endregion

var app = WebApplicationBuilder.Build();

  #region Update-Databas Automatic

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var _dbcontext = services.GetRequiredService<ERPDBContext>(); //ask clr for create obj from dbcontext
var _loggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    var migrations = await _dbcontext.Database.GetPendingMigrationsAsync();


    await _dbcontext.Database.MigrateAsync();             //Update Database



    var userManager = services.GetRequiredService<UserManager<Employee>>();
    var roleManager = services.GetRequiredService <RoleManager<IdentityRole>> ();
    await AppDbContextSeed.SeedRolesAsync(roleManager);
    await AppDbContextSeed.SeedDepartmentsAsync(_dbcontext);
    await AppDbContextSeed.SeedUserAsync(userManager);
}
catch (Exception ex)
{
    var logger = _loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error Occured During Apply The Migration");
}

#endregion

#region Configure Kestrel Middleware



// Configure the HTTP request pipeline.
app.Services.CreateScope();
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerMiddleware();
}



#region Not found End Point Handler

app.UseStatusCodePagesWithReExecute("/errors/{0}");

#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
#endregion

app.Run();