using ERP.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Data.Config
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(emp => emp.EmployeeJob)
                   .IsRequired()
            .HasMaxLength(255);

            builder.ToTable("Employees");
            
            builder.HasOne(e => e.AddedBy)
                .WithMany()
                .HasForeignKey(e => e.AddedById)
                .OnDelete(DeleteBehavior.NoAction); 
            // EmployeeDepartment  => Constrain ?? 

        }
    }
}
