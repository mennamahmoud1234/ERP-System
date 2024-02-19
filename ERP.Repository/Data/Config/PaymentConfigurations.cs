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
    internal class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(P => P.Amount)
                .HasColumnType("decimal(10, 2)");


            //Relationship one to many {Employee , Payment}
            builder.HasOne(P => P.Employee)
                .WithMany(E => E.Payments)
                .HasForeignKey(P => P.DoneBy)
                .OnDelete(DeleteBehavior.NoAction);

            //Relationship one to many {Employee , Payment}
            builder.HasOne(P => P.Supplier)
                .WithMany(S => S.Payments)
                .HasForeignKey(P => P.SupplierId);
        }
    }
}
