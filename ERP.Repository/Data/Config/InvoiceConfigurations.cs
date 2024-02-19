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
    internal class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            //What is required
            builder.Property(P => P.Total)
                .HasColumnType("decimal(10, 2)");
            builder.Property(P => P.TaxValue)
                .HasColumnType("decimal(10, 2)");
            builder.Property(P => P.Paid)
                .HasColumnType("decimal(10, 2)");
            builder.Property(P => P.ToPay)
                .HasColumnType("decimal(10, 2)");

            //Relationship one to many {Invoice , Supplier}
            builder.HasOne(I => I.Supplier)
                .WithMany(S => S.Invoices)
                .HasForeignKey(I => I.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            //Relationship one to many {Invoice , Employee}
            builder.HasOne(I => I.Employee)
                .WithMany(E => E.Invoices)
                .HasForeignKey(I => I.OrderBy)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
