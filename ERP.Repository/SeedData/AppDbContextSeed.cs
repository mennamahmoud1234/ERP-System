using ERP.Core.Data;
using ERP.Core.Entities;
using ERP.Core.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Repository.SeedData
{
    public static class AppDbContextSeed
    {
        #region Seed User
        public static async Task SeedUserAsync(UserManager<Employee> userManager)
        {
            if (userManager.Users.Count() == 0)
            {
                // Define user data want to seed
                var user = new Employee()
                {
                    Email = "basmamohsen53@gmail.com",
                    UserName = "BasmaMohsen",
                    PhoneNumber = "01014654026",
                    EmployeeDepartment = 1,
                    EmployeeJob = "test"
                };
                //Create the user
                await userManager.CreateAsync(user, "P@$$w0rd");
            }
        } 
        #endregion

        #region Seed Roles
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Define roles you want to seed
            List<string> roleNames = new List<string> { "Manager", "Employee", "HR" };

            foreach (var roleName in roleNames)
            {
                // Check if the role doesn't exist
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    // Create the role
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }
        }
        #endregion

        public static async Task SeedDepartmentsAsync(ERPDBContext context)
        {
            // Define Departments you want to seed
            List<string> Departments = new List<string> { "INVENTORY", "HR" };
            foreach (var DepartmentName in Departments)
            {
                // Check if the Department doesn't exist
                if (!context.Departments.Any(d => d.DepartmentName == DepartmentName))
                {
                    // Create the Department
                    var Department = new Department() { DepartmentName = DepartmentName };
                    await context.Departments.AddAsync(Department);
                }
            }

            // Save changes to the database
            await context.SaveChangesAsync();
        }
    }
}
