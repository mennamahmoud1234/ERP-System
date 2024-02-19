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
    internal class ScmOrderConfigurations : IEntityTypeConfiguration<ScmOrder>
    {
        public void Configure(EntityTypeBuilder<ScmOrder> builder)
        {
            builder.Property(IO => IO.Reference)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Date)
                .IsRequired();

            //Fk [one to many]   => Employee
            builder.HasOne(SO => SO.EmployeeScm)
                    .WithMany(E => E.ScmOrders)
                    .HasForeignKey(SO => SO.ScmEmployeeId)
                    .OnDelete(DeleteBehavior.NoAction);

            //Fk [one to many]   => Employee
            builder.HasOne(SO => SO.EmployeeAcc)
                    .WithMany(E => E.ScmOrdersAcc)
                    .HasForeignKey(SO => SO.AccEmployeeId)
                    .OnDelete(DeleteBehavior.NoAction);

             
        }
    }
}
