using ERP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Data.Config
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            
            builder.Property(D=>D.DepartmentName)
                   .HasMaxLength(255)
                   .IsRequired();

            //foreign key self relationship
            builder.Property(d => d.ParentDepartmentId)
                .IsRequired(false);

            // foreign key self relationship
            builder.HasOne(d => d.ParentDepartment)
                .WithMany(d => d.ChildDepartment)
                .HasForeignKey(d => d.ParentDepartmentId)
                .OnDelete(DeleteBehavior.NoAction);


            //foreign key  [one to many] => JobPosition

            builder.HasMany(D => D.JobPositions)
                   .WithOne(JP => JP.Department)
                   .HasForeignKey(D => D.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);


            //foreign key  [one to many] => Employee

            builder.HasMany(D => D.Employees)
                   .WithOne(E => E.Department)
                   .HasForeignKey(E=>E.EmployeeDepartment);

        }
    }
}
