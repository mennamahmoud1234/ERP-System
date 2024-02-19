using ERP.Core.Entities;
using ERP.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Data
{
    public class ERPDBContext : IdentityDbContext<Employee>
    {


        public ERPDBContext(DbContextOptions<ERPDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
       

        public DbSet<Attend> Attends { get; set; }

        public DbSet<ParentCategory> ParentCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeePrivateInformation> EmployeePrivateInformations { get; set; }
        public DbSet<EmployeeWorkInformation> EmployeeWorkInformations { get; set; }
        public DbSet<InventoryOrder> InventoryOrders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<MovesHistory> MovesHistories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Replenishment> Replenishments { get; set; }
        public DbSet<ScmOrder> ScmOrders { get; set; }
        public DbSet<ScmOrderProduct> ScmOrderProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferProduct> TransferProducts { get; set; }




    }
}