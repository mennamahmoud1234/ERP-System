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
    public class AttendConfigurations : IEntityTypeConfiguration<Attend>
    {
        public void Configure(EntityTypeBuilder<Attend> builder)
        {
            builder.Property(A => A.CheckIn)
                .IsRequired()
                .HasMaxLength(255);

           
            builder.Property(A => A.CheckOut)
             .IsRequired()
             .HasMaxLength(255);


            builder.Property(A => A.Delay)
             .IsRequired()
             .HasMaxLength(255);

            //  FK [one to many] =>Employee
            //builder.HasMany(A => A.Employees)
            //       .WithOne(E => E.Attends)
            //       .HasForeignKey(E => E.AttendId)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
